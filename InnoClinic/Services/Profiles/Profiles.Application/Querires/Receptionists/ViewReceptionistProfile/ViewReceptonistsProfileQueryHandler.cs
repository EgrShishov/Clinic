public class ViewReceptonistsProfileQueryHandler(IUnitOfWork unitOfWork, IAccountHttpClient accountHttpClient)
    : IRequestHandler<ViewReceptionistProfileQuery, ErrorOr<ReceptionistProfileInfoResponse>>
{
    public async Task<ErrorOr<ReceptionistProfileInfoResponse>> Handle(ViewReceptionistProfileQuery request, CancellationToken cancellationToken)
    {
        var receptionist = await unitOfWork.ReceptionistsRepository.GetReceptionistByIdAsync(request.ReceptionistId);
        if (receptionist is null)
        {
            return Errors.Receptionists.NotFound;
        }

        var accountResponse = await accountHttpClient.GetAccountInfo(receptionist.AccountId);
        if (accountResponse.IsError)
        {
            return accountResponse.FirstError;
        }

        var account = accountResponse.Value;

        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(receptionist.OfficeId);
        if (office is null)
        {
            return Error.NotFound();
        }

        return new ReceptionistProfileInfoResponse
        {
            Id = receptionist.Id,
            PhotoUrl = account.PhotoUrl,
            FirstName = receptionist.FirstName,
            LastName = receptionist.LastName,
            MiddleName = receptionist.MiddleName,
            Email = account.Email,
            City = office.City,
            Street = office.Street,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber
        };
    }
}
