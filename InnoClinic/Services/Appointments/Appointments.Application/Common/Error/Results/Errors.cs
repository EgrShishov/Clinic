public static partial class Errors
{
    public static class Results
    {
        public static Error NotFound => Error.NotFound(
            code: "AppointmentResults.NotFound",
            description: "Cannot found results with given id");
    }
}
