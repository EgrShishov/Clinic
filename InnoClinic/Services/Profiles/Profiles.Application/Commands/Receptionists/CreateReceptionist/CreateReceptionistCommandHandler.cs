public class CreateReceptionistCommandHandler(
    IUnitOfWork unitOfWork, 
    IPasswordGenerator passwordGenerator,
    IEmailSender emailService, 
    IAccountService accountService) 
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
            var addedAccount = await accountService.CreateAccountAsync(new CreateAccountRequest("", request.Email, password, password));

            await emailService.SendEmailWithCredentialsAsync(request.Email, password);

            var newReceptionist = await unitOfWork.ReceptionistsRepository.AddReceptionistAsync(receptionist);

            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new CreateReceptionistProfileResponse(
                newReceptionist.Id,
                newReceptionist.AccountId,
                request.Email);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
