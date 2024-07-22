public sealed record CreateServiceCommand(
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    bool IsActive,
    int SpecializationId) : IRequest<ErrorOr<ServiceInfoResponse>>
{
}
