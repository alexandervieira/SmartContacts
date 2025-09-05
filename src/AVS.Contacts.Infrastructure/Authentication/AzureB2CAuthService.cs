using AVS.Contacts.Contracts.Services;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace AVS.Contacts.Infrastructure.Authentication;

public class AzureB2CAuthService : IAuthService
{
    private readonly IPublicClientApplication _app;
    private readonly AuthSettings _settings;

    public AzureB2CAuthService(IOptions<AuthSettings> settings)
    {
        _settings = settings.Value;
        _app = PublicClientApplicationBuilder
            .Create(_settings.ClientId)
            .WithB2CAuthority($"{_settings.Instance}/tfp/{_settings.TenantId}/{_settings.SignUpSignInPolicyId}")
            .WithRedirectUri("msal" + _settings.ClientId + "://auth")
            .Build();
    }

    public async Task<AuthResult> SignInAsync(CancellationToken ct = default)
    {
        try
        {
            var accounts = await _app.GetAccountsAsync();
            var result = await _app.AcquireTokenSilent(_settings.Scopes, accounts.FirstOrDefault())
                .ExecuteAsync(ct);

            return new AuthResult(true, result.AccessToken);
        }
        catch (MsalUiRequiredException)
        {
            try
            {
                var result = await _app.AcquireTokenInteractive(_settings.Scopes)
                    .ExecuteAsync(ct);
                return new AuthResult(true, result.AccessToken);
            }
            catch (Exception ex)
            {
                return new AuthResult(false, Error: ex.Message);
            }
        }
    }

    public async Task SignOutAsync(CancellationToken ct = default)
    {
        var accounts = await _app.GetAccountsAsync();
        foreach (var account in accounts)
        {
            await _app.RemoveAsync(account);
        }
    }

    public async Task<string?> GetAccessTokenAsync(CancellationToken ct = default)
    {
        try
        {
            var accounts = await _app.GetAccountsAsync();
            var result = await _app.AcquireTokenSilent(_settings.Scopes, accounts.FirstOrDefault())
                .ExecuteAsync(ct);
            return result.AccessToken;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsAuthenticatedAsync(CancellationToken ct = default)
    {
        var accounts = await _app.GetAccountsAsync();
        return accounts.Any();
    }
}