public record UpdateAccountResponse(
    string PhoneNumber,
    string Email,
    int PhotoId,
    DateTime UpdatedAt,
    int UpdatedBy,
    string RefreshToken,
    string AccessToken,
    string Role);