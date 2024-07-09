public class GetAccountByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAccountByIdQuery, ErrorOr<Account>>
{
    public async Task<ErrorOr<Account>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountRepository.GetByIdAsync(request.id);
        if (account is null)
        {
            return Errors.Authentication.NotFound;
        }

        return account;
    }
}
