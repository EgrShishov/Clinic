public class CreateSpecializationCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateSpecializationCommand, ErrorOr<Specialization>>
{
    public async Task<ErrorOr<Specialization>> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = new Specialization
        {
            SpecializationName = request.SpecializatioName,
            IsActive = request.IsActive
        };
        var newSpecialization = await unitOfWork.Specializations.AddSpecializationAsync(specialization, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return newSpecialization;
    }
}