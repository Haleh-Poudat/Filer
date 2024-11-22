using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.Utility.Extentions
{
    public static class ConvertExtentions
    {
        #region Fields

        private static readonly IDictionary<Type, TypeConverter> CustomTypeConverters;

        #endregion Fields

        #region Ctor

        static ConvertExtentions()
        {
            CustomTypeConverters = new Dictionary<Type, TypeConverter>();
        }

        #endregion Ctor

        #region Object

        internal static TypeConverter GetTypeConverter(Type type)
        {
            TypeConverter converter;
            return CustomTypeConverters.TryGetValue(type, out converter) ? converter : TypeDescriptor.GetConverter(type);
        }

        #endregion Object
    }
}