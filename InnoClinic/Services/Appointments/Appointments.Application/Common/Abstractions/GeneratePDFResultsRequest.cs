public sealed record GeneratePDFResultsRequest(
    DateTime Date,
    string PatientName,
    DateTime DateOfBirth,
    string DoctorName,
    string Specialization,
    string ServiceName,
    string Complaints,
    string Conclusion,
    string Recommendations);
