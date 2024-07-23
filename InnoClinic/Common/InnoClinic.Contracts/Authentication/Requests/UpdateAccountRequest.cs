public record UpdateAccountRequest(
    int AccountId,
    string OldPassword,
    string NewPassword,
    string ReenteredNewPassword,
    string PhoneNumber, 
    string Email, 
    string Password, 
    string ReenteredPassword);