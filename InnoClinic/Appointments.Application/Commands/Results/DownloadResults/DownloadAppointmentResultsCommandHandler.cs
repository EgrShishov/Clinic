public class DownloadAppointmentResultsCommandHandler(IUnitOfWork unitOfWork, IPDFDocumentGenerator documentGenerator)
: IRequestHandler<DownloadAppointmentResultsCommand, ErrorOr<byte[]>>
{
    public async Task<ErrorOr<byte[]>> Handle(DownloadAppointmentResultsCommand request, CancellationToken cancellationToken)
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
            Complaints = results.Complaints,
            DateOfBirth = dateOfBirth,
            Conclusion = results.Conclusion,
            Recommendations = results.Recommendations,
            DoctorName = doctorsName,
            PatientName = patientsName,
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
