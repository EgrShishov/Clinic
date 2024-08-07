
public sealed record SignUpCommand(
    string Email, 
    string Password,
    string PhoneNumber,
    string ReenteredPassword,
    int CreatedBy,
    string Role)
    : IRequest<ErrorOr<AuthorizationResponse>>
{
}
