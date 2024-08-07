using Microsoft.AspNetCore.Identity;

public class VerifyEmailCommandHandler(UserManager<Account> manager) 
    : IRequestHandler<VerifyEmailCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var account = await manager.FindByIdAsync(request.AccountId);
        if (account is null)
        {
            return Errors.Authentication.NotFound;
        }

        var identityResult = await manager.ConfirmEmailAsync(account, request.Link);
        if (!identityResult.Succeeded)
        {
            return Errors.Authentication.InvalidToken;
        }

        return Unit.Value;
    }
}