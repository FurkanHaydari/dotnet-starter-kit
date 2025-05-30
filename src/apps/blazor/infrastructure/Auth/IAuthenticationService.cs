using FSH.Starter.Blazor.Infrastructure.Api;

namespace FSH.Starter.Blazor.Infrastructure.Auth;

public interface IAuthenticationService
{

    void NavigateToExternalLogin(string returnUrl);

    Task<bool> LoginAsync(TokenGenerationCommand request);

    Task LogoutAsync();

    Task ReLoginAsync(string returnUrl);
}