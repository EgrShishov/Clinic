public static partial class Errors
{
    public static class Doctors
    {
        public static Error NotFound => Error.NotFound(
            code: "Doctor.NotFound",
            description: "Doctor not found.");

        public static Error InvalidSpecialization => Error.Validation(
            code: "Doctor.InvalidSpecialization",
            description: "The specialization provided is invalid.");

        public static Error LicenseNumberAlreadyExists => Error.Conflict(
            code: "Doctor.LicenseNumberAlreadyExists",
            description: "A doctor with this license number already exists.");
    }
}
