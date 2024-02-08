using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.Utility.Extensions
{
    public static class StringValidatorExtension
    {
        #region string Extentions
        public static string NullOrEmpty(this string input, string displayColumnName)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must not be empty{input}.";
        }
        public static string NullOrWhiteSpace(this string input, string displayColumnName)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must not be empty{input}.";
        }

        public static string EmailAddress(this string input, string displayColumnName)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
    RegexOptions.CultureInvariant | RegexOptions.Singleline);
                if (regex.IsMatch(input))
                {
                    return string.Empty;
                }
            }

            return $"'{displayColumnName}' is not a valid email address.";
        }

        public static string MinimumLength(this string input, int min, string displayColumnName)
        {
            if (input.Length >= min)
            {
                return string.Empty;
            }
            return $"The length of '{displayColumnName}' must be at least {min} characters. You entered {input} characters.";
        }

        public static string MaximumLength(this string input, int max, string displayColumnName)
        {
            if (input.Length <= max)
            {
                return string.Empty;
            }
            return $"The length of '{displayColumnName}' must be {max} characters or fewer. You entered {input} characters.";
        }
        #endregion
    }
}
