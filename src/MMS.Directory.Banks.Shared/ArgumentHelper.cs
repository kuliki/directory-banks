using System;
using System.Collections.Generic;
using System.Linq;

namespace MMS.Directory.Banks
{
    internal static class ArgumentHelper
    {
        public static void AssertNotNull<T>(this T argumentValue, string argumentName)
            where T : class
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void AssertNotNullOrEmpty<T>(this IEnumerable<T> argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);

            if (!argumentValue.Any())
                throw new ArgumentException(argumentName);
        }

        public static bool In<T>(this T value, params T[] set)
        {
            return set.Contains(value);
        }
    }
}
