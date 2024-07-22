public sealed record CreateServiceCommand(
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    int SpecializationId,
    bool IsActive) : IRequest<ErrorOr<Service>>
{
}
