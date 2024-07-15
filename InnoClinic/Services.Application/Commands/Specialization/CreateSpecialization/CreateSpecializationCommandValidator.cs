public class CreateSpecializationCommandValidator : AbstractValidator<CreateSpecializationCommand>
{
    public CreateSpecializationCommandValidator()
    {
        RuleFor(v => v.SpecializatioName)
            .NotEmpty().WithMessage("Specialization name is required.");
    }
}
