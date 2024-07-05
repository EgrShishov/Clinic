using Auth.Application.Common.Abstractions;
using Auth.Application.Queries;
using InnoClinic.Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Commands.SignUp
{
    public class SignUpCommandHandler(
          IMediator mediator,
          IAccountRepository _accountRepository,
          IEmailSender emailSender,
          UserManager<Account> manager,
          ITokenGenerator tokenGenerator
          )
          : IRequestHandler<SignUpCommand, ErrorOr<AuthorizationResponse>>
    {
        public async Task<ErrorOr<AuthorizationResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _accountRepository.EmailExistsAsync(request.Email))
            {
                return Errors.Authentication.DuplicateEmail;
            }

            if (request.Password != request.ReenteredPassword)
            {
                return Errors.Authentication.PasswordNotCoincide;
            }

            var account = new Account
            {
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
            };

            var addedAccount = await _accountRepository.AddAsync(account);
            await manager.AddToRoleAsync(account, request.Role);

            var confirmationLink = await mediator.Send(new GenerateEmailConfirmationLinkQuery(account));
            await emailSender.SendEmailAsync(request.Email, "Confirm your email", confirmationLink.Value);

            var accessToken = tokenGenerator.GenerateAccessToken(account);
            var refreshToken = tokenGenerator.GenerateRefreshToken(account);

            var roles = await manager.GetRolesAsync(account);
            var role = roles.Contains("Doctor") ? "Doctor" :
                    roles.Contains("Receptionist") ? "Receptionist" : "Patient";

            return new AuthorizationResponse(accessToken, refreshToken, role);
        }
    }
}
