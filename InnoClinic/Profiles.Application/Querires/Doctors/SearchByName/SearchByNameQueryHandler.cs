
namespace Profiles.Application.Querires.Doctors.SearchByName
{
    public class SearchByNameQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchByNameQuery, ErrorOr<List<Doctor>>>
    {
        public async Task<ErrorOr<List<Doctor>>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            var doctors = await unitOfWork.DoctorsRepository.SearchByNameAsync(request.FirstName, request.LastName, request.MiddleName);
            if (doctors is null)
            {
                return Errors.Doctors.NotFound;
            }
            return doctors;
        }
    }

}
