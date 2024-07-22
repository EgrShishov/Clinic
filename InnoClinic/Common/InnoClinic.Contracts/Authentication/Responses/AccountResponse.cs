public sealed record AccountResponse(
        string PhoneNumber,
        string Email,
        int PhotoId,
        int CreatedBy,
        DateTime CreatedAt,
        int UpdatedBy,
        DateTime UpdatedAt);
