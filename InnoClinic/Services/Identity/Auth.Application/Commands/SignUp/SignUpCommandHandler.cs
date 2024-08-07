using Microsoft.AspNetCore.Identity;

public class SignUpCommandHandler(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        UserManager<Account> manager,
        ITokenGenerator tokenGenerator
        )
        : IRequestHandler<SignUpCommand, ErrorOr<AuthorizationResponse>>
{
    public async Task<ErrorOr<AuthorizationResponse>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.AccountRepository.EmailExistsAsync(request.Email))
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
            CreatedBy = request.CreatedBy,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            PhotoUrl = ""
        };

        var identityResult = await manager.CreateAsync(account, request.Password);
        if (!identityResult.Succeeded)
        {
            return Errors.Authentication.GenerationFailed;
        }

        await manager.AddToRoleAsync(account, request.Role);

        var confirmationLink = await mediator.Send(new GenerateEmailConfirmationLinkQuery(account.Id));
        var emailTemplate = new EmailTemplates.EmailConfirmationLinkTemplate
        {
            ConfirmationLink = confirmationLink.Value
        };

        await emailSender.SendEmailAsync(request.Email, "Confirm your email", emailTemplate.GetContent());

        var accessToken = tokenGenerator.GenerateAccessToken(account);
        var refreshToken = tokenGenerator.GenerateRefreshToken(account);

        account.RefreshToken = refreshToken;
        await manager.UpdateAsync(account);

        var roles = await manager.GetRolesAsync(account);
        var role = roles.Contains("Doctor") ? "Doctor" :
                roles.Contains("Receptionist") ? "Receptionist" : "Patient";

        return new AuthorizationResponse(accessToken, refreshToken, role);
    }
}
