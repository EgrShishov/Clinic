using Microsoft.AspNetCore.Identity;

public class SignInCommandHandler(
    IMediator mediator,
    IUnitOfWork unitOfWork,
    UserManager<Account> manager,
    IEmailSender emailSender,
    ITokenGenerator tokenService
    )
    : IRequestHandler<SignInCommand, ErrorOr<AuthorizationResponse>>
{
    public async Task<ErrorOr<AuthorizationResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.AccountRepository.EmailExistsAsync(request.Email))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var account = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

        var isPasswordValid = await manager.CheckPasswordAsync(account, request.Password);
        if (!isPasswordValid)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var accessToken = tokenService.GenerateAccessToken(account);
        var refreshToken = tokenService.GenerateRefreshToken(account);

        account.RefreshToken = refreshToken;
        await manager.UpdateAsync(account);

        var roles = await manager.GetRolesAsync(account);
        var role = roles.Contains("Doctor") ? "Doctor" :
                roles.Contains("Receptionist") ? "Receptionist" : "Patient";

        return new AuthorizationResponse(accessToken, refreshToken, role);
    }
}
