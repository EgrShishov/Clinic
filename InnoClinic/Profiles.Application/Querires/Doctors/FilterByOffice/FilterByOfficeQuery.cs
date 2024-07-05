
namespace Profiles.Application.Querires.Doctors.FilterByOffice
{
    public record FilterByOfficeQuery(int OfficeId, int PageNumber, int PageSize) : IRequest<ErrorOr<List<Doctor>>>;

}
