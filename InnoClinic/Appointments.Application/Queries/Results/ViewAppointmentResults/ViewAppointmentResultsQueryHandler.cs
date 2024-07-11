public class ViewAppointmentResultsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewAppointmentResultQuery, ErrorOr<Results>>
{
    public async Task<ErrorOr<Results>> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken)
    {
        var results = await unitOfWork.ResultsRepository.GetResultsByIdAsync(request.ResultsId);

        if (results == null)
        {
            return Errors.Results.NotFound;
        }
        return results;
    }
}
