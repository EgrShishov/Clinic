public record VerifyEmailCommand(int AccountId, string Token) : IRequest<ErrorOr<Unit>>
{
}

