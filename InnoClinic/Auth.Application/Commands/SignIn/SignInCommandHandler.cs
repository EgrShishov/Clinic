using Auth.Application.Common.Abstractions;
using InnoClinic.Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Commands.SignIn
{
    public class SignInCommandHandler(
          IMediator mediator,
          IAccountRepository _accountRepository,
          UserManager<Account> manager,
          IEmailSender emailSender,
          ITokenGenerator tokenService
          )
          : IRequestHandler<SignInCommand, ErrorOr<AuthorizationResponse>>
    {
        public async Task<ErrorOr<AuthorizationResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if (!await _accountRepository.EmailExistsAsync(request.Email))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var account = await _accountRepository.GetByEmailAsync(request.Email);

            var isPasswordValid = await manager.CheckPasswordAsync(account, request.Password);
            if (!isPasswordValid)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var accessToken = tokenService.GenerateAccessToken(account);
            var refreshToken = tokenService.GenerateRefreshToken(account);

            var roles = await manager.GetRolesAsync(account);
            var role = roles.Contains("Doctor") ? "Doctor" :
                    roles.Contains("Receptionist") ? "Receptionist" : "Patient";

            return new AuthorizationResponse(accessToken, refreshToken, role);
        }
    }
}
