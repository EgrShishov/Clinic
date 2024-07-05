namespace Profiles.Application.Commands.Doctors.UpdateDoctor
{
    public class UpdateDoctorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateDoctorCommand, ErrorOr<Doctor>>
    {
        public async Task<ErrorOr<Doctor>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var doctor = await unitOfWork.DoctorsRepository.GetDoctorByIdAsync(request.DoctorId);
                if (doctor == null)
                {
                    return Errors.Doctors.NotFound;
                }

                doctor.FirstName = request.FirstName;
                doctor.LastName = request.LastName;
                doctor.MiddleName = request.MiddleName;
                doctor.DateOfBirth = request.DateOfBirth;
                doctor.SpecializationId = request.SpecializationId;
                doctor.OfficeId = request.OfficeId;
                doctor.CareerStartYear = request.CareerStartYear;
                doctor.Status = request.Status;

                await unitOfWork.DoctorsRepository.UpdateDoctorAsync(doctor);
                await unitOfWork.CompleteAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return doctor;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Error.Failure("Failed to update doctor", ex.Message);
            }
        }
    }
}
