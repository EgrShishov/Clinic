public sealed record UpdateServiceCommand(
    int Id, 
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    int SpecializationId,
    bool IsActive) : IRequest<ErrorOr<Service>>
{
}
