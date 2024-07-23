﻿using Microsoft.AspNetCore.Identity;

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
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var addedAccount = await unitOfWork.AccountRepository.AddAsync(account);
        var roleResult = await manager.AddToRoleAsync(account, request.Role);
        if (!roleResult.Succeeded)
        {
            return Error.Failure(roleResult.Errors.First().Description);
        }

        var confirmationLink = await mediator.Send(new GenerateEmailConfirmationLinkQuery(account));
        await emailSender.SendEmailAsync(request.Email, "Confirm your email", confirmationLink.Value);

        var accessToken = tokenGenerator.GenerateAccessToken(account);
        var refreshToken = tokenGenerator.GenerateRefreshToken(account);

        account.RefreshToken = refreshToken;
        var updatingAccountResult = await manager.UpdateAsync(account);
        if (!updatingAccountResult.Succeeded)
        {
            return Error.Failure(updatingAccountResult.Errors.First().Description);
        }

        return new AuthorizationResponse(accessToken, refreshToken, request.Role);
    }
}
