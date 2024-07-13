public static partial class Errors
{
    public static class Photos
    {
        public static Error NotFound => Error.NotFound(
            code: "Photos.NotFound",
            description: "Cannot found photo with such GUID");
    }
}
