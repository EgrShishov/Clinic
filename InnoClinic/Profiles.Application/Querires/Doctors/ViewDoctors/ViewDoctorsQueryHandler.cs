public class ViewDoctorsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewDoctorsQuery, ErrorOr<List<Doctor>>>
{
    public async Task<ErrorOr<List<Doctor>>> Handle(ViewDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await unitOfWork.DoctorsRepository.GetListDoctorsAsync(request.PageNumber, request.PageSize);
        if (doctors is null)
        {
            return Errors.Doctors.NotFound;
        }
        return doctors;
    }
}
