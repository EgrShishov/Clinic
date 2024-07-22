public sealed record GetAccountByIdQuery(int id) : IRequest<ErrorOr<Account>>
{
}
