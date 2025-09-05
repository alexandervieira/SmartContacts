namespace AVS.Contacts.Contracts.Services;

public interface IAuthService
{
    Task<AuthResult> SignInAsync(CancellationToken ct = default);
    Task SignOutAsync(CancellationToken ct = default);
    Task<string?> GetAccessTokenAsync(CancellationToken ct = default);
    Task<bool> IsAuthenticatedAsync(CancellationToken ct = default);
}

public record AuthResult(bool IsSuccess, string? AccessToken = null, string? Error = null);