namespace Auth.Application.Queries.GenerateEmailConfirmationLink
{
    public sealed record GenerateEmailConfirmationLinkQuery(Account Account) : IRequest<ErrorOr<string>>
    {
    }
}
