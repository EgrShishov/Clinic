using Microsoft.AspNetCore.Identity;

public class VerifyEmailCommandHandler(IAccountRepository repository, UserManager<Account> manager) 
    : IRequestHandler<VerifyEmailCommand, ErrorOr<Unit>>
{
    public Task<ErrorOr<Unit>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}