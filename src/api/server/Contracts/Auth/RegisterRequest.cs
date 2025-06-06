using System.ComponentModel.DataAnnotations;

namespace FSH.Starter.WebApi.Contracts.Auth;

/// <summary>
/// Registration request DTO for the presentation layer.
/// Follows Clean Architecture by separating external contracts from domain models.
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    /// Email address
    /// </summary>
    [Required(ErrorMessage = "Email gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    public string Email { get; init; } = default!;

    /// <summary>
    /// Username
    /// </summary>
    [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter arasında olmalıdır")]
    public string Username { get; init; } = default!;

    /// <summary>
    /// Phone number (Turkish format)
    /// </summary>
    [Required(ErrorMessage = "Telefon numarası gereklidir")]
    [RegularExpression(@"^5\d{2}(\s?\d{3}\s?\d{2}\s?\d{2}|\d{7})$", ErrorMessage = "Telefon numarası 5XXXXXXXXX veya 5XX XXX XX XX formatında olmalıdır")]
    public string PhoneNumber { get; init; } = default!;

    /// <summary>
    /// Turkish Citizen ID (TC Kimlik No)
    /// </summary>
    [Required(ErrorMessage = "TC Kimlik No gereklidir")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik No sadece rakamlardan oluşmalıdır")]
    public string Tckn { get; init; } = default!;

    /// <summary>
    /// Password
    /// </summary>
    [Required(ErrorMessage = "Şifre gereklidir")]
    [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalıdır")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_\-\.#])[A-Za-z\d@$!%*?&_\-\.#]{8,}$", 
        ErrorMessage = "Şifre en az 1 büyük harf, 1 küçük harf, 1 rakam ve 1 özel karakter içermelidir")]
    public string Password { get; init; } = default!;

    /// <summary>
    /// First name
    /// </summary>
    [Required(ErrorMessage = "Ad gereklidir")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 karakter arasında olmalıdır")]
    public string FirstName { get; init; } = default!;

    /// <summary>
    /// Last name
    /// </summary>
    [Required(ErrorMessage = "Soyad gereklidir")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 karakter arasında olmalıdır")]
    public string LastName { get; init; } = default!;

    /// <summary>
    /// Profession (optional)
    /// </summary>
    [StringLength(100, ErrorMessage = "Meslek en fazla 100 karakter olabilir")]
    public string? Profession { get; init; }

    /// <summary>
    /// Birth date (optional)
    /// </summary>
    public DateTime? BirthDate { get; init; }
} 