using MediatR;
using Microsoft.Extensions.Logging;
using FSH.Framework.Core.Common.Models;
using FSH.Framework.Core.Auth.Domain;
using FSH.Framework.Core.Auth.Repositories;
using FSH.Framework.Core.Auth.Domain.ValueObjects;

namespace FSH.Framework.Core.Auth.Features.Admin;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Result<CreateUserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Starting user creation process for email: {Email}", request.Email.Value);

            // Check if user with same email or username exists
            if (await _userRepository.EmailExistsAsync(request.Email.Value))
            {
                _logger.LogWarning("User with email {Email} already exists", request.Email.Value);
                return Result<CreateUserResult>.Failure($"User with email {request.Email.Value} already exists");
            }

            if (await _userRepository.UsernameExistsAsync(request.Username.Value))
            {
                _logger.LogWarning("User with username {Username} already exists", request.Username.Value);
                return Result<CreateUserResult>.Failure($"User with username {request.Username.Value} already exists");
            }

            // Create domain user entity
            var userResult = AppUser.Create(
                email: request.Email.Value,
                username: request.Username.Value,
                phoneNumber: request.PhoneNumber.Value,
                tckn: request.Tckn.Value,
                firstName: request.FirstName.Value,
                lastName: request.LastName.Value,
                profession: request.Profession?.Value ?? string.Empty,
                birthDate: request.BirthDate.Value
            );

            if (!userResult.IsSuccess)
            {
                _logger.LogWarning("Failed to create user: {Error}", userResult.Error);
                return Result<CreateUserResult>.Failure(userResult.Error!);
            }

            var user = userResult.Value;

            // Set password - SetPassword returns a new instance with hashed password
            user = user.SetPassword(request.Password.Value);

            // Set verification status - UpdateVerificationStatus returns a new instance
            user = user.UpdateVerificationStatus(
                isIdentityVerified: request.IsIdentityVerified,
                isPhoneVerified: request.IsPhoneVerified,
                isEmailVerified: request.IsEmailVerified
            );

            // Set status if provided - UpdateStatus returns a new instance
            if (!string.IsNullOrEmpty(request.Status))
            {
                user = user.UpdateStatus(request.Status);
            }

            // Save user
            var userId = await _userRepository.CreateUserAsync(user);

            // Assign default base_user role (admin can change this later if needed)
            await _userRepository.AssignRoleAsync(userId, "base_user");

            _logger.LogInformation("Admin created new user with ID {UserId}", userId);

            return Result<CreateUserResult>.Success(new CreateUserResult 
            { 
                UserId = userId,
                Email = request.Email.Value,
                Username = request.Username.Value,
                Message = "User created successfully" 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return Result<CreateUserResult>.Failure("An error occurred while creating the user");
        }
    }
} 