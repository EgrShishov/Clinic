
namespace Profiles.Application.Querires.Doctors.ViewById
{
    public class ViewByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewByIdQuery, ErrorOr<Doctor>>
    {
        public async Task<ErrorOr<Doctor>> Handle(ViewByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await unitOfWork.DoctorsRepository.GetDoctorByIdAsync(request.DoctorId);
            if (doctor is null)
            {
                return Errors.Doctors.NotFound;
            }
            return doctor;
        }
    }
}
