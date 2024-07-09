public class FilterByOfficeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<FilterByOfficeQuery, ErrorOr<List<Doctor>>>
{
    public async Task<ErrorOr<List<Doctor>>> Handle(FilterByOfficeQuery request, CancellationToken cancellationToken)
    {
        var doctors = await unitOfWork.DoctorsRepository.FilterByOfficeAsync(request.OfficeId, request.PageNumber, request.PageSize);
        if (doctors is null)
        {
            return Errors.Doctors.NotFound;
        }

        return doctors;
    }
}
