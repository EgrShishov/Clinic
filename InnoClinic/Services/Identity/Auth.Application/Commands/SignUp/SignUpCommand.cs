
public sealed record SignUpCommand(
    string PhoneNumber, 
    string Email, 
    string Password, 
    string ReenteredPassword, 
    string Role)
    : IRequest<ErrorOr<AuthorizationResponse>>
{
}
