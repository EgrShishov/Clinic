public sealed record UpdateAccountCommand(
    int AccountId,
    int ReceptionistId,
    string PhoneNumber,
    string Email,
    string NewPassword,
    string ReenteredNewPassword,
    string OldPassword) : IRequest<ErrorOr<UpdateAccountResponse>>
{
}
