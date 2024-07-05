
namespace Profiles.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class Receptionists
        {
            public static Error NotFound => Error.NotFound(
                code: "Receptionist.NotFound",
                description: "Receptionist not found.");

            public static Error EmailAlreadyExists => Error.Conflict(
                code: "Receptionist.EmailAlreadyExists",
                description: "A receptionist with this email already exists.");

            public static Error InvalidOffice => Error.Validation(
                code: "Receptionist.InvalidOffice",
                description: "The office provided is invalid.");
        }
    }
}
