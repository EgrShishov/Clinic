public class CreateReceptionistCommandHandler(
    IUnitOfWork unitOfWork, 
    IPasswordGenerator passwordGenerator,
    IEmailSender emailService, 
    IAccountHttpClient accountHttpClient) 
    : IRequestHandler<CreateReceptionistCommand, ErrorOr<CreateReceptionistProfileResponse>>
{
    public async Task<ErrorOr<CreateReceptionistProfileResponse>> Handle(CreateReceptionistCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            Receptionist receptionist = new Receptionist
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                OfficeId = request.OfficeId
            };

            var password = passwordGenerator.GeneratePassword(15, 5);
            var addedAccount = await accountHttpClient.CreateAccount(
                new CreateAccountRequest
                {
                    Password = password,
                    Email = request.Email,
                    Photo = request.Photo,
                    Role = nameof(Patient)
                });

            await emailService.SendEmailWithCredentialsAsync(request.Email, password);

            var newReceptionist = await unitOfWork.ReceptionistsRepository.AddReceptionistAsync(receptionist);

            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new CreateReceptionistProfileResponse
            {
                AccountId = newReceptionist.AccountId,
                Email = request.Email,
                ReceptionistId = newReceptionist.Id
            };
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
