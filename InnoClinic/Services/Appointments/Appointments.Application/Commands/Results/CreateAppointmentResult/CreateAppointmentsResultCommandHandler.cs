public class CreateAppointmentsResultCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentsResultCommand, ErrorOr<Results>>
{
    public async Task<ErrorOr<Results>> Handle(CreateAppointmentsResultCommand request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(request.ServiceId);
        if (service is null)
        {
            return Error.NotFound();
        }

        var appointmetsResult = new Results
        {
            Complaints = request.Complaints,
            Conclusion = request.Conclusion,
            Date = request.AppointmentDate.Value,
            Recommendations = request.Recommendations
        };

        var newResults = await unitOfWork.ResultsRepository.AddResultsAsync(appointmetsResult, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newResults;
    }
}