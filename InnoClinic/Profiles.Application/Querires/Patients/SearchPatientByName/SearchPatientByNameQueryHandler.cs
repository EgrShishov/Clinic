
namespace Profiles.Application.Querires.Patients.SearchPatientByName
{
    public class SearchPatientByNameQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchPatientByNameQuery, ErrorOr<List<Patient>>>
    {
        public async Task<ErrorOr<List<Patient>>> Handle(SearchPatientByNameQuery request, CancellationToken cancellationToken)
        {
            var patient = await unitOfWork.PatientsRepository
                        .SearchPatientByNameAsync(request.FirstName, request.LastName, request.MiddleName);
            if (patient is null)
            {
                return Errors.Patients.NotFound;
            }

            return patient;
        }
    }
}
