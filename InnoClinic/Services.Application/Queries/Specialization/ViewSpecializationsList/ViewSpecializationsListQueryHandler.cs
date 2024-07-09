public class ViewSpecializationsListQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<ViewSpecializationsListQuery, ErrorOr<List<Specialization>>>
{
    public async Task<ErrorOr<List<Specialization>>> Handle(ViewSpecializationsListQuery request, CancellationToken cancellationToken)
    {
        var specializations = await unitOfWork.Specializations.GetAllAsync(cancellationToken);
        if (specializations is null)
        {
            return Error.NotFound();
        }

        return specializations.ToList();
    }
}
