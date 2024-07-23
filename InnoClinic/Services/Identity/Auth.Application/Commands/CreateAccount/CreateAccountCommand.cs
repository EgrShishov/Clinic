public sealed record CreateAccountCommand(
    string PhoneNumber,
    string Email,
    string Password,
    string ReenteredPassword,
    string Role,
    int ReceptionistId) : IRequest<ErrorOr<CreateAccountResponse>>
{
}
