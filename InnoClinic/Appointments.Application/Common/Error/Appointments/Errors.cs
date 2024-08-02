public static partial class Errors
{
    public static class Appointments
    {
        public static Error NotFound => Error.NotFound(
            code: "Appointments.NotFound",
            description: "Cannot found appointment with given id");

        public static Error RescheduleIsImpossibleBecauseIsApproved => Error.Failure(
            code: "Appointments.CannotReschedule",
            description: "Only appointments that aren’t approved can be rescheduled");
    }
}