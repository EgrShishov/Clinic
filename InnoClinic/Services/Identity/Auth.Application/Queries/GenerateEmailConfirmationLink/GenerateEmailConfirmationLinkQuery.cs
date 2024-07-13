public sealed record GenerateEmailConfirmationLinkQuery(Account Account) : IRequest<ErrorOr<string>>
{
}
