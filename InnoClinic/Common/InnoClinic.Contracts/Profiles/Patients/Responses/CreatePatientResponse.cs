public record CreatePatientResponse(
    bool Success,
    bool IsMatchFound,
    string Message,
    PatientProfileResponse MatchedProfile);