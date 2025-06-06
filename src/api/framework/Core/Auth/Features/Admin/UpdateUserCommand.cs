using MediatR;
using FSH.Framework.Core.Common.Models;
using FSH.Framework.Core.Auth.Domain.ValueObjects;

namespace FSH.Framework.Core.Auth.Features.Admin;

public record UpdateUserCommand : IRequest<Result<UpdateUserResult>>
{
    public Guid UserId { get; init; }
    public Email Email { get; init; } = default!;
    public Username Username { get; init; } = default!;
    public Name FirstName { get; init; } = default!;
    public Name LastName { get; init; } = default!;
    public PhoneNumber PhoneNumber { get; init; } = default!;
    public Profession? Profession { get; init; }
    public string Status { get; init; } = default!;
    public bool IsIdentityVerified { get; init; }
    public bool IsPhoneVerified { get; init; }
    public bool IsEmailVerified { get; init; }
}

public record UpdateUserResult
{
    public Guid UserId { get; init; }
    public string Message { get; init; } = default!;
} 