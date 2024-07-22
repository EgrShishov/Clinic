﻿public class EditAppointmentsResultsValidator : AbstractValidator<EditAppointmentsResultCommand>
{
    public EditAppointmentsResultsValidator()
    {
        RuleFor(x => x.Complaints).NotEmpty().WithMessage("Please, enter the complaints");
        RuleFor(x => x.Conclusion).NotEmpty().WithMessage("Please, enter the conclusion");
        RuleFor(x => x.Recommendations).NotEmpty().WithMessage("Please, enter the recommendations");
    }
}
