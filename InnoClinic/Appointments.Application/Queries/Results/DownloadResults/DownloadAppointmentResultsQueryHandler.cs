public class DownloadAppointmentResultsQueryHandler(IUnitOfWork unitOfWork, IPDFDocumentGenerator documentGenerator)
: IRequestHandler<DownloadAppointmentResultsQuery, ErrorOr<byte[]>>
{
    public async Task<ErrorOr<byte[]>> Handle(DownloadAppointmentResultsQuery request, CancellationToken cancellationToken)
    {
        var results = await unitOfWork.ResultsRepository.GetResultsByIdAsync(request.ResultsId);
        if (results is null)
        {
            return Errors.Results.NotFound;
        }

        DateTime dateOfBirth = DateTime.UtcNow;
        string doctorsName = string.Empty;
        string patientsName = string.Empty;
        string servicesName = string.Empty;
        string specialization = string.Empty;


        var pdfRequest = new GeneratePDFResultsRequest
        {
            Date = results.Date,
            DoctorName = doctorsName,
            PatientName = patientsName,
            DateOfBirth = dateOfBirth,
            Complaints = results.Complaints,
            Conclusion = results.Conclusion,
            Recommendations = results.Recommendations,
            ServiceName = servicesName,
            Specialization = specialization
        };

        var appointmentResultsInPDF = documentGenerator.GenerateAppointmentResults(pdfRequest);
        if (appointmentResultsInPDF is null)
        {
            return Error.Failure("Cannot generate PDF aapointments results");
        }
        return appointmentResultsInPDF;
    }
}
