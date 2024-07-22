public class ViewAllReceptionistsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ViewAllReceptionistsQuery, ErrorOr<List<ReceptionistListResponse>>>
{
    public async Task<ErrorOr<List<ReceptionistListResponse>>> Handle(ViewAllReceptionistsQuery request, CancellationToken cancellationToken)
    {
        var receptionists = await unitOfWork.ReceptionistsRepository.GetListReceptionistAsync(request.PageNumber, request.PageSize);
            
        if (receptionists is null)
        {
            return Errors.Receptionists.NotFound;
        }

        var receptionistList = new List<ReceptionistListResponse>();
        foreach (var receptionist in receptionists)
        {
            receptionistList.Add(new ReceptionistListResponse
            {
                Id = receptionist.Id,
                //accountInfo.Photo
                receptionist.FirstName,
                receptionist.LastName,
                receptionist.MiddleName,
                //accountInfo.Email,
                //office.City,
                //office.Street,
                //office.HouseNumber,
                //office.OfficeNumber);
            });
        }

        return receptionistList;
    }
}
