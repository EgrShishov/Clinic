public class CreatePatientCommandHandler(IUnitOfWork unitOfWork, IAccountHttpClient accountHttpClient) 
    : IRequestHandler<CreatePatientCommand, ErrorOr<CreatePatientResponse>>
{
    public async Task<ErrorOr<CreatePatientResponse>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var accountInfoResponse = await accountHttpClient.GetAccountInfo(request.AccountId);
        if (accountInfoResponse.IsError)
        {
            return Error.NotFound("Accounts.AccountNotFound", "User account does not exist.");
        }

        var account = accountInfoResponse.Value;

        var existingProfiles = await unitOfWork
            .PatientsRepository
            .SearchPatientByNameAsync(request.FirstName, request.LastName, request.MiddleName);

        if (existingProfiles.Any())
        {
            var matchedProfile = existingProfiles
                .Where(p => !p.IsLinkedToAccount)
                .Select(p => new
                {
                    Profile = p,
                    MatchScore = CalculateMatchScore(p, request)
                })
                .OrderByDescending(p => p.MatchScore)
                .FirstOrDefault(p => p.MatchScore >=13).Profile;

            if (matchedProfile is not null)
            {
                return new CreatePatientResponse
                {
                    Success = false,
                    IsMatchFound = true,
                    Message = "A similar profile has been found",
                    MatchedProfile = new PatientProfileResponse
                    {
                        UserId = matchedProfile.Id,
                        FirstName = matchedProfile.FirstName,
                        LastName = matchedProfile.LastName,
                        MiddleName = matchedProfile.MiddleName,
                        DateOfBirth = matchedProfile.DateOfBirth
                    }
                };
            }
        }

/*        var createPatientAccountResponse = await accountHttpClient.CreateAccount(
           new CreateAccountRequest
           {
               Email = request.Email,
               Password = ,
               Role = nameof(Patient),
               Photo = request.Photo
           });*/

        Patient patient = new Patient
        {
            AccountId = account.AccountId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            IsLinkedToAccount = true
        };
        var newPatient = await unitOfWork.PatientsRepository.AddPatientAsync(patient);
        await unitOfWork.CompleteAsync(cancellationToken);

        return new CreatePatientResponse
        {
            Success = true,
            IsMatchFound = false
        };
    }

    private int CalculateMatchScore(Patient existingProfile, CreatePatientCommand request)
    {
        int score = 0;
        if (existingProfile.FirstName == request.FirstName) score += 5;
        if (existingProfile.LastName == request.LastName) score += 5;
        if (existingProfile.MiddleName == request.MiddleName) score += 5;
        if (existingProfile.DateOfBirth == request.DateOfBirth) score += 3;
        return score;
    }
}
