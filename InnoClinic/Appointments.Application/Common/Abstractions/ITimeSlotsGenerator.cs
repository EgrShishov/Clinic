public interface ITimeSlotsGenerator
{
    public List<TimeSpan> GenerateSlots(DateTime appointmentDate, TimeSpan startWorkingHours, TimeSpan endWorkingHours, string serviceCategoryId);
}
