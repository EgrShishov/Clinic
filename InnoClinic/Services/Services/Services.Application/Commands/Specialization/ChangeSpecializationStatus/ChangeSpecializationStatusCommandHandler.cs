public class ChangeSpecializationStatusCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ChangeSpecializationStatusCommand, ErrorOr<SpecializationResponse>>
{
    public async Task<ErrorOr<SpecializationResponse>> Handle(ChangeSpecializationStatusCommand request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken);
        if (specialization is null)
        {
            return Error.NotFound("");
        }

        specialization.IsActive = request.IsActive;
        await unitOfWork.Specializations.UpdateSpecializationAsync(specialization, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return new SpecializationResponse(specialization.Id, specialization.SpecializationName, specialization.IsActive);
    }
}
