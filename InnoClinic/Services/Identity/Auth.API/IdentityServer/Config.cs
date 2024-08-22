using IdentityModel;
using IdentityServer4.Models;

public static class Config
{
    public static JwtSettings JwtSettings { get; set; }
    public static void InitializeJwtSettings(IConfiguration configuration)
    {
        JwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
    }
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "Role",
                UserClaims = new List<string> { "Role" }
            }
        };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope("gateway-api", "Gateway API")
        };

    public static IEnumerable<ApiResource> GetApis() =>
        new List<ApiResource>
        {
            new ApiResource
            {
                Name = "gateway-api",

                ApiSecrets =
                {
                    new Secret(JwtSettings.Secret.Sha256())
                },

                UserClaims = {JwtClaimTypes.Name, JwtClaimTypes.Email, JwtClaimTypes.Id, JwtClaimTypes.Role},
            }
        };

    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret(JwtSettings.Secret.Sha256()) },
                AllowedScopes = { "gateway-api" },
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
            }
        };
}
