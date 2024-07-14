using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

public class RefreshTokenCommandHandler(UserManager<Account> userManager, ITokenGenerator tokenGenerator)
    : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResponse>>
{
    public async Task<ErrorOr<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = tokenGenerator.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
        {
            return Errors.Authentication.InvalidToken;
        }

        var email = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        if (email == null)
        {
            return Errors.Authentication.InvalidToken;
        }

        var account = await userManager.FindByEmailAsync(email);
        if (account == null || !string.IsNullOrEmpty(account.RefreshToken))
        {
            return Errors.Authentication.InvalidToken;
        }

        var newAccessToken = tokenGenerator.GenerateAccessToken(account);
        var newRefreshToken = tokenGenerator.GenerateRefreshToken(account);

        account.RefreshToken = newRefreshToken;
        await userManager.UpdateAsync(account);

        return new RefreshTokenResponse(newAccessToken, newRefreshToken);
    }
}
