public record VerifyEmailCommand(string Link) : IRequest<ErrorOr<Unit>> 
{
}
