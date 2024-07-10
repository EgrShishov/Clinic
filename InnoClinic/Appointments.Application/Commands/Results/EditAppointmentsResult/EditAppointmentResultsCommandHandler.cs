public class EditAppointmentResultsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditAppointmentsResultCommand, ErrorOr<Results>>
{
    public async Task<ErrorOr<Results>> Handle(EditAppointmentsResultCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
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
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return results;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
