using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.Utility.Extentions
{
    public static class TypeExtentions
    {
        public static DateTime ConvertFromAndroidDateTime(this DateTime dateTime, string androidDateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 4, 30, 0, DateTimeKind.Utc);
            dateTime = epoch.AddMilliseconds(Convert.ToInt64(androidDateTime));
            return dateTime;
        }

        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider target, bool inherits) where TAttribute : Attribute
        {
            return target.IsDefined(typeof(TAttribute), inherits);
        }

        public static bool IsAllDigit(this string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        public static string GetLast(this string source, int charCount)
        {
            if (charCount >= source.Length)
                return source;
            return source.Substring(source.Length - charCount, charCount);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsInteger(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return true;

                default:
                    return false;
            }
        }

        public static Type GetNonNullableType(this Type type)
        {
            return !IsNullable(type) ? type : type.GetGenericArguments()[0];
        }

        public static bool IsNullable(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static TypeConverter GetTypeConverter(Type type)
        {
            return ConvertExtentions.GetTypeConverter(type);
        }

        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>

        public static T GetCustomAttribut<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttributes(true)
                .FirstOrDefault(x => x is T) as
                T;
        }

        public static string NormalizeForFileName(this string str)
        {
            str = str.Replace(":", " ");
            str = str.Replace("/", " ");
            str = str.Replace("?", " ");
            str = str.Replace("\\", " ");
            str = str.Replace("*", " ");
            str = str.Replace("$", " ");
            return str;
        }
    }
}