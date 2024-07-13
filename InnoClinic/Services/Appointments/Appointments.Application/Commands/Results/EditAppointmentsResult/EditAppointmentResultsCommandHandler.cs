public class EditAppointmentResultsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditAppointmentsResultCommand, ErrorOr<Results>>
{
    public async Task<ErrorOr<Results>> Handle(EditAppointmentsResultCommand request, CancellationToken cancellationToken)
    {
        var results = await unitOfWork.ResultsRepository.GetResultsByIdAsync(request.ResultsId);
        if (results is null)
        {
            return Errors.Results.NotFound;
        }

        results.Complaints = request.Complaints;
        results.Conclusion = request.Conclusion;
        results.Recommendations = request.Recommendations;

        await unitOfWork.ResultsRepository.UpdateResultsAsync(results, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return results;
    }
}
