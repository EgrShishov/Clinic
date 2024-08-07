public sealed record AccountResponse(
        int AccountId,
        string PhoneNumber,
        string Email,
        string PhotoUrl,
        int CreatedBy,
        DateTime CreatedAt,
        int UpdatedBy,
        DateTime UpdatedAt);
