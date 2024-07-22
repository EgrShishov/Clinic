public class TimeSlotsGenerator : ITimeSlotsGenerator
{
    public List<TimeSpan> GenerateSlots(DateTime appointmentDate, TimeSpan startWorkingHours, TimeSpan endWorkingHours, string serviceCategory)
    {
        List<TimeSpan> availableSlots = new List<TimeSpan>();
        int duration = 10;

        for (var time = startWorkingHours; time < endWorkingHours; time = time.Add(TimeSpan.FromMinutes(duration)))
        {
            var requiredMinutes = serviceCategory switch
            {
                "Analyses" => 10,
                "Consultation" => 20,
                "Diagnostics" => 30,
                _ => 10
            };

            var endTime = time.Add(TimeSpan.FromMinutes(requiredMinutes));

            if (endTime > endWorkingHours)
            {
                break;
            }

            var slotAvailable = !existingAppointments.Any(a =>
                (a.StartTime < endTime && a.EndTime > time));

            if (slotAvailable)
            {
                availableSlots.Add(time);
            }
        }

        return availableSlots;
    }
}
