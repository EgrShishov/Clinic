public class SelectDateAndTimeSlotValidator : AbstractValidator<SelectDateAndTimeSlotCommand>
{
    public SelectDateAndTimeSlotValidator()
    {
        RuleFor(x => x.ServiceId).GreaterThan(0);
        RuleFor(x => x.AppointmentId).GreaterThan(0);
        RuleFor(x => x.ServiceId).GreaterThan(0);
        RuleFor(x => x.AppointmentDate).NotEmpty().WithMessage("Please, select the date");
    }
}
