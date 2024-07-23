using Microsoft.AspNetCore.Identity;

public class CreateAccountCommandHandler(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        UserManager<Account> manager,
        ITokenGenerator tokenGenerator
        ) : IRequestHandler<CreateAccountCommand, ErrorOr<CreateAccountResponse>>
{
    public async Task<ErrorOr<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
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
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.ReceptionistId
        };

        var addedAccount = await unitOfWork.AccountRepository.AddAsync(account);
        await manager.AddToRoleAsync(account, request.Role);

        var confirmationLink = await mediator.Send(new GenerateEmailConfirmationLinkQuery(account));
        await emailSender.SendEmailAsync(request.Email, "Confirm your email", confirmationLink.Value);

        var accessToken = tokenGenerator.GenerateAccessToken(account);
        var refreshToken = tokenGenerator.GenerateRefreshToken(account);

        account.RefreshToken = refreshToken;
        await manager.UpdateAsync(account);

        await manager.AddToRoleAsync(account, request.Role);

        return new CreateAccountResponse(
            addedAccount.PhoneNumber,
            addedAccount.Email,
            addedAccount.PhotoId,
            addedAccount.CreatedAt,
            addedAccount.CreatedBy,
            accessToken, 
            refreshToken);
    }
}
