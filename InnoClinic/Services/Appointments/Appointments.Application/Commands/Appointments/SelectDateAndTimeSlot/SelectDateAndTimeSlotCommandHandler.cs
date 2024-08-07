public class SelectDateAndTimeSlotCommandHandler(
    IUnitOfWork unitOfWork, 
    ITimeSlotsGenerator timeSlotGenerator) 
    : IRequestHandler<SelectDateAndTimeSlotCommand, ErrorOr<List<TimeSlotResponse>>>
{
    public async Task<ErrorOr<List<TimeSlotResponse>>> Handle(SelectDateAndTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(request.ServiceId);

        if (service is null)
        {
            return Error.NotFound();
        }

        var avaibaleSlots = await timeSlotGenerator
            .GenerateSlots(request.AppointmentDate, TimeSpan.FromHours(8), TimeSpan.FromHours(17), service.ServiceCategory);

        if (!avaibaleSlots.Any())
        {
            return Error.NotFound();
        }

        return avaibaleSlots;
    }
}
