using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain
{
    public static class Errors
    {
        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(
                "Authentication.InvalidEmailOrPassword",
                "The provided email or password combination is invalid.");

            public static Error PasswordsDoNotMatch => new Error(
                "Authentication.PasswordsDoNotMatch",
                "The password and confirmation password do not match.");

            public static Error DuplicateEmail => new Error(
                "Authentication.DuplicateEmail",
                "The email is already taken.");
        }

        public static class FirstName
        {
            public static Error NullOrEmpty => new Error("FirstName.NullOrEmpty", "The first name is required.");

            public static Error LongerThanAllowed => new Error(
                "FirstName.LongerThanAllowed",
                "The first name is longer than allowed.");
        }
        public static class LastName
        {
            public static Error NullOrEmpty => new Error("LastName.NullOrEmpty", "The last name is required.");

            public static Error LongerThanAllowed => new Error(
                "LastName.LongerThanAllowed",
                "The last name is longer than allowed.");
        }
        public static class Email
        {
            public static Error NullOrEmpty => new Error("Email.NullOrEmpty", "The email is required.");

            public static Error LongerThanAllowed => new Error("Email.LongerThanAllowed", "The email is longer than allowed.");

            public static Error InvalidFormat => new Error("Email.InvalidFormat", "The email format is invalid.");


            public static Error DuplicateEmail => new Error(
                "Authentication.DuplicateEmail",
                "The email is already taken.");
        }
        public static class General
        {
            public static Error BadRequest => new Error("General.BadRequest", "The server could not process the request.");

            public static Error EntityNotFound => new Error(
                "General.EntityNotFound",
                "The entity with the specified identifier was not found.");

            public static Error ServerError => new Error(
                "General.ServerError",
                "The server encountered an unrecoverable error.");
        }
        public static class Name
        {
            public static Error NullOrEmpty => new Error("Name.NullOrEmpty", "The name is required.");

            public static Error LongerThanAllowed => new Error(
                "Name.LongerThanAllowed",
                "The  name is longer than allowed.");
        }

        public static class Amount
        {

        }
        public static class Role
        {
            public static Error AtLeastOnePermissionIsRequired => new Error(
                "Role.AtLeastOnePermissionIsRequired",
                "The role must have at least one permission associated with it.");
        }

        public static class Password
        {
            public static Error NullOrEmpty => new Error("Password.NullOrEmpty", "The password is required.");

            public static Error TooShort => new Error("Password.TooShort", "The password is too short.");

            public static Error MissingUppercaseLetter => new Error(
                "Password.MissingUppercaseLetter",
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                "Password.MissingLowercaseLetter",
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                "Password.MissingDigit",
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                "Password.MissingNonAlphaNumeric",
                "The password requires at least one non-alphanumeric.");
        }

        public static class PhoneNumber
        {
            public static Error NullOrEmpty => new Error("PhoneNumber.NullOrEmpty", "The phone number is required.");

            public static Error LongerThanAllowed => new Error(
                "PhoneNumber.LongerThanAllowed",
                "The  phone number is longer than allowed.");

            public static Error InvalidFormat => new Error("PhoneNumber.InvalidFormat", "The phone number format is invalid.");
        }

        #region AddressLine
        public static class AddressLine
        {
            public static Error NullOrEmpty => new Error("AddressLine.NullOrEmpty", "The address line is required.");

            public static Error LongerThanAllowed => new Error(
                "AddressLine.LongerThanAllowed",
                "The address line is longer than allowed.");
        }
        #endregion
    }
}
