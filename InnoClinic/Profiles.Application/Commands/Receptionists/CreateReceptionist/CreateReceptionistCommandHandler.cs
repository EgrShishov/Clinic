public class CreateReceptionistCommandHandler(IUnitOfWork unitOfWork, IEmailSender emailService) 
    : IRequestHandler<CreateReceptionistCommand, ErrorOr<Receptionist>>
{
    public async Task<ErrorOr<Receptionist>> Handle(CreateReceptionistCommand request, CancellationToken cancellationToken)
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

            /* receptionist.Password = GeneratePassword(); throw identity framework? and also email verify
                receptionist.IsEmailVerified = true;*/
                
            // await emailService.SendEmailWithCredentialsAsync(receptionist.Email, receptionist.Password);

            var newReceptionist = await unitOfWork.ReceptionistsRepository.AddReceptionistAsync(receptionist);
            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return newReceptionist;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
