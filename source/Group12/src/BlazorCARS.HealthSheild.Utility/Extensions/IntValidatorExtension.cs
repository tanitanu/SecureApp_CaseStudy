using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.Utility.Extensions
{
    public static class IntValidatorExtension
    {
        #region Integer Extentions
        public static string NotrEmpty(this int? input, string displayColumnName)
        {
            if (input != null || input.HasValue)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must not be empty {input}.";
        }
        public static string GreaterThan(this int input, int value, string displayColumnName)
        {
            if (input > value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be greater than {value}.";
        }
        public static string GreaterThanOrEqual(this int input, int value, string displayColumnName)
        {
            if (input >= value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be greater than or equal to {value}.";
        }
        public static string LessThan(this int input, int value, string displayColumnName)
        {
            if (input < value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be less than {value}.";
        }
        public static string LessThanOrEqual(this int input, int value, string displayColumnName)
        {
            if (input <= value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be less than or equal to {value}.";
        }
        public static string NotEqual(this int input, int value, string displayColumnName)
        {
            if (input != value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' should not be equal to {value}.";
        }
        public static string Equal(this int input, int value, string displayColumnName)
        {
            if (input == value)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' should be equal to {value}.";
        }
        public static string ExclusiveBetween(this int input, int min, int max, string displayColumnName)
        {
            if (input > min && input < max)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be between {min} and {max} (exclusive). You entered {input}.";

        }
        public static string InclusiveBetween(this int input, int min, int max, string displayColumnName)
        {
            if (input >= min && input <= max)
            {
                return string.Empty;
            }
            return $"'{displayColumnName}' must be between {min} and {max} (inclusive). You entered {input}.";
        }

        #endregion
    }
}
