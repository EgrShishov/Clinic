public class UpdateSpecializationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateSpecializationCommand, ErrorOr<Specialization>>
{
    public async Task<ErrorOr<Specialization>> Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id);
        if (specialization is null) 
        {
            return Error.NotFound("");
        }

        specialization.SpecializationName = request.SpecializationName;
        specialization.IsActive = request.IsActive;

        await unitOfWork.Specializations.UpdateSpecializationAsync(specialization);
        return specialization;
    }
}
