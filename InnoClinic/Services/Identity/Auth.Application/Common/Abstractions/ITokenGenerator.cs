using System.Security.Claims;

public interface ITokenGenerator
{
    string GenerateAccessToken(Account account);
    string GenerateRefreshToken(Account account);
    ErrorOr<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
}
