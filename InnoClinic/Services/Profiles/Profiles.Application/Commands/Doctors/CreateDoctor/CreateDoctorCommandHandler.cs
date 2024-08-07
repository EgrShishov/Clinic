public class CreateDoctorCommandHandler(
    IUnitOfWork unitOfWork, 
    IAccountHttpClient accountHttpClient,
    IFilesHttpClient filesHttpClient,
    IPasswordGenerator passwordGenerator,
    IEmailSender emailService) 
    : IRequestHandler<CreateDoctorCommand, ErrorOr<Doctor>>
{
    public async Task<ErrorOr<Doctor>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var generatedPassword = passwordGenerator.GeneratePassword(15, 5);

        var createDoctorsAccountResponse = await accountHttpClient.CreateAccount(
            new CreateAccountRequest
            {
                Email = request.Email,
                Password = generatedPassword,
                Role = nameof(Doctor),
                PhoneNumber = string.Empty,
                Photo = request.Photo,
                CreatedBy = request.CreatedBy
            });

        if (createDoctorsAccountResponse.IsError) 
        {
            return createDoctorsAccountResponse.FirstError;
        }

        var account = createDoctorsAccountResponse.Value;

        await emailService.SendEmailWithCredentialsAsync(request.Email, generatedPassword);

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
            AccountId = account.AccountId
        };

        var addedDoctor = await unitOfWork.DoctorsRepository.AddDoctorAsync(doctor);
        await unitOfWork.CompleteAsync(cancellationToken);
        return addedDoctor;
    }
}
