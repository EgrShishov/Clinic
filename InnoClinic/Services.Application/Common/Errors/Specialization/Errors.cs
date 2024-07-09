public static partial class Errors
{
    public static class Specialization
    {
        public static Error NotFound => Error.NotFound(
            code: "Specialization.NotFound",
            description: "Specialization not found.");
    }
}
