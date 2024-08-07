public class ViewDoctorsQueryHandler(IUnitOfWork unitOfWork, IAccountHttpClient accountHttpClient) 
    : IRequestHandler<ViewDoctorsQuery, ErrorOr<List<DoctorListResponse>>>
{
    public async Task<ErrorOr<List<DoctorListResponse>>> Handle(ViewDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await unitOfWork.DoctorsRepository.GetListDoctorsAsync(request.PageNumber, request.PageSize);
        if (doctors is null)
        {
            return Errors.Doctors.NotFound;
        }

        var allDoctors = new List<DoctorListResponse>();

        foreach (var doctor in doctors)
        {
            string photoUrl = string.Empty;

            var accountInfoResponse = await accountHttpClient.GetAccountInfo(doctor.AccountId);
            if (accountInfoResponse.IsError)
            {
                return accountInfoResponse.FirstError;
            }
            var account = accountInfoResponse.Value;

            var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(doctor.OfficeId);
            if (office is null)
            {
                return Error.NotFound();
            }

            string specialization = string.Empty;

            allDoctors.Add(new DoctorListResponse
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                MiddleName = doctor.MiddleName,
                PhotoUrl = account.PhotoUrl,
                Experience = DateTime.UtcNow.Year - doctor.CareerStartYear + 1,
                Specialization = specialization,
                City = office.City,
                HouseNumber = office.HouseNumber,
                OfficeNumber = office.OfficeNumber,
                Street = office.Street
            });
        }

        return allDoctors;
    }
}
