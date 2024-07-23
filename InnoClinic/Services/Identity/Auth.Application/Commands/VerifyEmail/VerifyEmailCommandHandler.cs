using Microsoft.AspNetCore.Identity;

public class VerifyEmailCommandHandler(IUnitOfWork unitOfWork, UserManager<Account> manager) 
    : IRequestHandler<VerifyEmailCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            return Errors.Authentication.NotFound;
        }

        var confirmationResult = await manager.ConfirmEmailAsync(account, request.Token);
        if (!confirmationResult.Succeeded)
        {
            return Error.Failure(confirmationResult.Errors.First().Description);
        }

        return Unit.Value;
    }
}