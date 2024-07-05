
namespace Profiles.Application.Commands.Doctors.DeleteDoctor
{
    public class DeleteDoctorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteDoctorCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var doctor = await unitOfWork.DoctorsRepository.GetDoctorByIdAsync(request.DoctorId);
                if (doctor == null)
                {
                    return Errors.Doctors.NotFound;
                }

                await unitOfWork.DoctorsRepository.DeleteDoctorAsync(doctor.Id);
                await unitOfWork.CompleteAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Error.Failure("Failed to delete doctor", ex.Message);
            }
        }
    }

}
