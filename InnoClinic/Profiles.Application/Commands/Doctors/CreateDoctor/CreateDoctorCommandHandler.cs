public class CreateDoctorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateDoctorCommand, ErrorOr<Doctor>>
{
    public async Task<ErrorOr<Doctor>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor doctor = new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            SpecializationId = request.SpecializationId,
            DateOfBirth = request.DateOfBirth,
            CareerStartYear = request.CareerStartYear,
            OfficeId = request.OfficeId,
            Status = request.Status,
            AccountId = request.AccountId
        };

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var addedDoctor = await unitOfWork.DoctorsRepository.AddDoctorAsync(doctor);
            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return addedDoctor;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken); 
            return Error.Failure("Failed to create doctor");
        }
    }
}
