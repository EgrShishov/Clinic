﻿public class CreateAppointmentResultValidator : AbstractValidator<CreateAppointmentsResultCommand>
{
    public CreateAppointmentResultValidator()
    {
        RuleFor(x => x.AppointmentDate).NotEmpty();
        RuleFor(x => x.DateofBirth).NotEmpty();
        RuleFor(x => x.Complaints).NotEmpty().WithMessage("Please, enter the complaints");
        RuleFor(x => x.Conclusion).NotEmpty().WithMessage("Please, enter the conclusion");
        RuleFor(x => x.Recommendations).NotEmpty().WithMessage("Please, enter the recommendations");
    }
}
