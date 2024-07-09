public sealed record ServiceInfoResponse(
    int Id,
    string ServiceCategory,
    string ServiceName,
    Decimal ServicePrice,
    bool IsActive);