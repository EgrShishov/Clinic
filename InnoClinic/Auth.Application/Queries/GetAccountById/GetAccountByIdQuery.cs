
namespace Auth.Application.Queries.GetAccountById
{
    public sealed record GetAccountByIdQuery(int id) : IRequest<ErrorOr<Account>>
    {
    }
}
