public static partial class Errors
{
    public static class Results
    {
        public static Error NotFound => Error.NotFound(
            code: "AppointmentResults.NotFound",
            description: "Cannot found results with given id");

        public static Error CannotGeneratePDF => Error.Failure(
            code: "AppointmentResults.PDFGenerationError",
            description: "Cannot generate PDF for results");
    }
}
