public record CreateServiceRequest(
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    bool IsActive);
