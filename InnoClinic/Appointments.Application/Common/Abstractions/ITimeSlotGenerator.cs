public interface ITimeSlotGenerator
{
    public List<TimeSlot> GenerateSlots(DateTime appointmentDate, int duration, int serviceCategory);
}
