public record VerifyEmailCommand(string AccountId, string Link) : IRequest<ErrorOr<Unit>> 
{
}
