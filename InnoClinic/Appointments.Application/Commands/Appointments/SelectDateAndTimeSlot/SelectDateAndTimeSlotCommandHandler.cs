public class SelectDateAndTimeSlotCommandHandler(IUnitOfWork unitOfWork, ITimeSlotGenerator timeSlotGenerator) 
    : IRequestHandler<SelectDateAndTimeSlotCommand, ErrorOr<List<TimeSlot>>>
{
    public async Task<ErrorOr<List<TimeSlot>>> Handle(SelectDateAndTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentsRepository.ListAsync(a => a.Date ==  request.AppointmentDate.Date);

        return Error.NotFound();
    }
}
