public sealed record CreateAccountResponse(
        string PhoneNumber,
        string Email,
        int PhotoId,
        DateTime CreatedAt,
        int CreatedBy,
        string RefreshToken,
        string AccessToken);
