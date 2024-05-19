using EmailValidation;
using System.Text.RegularExpressions;

namespace SpaceHackathon_2024.Helpers
{
    public static class UserInputValidator
    {
        /// <summary>
        /// Validates user inputted name or surname. The name can contain letters.
        /// </summary>
        /// <param name="address">The name or surname.</param>
        /// <returns><strong>true</strong> if name is valid, <strong>false</strong> otherwise.</returns>
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Regex.IsMatch(name, "^[a-zA-Z -öäüÖÄÜß]+$");
        }

        /// <summary>
        /// Validates user inputted E-Mail.
        /// </summary>
        /// <param name="address">The E-Mail address.</param>
        /// <returns><strong>true</strong> if E-Mail is valid, <strong>false</strong> otherwise.</returns>
        public static bool IsValidEmailAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }

            return EmailValidator.Validate(address);
        }
    }
}
