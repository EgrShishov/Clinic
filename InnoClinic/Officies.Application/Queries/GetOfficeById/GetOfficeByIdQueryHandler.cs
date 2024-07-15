public class GetOfficeByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOfficeByIdQuery, Office>
{
    public async Task<Office> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.Id, unitOfWork.Session);
    }
}
