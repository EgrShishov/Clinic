public class ChangeSpecializationStatusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ChangeSpecializationStatusCommand, ErrorOr<Specialization>>
{
    public async Task<ErrorOr<Specialization>> Handle(ChangeSpecializationStatusCommand request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken);
        if (specialization is null)
        {
            return Error.NotFound("");
        }

        specialization.IsActive = request.IsActive;
        await unitOfWork.Specializations.UpdateSpecializationAsync(specialization, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        return specialization;
    }
}
