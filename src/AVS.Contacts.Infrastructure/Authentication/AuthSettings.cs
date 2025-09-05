namespace AVS.Contacts.Infrastructure.Authentication;

public class AuthSettings
{
    public string Instance { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string SignUpSignInPolicyId { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = Array.Empty<string>();
}