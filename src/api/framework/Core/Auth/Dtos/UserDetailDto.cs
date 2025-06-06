namespace FSH.Framework.Core.Auth.Dtos;

public class UserDetailDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = default!;
    public string Username { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string PhoneNumber { get; init; } = string.Empty;
    public string? Profession { get; init; }
    public bool IsEmailVerified { get; init; }
    public bool IsPhoneVerified { get; init; }
    public bool IsIdentityVerified { get; init; }
    public bool IsActive { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = new List<string>();
} 