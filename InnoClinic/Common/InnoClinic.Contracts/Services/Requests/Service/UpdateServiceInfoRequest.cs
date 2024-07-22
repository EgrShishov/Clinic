public record UpdateServiceInfoRequest(
    int ServiceId,
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    bool IsActive);
