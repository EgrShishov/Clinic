public class ViewSpecializationsInfoQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<ViewSpecializationsInfoQuery, ErrorOr<Specialization>>
{
    public async Task<ErrorOr<Specialization>> Handle(ViewSpecializationsInfoQuery request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken);
        if (specialization is null)
        {
            return Error.NotFound();
        }

        return specialization;
    }
}
