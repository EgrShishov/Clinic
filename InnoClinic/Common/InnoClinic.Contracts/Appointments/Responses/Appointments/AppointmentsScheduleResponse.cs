public record AppointmentsScheduleResponse(
    DateTime Time,
    string PatientFullName,
    string PatientProfileLink,
    string ServiceName,
    string ApprovalStatus,
    string MedicalResultsLink);
