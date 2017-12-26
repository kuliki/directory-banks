using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Directory.Banks
{
    internal static class EnumHelper
    {
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            T ret;

            if (Enum.TryParse(value, true, out ret))
                return ret;

            throw new ArgumentException($"'{value}' is not valid value of '{typeof(T).Name}'");
        }

        public static T? ToNulalbleEnum<T>(this string value)
            where T : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return ToEnum<T>(value);
        }
    }
}
