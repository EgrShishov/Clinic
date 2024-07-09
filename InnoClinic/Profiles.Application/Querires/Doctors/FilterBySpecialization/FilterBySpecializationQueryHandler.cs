public class FilterBySpecializationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<FilterBySpecializationQuery, ErrorOr<List<Doctor>>>
{
    public async Task<ErrorOr<List<Doctor>>> Handle(FilterBySpecializationQuery request, CancellationToken cancellationToken)
    {
        var doctors = await unitOfWork.DoctorsRepository.FilterBySpecializationAsync(request.SpecializationId, request.PageNumber, request.PageSize);
        // checking specializationId
        return doctors;
    }
}
