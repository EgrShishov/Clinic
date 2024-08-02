public interface ITimeSlotsGenerator
{
    public List<TimeSlot> GenerateSlots(DateTime appointmentDate, int duration, int serviceCategory);
}
