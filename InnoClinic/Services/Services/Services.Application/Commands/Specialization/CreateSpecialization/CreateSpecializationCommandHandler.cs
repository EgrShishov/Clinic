public class CreateSpecializationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSpecializationCommand, ErrorOr<SpecializationResponse>>
{
    public async Task<ErrorOr<SpecializationResponse>> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = new Specialization
        {
            SpecializationName = request.SpecializatioName,
            IsActive = request.IsActive
        };
        var newSpecialization = await unitOfWork.Specializations.AddSpecializationAsync(specialization, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return new SpecializationResponse(specialization.Id, specialization.SpecializationName, specialization.IsActive);
    }
}