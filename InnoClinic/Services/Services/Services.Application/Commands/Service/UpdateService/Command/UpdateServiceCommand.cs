public sealed record UpdateServiceCommand(
    int Id, 
    int ServiceCategoryId,
    string ServiceName,
    Decimal ServicePrice,
    bool IsActive) : IRequest<ErrorOr<ServiceInfoResponse>>
{
}
