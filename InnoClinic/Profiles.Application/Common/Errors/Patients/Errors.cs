
namespace Profiles.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class Patients
        {
            public static Error NotFound => Error.NotFound(
            code: "Patient.NotFound",
            description: "Patient not found.");

            public static Error InvalidEmail => Error.Validation(
                code: "Patient.InvalidEmail",
                description: "The email provided is invalid.");

            public static Error EmailAlreadyExists => Error.Conflict(
                code: "Patient.EmailAlreadyExists",
                description: "A patient with this email already exists.");

            public static Error InvalidPhoneNumber => Error.Validation(
                code: "Patient.InvalidPhoneNumber",
                description: "The phone number provided is invalid.");
        }
    }
}
