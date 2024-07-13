public static partial class Errors
{
    public static class Documents
    {
        public static Error NotFound => Error.NotFound(
            code:"Documents.NotFound",
            description:"Cannot found docuemnt with such GUID");
    }
}
