public record SignUpRequest(
    string Email,
    string Password, 
    string ReenteredPassword, 
    string PhoneNumber, 
    string Role, 
    int CreatedBy);
