
public sealed record SignUpCommand(string Email, string Password, string ReenteredPassword, string Role)
    : IRequest<ErrorOr<AuthorizationResponse>>
{
}
