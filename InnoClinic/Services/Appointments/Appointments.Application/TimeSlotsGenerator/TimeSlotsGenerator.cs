public class TimeSlotsGenerator(IUnitOfWork unitOfWork) : ITimeSlotsGenerator
{
    public async Task<List<TimeSlotResponse>> GenerateSlots(
        DateTime appointmentDate, 
        TimeSpan startWorkingHours, 
        TimeSpan endWorkingHours, 
        string serviceCategory)
    {
        List<TimeSlotResponse> slots = new List<TimeSlotResponse>();

        var bookedSlots = await GetBookedSlotsForDate(appointmentDate);

        for (var time = startWorkingHours; time < endWorkingHours;)
        {
            var requiredMinutes = serviceCategory switch
            {
                "Analyses" => 10,
                "Consultation" => 20,
                "Diagnostics" => 30,
                _ => 10
            }; //api get service categories

            var endTime = time.Add(TimeSpan.FromMinutes(requiredMinutes));

            if (endTime > endWorkingHours)
            {
                break;
            }

            slots.Add(new TimeSlotResponse
            {
                StartTime = time,
                EndTime = endTime,
                AppointmentDate = appointmentDate,
                IsAvaibale = true
            });
        }

        return slots;
    }

    public async Task<List<TimeSlotResponse>> GetBookedSlotsForDate(DateTime AppointmentDate)
    {
        var avaibaleSlots = new List<TimeSlotResponse>();

        var appointmentsByDate = await unitOfWork.AppointmentsRepository.ListAsync(a => a.Date == AppointmentDate.Date);
        foreach(var appointment in appointmentsByDate)
        {
            avaibaleSlots.Add(new TimeSlotResponse
            {
                IsAvaibale = false,
                AppointmentDate = AppointmentDate,
                StartTime = appointment.Time
            });
        }

        return avaibaleSlots;
    }
}
