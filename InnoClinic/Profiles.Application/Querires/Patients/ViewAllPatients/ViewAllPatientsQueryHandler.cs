
namespace Profiles.Application.Querires.Patients.ViewAllPatients
{
    public class ViewAllPatientsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewAllPatientsQuery, ErrorOr<List<Patient>>>
    {
        public async Task<ErrorOr<List<Patient>>> Handle(ViewAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await unitOfWork.PatientsRepository.GetListPatientsAsync(request.PageNumber, request.PageSize);
            if (patients is null)
            {
                return Errors.Patients.NotFound;
            }
            return patients;
        }
    }
}
