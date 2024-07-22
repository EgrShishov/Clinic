public class ViewReceptonistsProfileQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ViewReceptionistProfileQuery, ErrorOr<ReceptionistProfileInfoResponse>>
{
    public async Task<ErrorOr<ReceptionistProfileInfoResponse>> Handle(ViewReceptionistProfileQuery request, CancellationToken cancellationToken)
    {
        var receptionist = await unitOfWork.ReceptionistsRepository.GetReceptionistByIdAsync(request.ReceptionistId);

        if (receptionist is null)
        {
            return Errors.Receptionists.NotFound;
        }
        //var accountInfo = ;

        return new ReceptionistProfileInfoResponse(
            receptionist.Id,
            //accountInfo.Photo
            receptionist.FirstName,
            receptionist.LastName,
            receptionist.MiddleName,
            //accountInfo.Email,
            //office.City,
            //office.Street,
            //office.HouseNumber,
            //office.OfficeNumber);
    }
}
