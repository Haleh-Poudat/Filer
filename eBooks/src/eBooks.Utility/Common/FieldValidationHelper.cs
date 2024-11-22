using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eBooks.Utility.Common
{
    public static class FieldValidationHelper
    {
        private static readonly Regex AllDigitEqual = new Regex(@"(\d)\1{9}", RegexOptions.Compiled);
        private static readonly Regex NumberOnlyRegex = new Regex(@"\d{10}", RegexOptions.Compiled);

        #region NationalCode

        /// <summary>
        /// Validate NationalCode
        /// </summary>
        /// <param name="nationalCode"></param>
        /// <exception cref="Exception"></exception>
        public static bool IsValidNationalCode(this string nationalCode)
        {
            nationalCode = nationalCode.GetEnglishNumber();
            if (string.IsNullOrEmpty(nationalCode))
                throw new Exception("Please enter the national ID correctly.");

            if (nationalCode.Length != 10)
                throw new Exception("The national ID must be 10 characters long.");

            if (!NumberOnlyRegex.IsMatch(nationalCode))
                throw new Exception("The national ID consists of 10 numeric digits; please enter the national ID correctly.");

            if (AllDigitEqual.IsMatch(nationalCode))
                return false;

            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString(CultureInfo.InvariantCulture)) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString(CultureInfo.InvariantCulture)) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString(CultureInfo.InvariantCulture)) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString(CultureInfo.InvariantCulture)) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString(CultureInfo.InvariantCulture)) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString(CultureInfo.InvariantCulture)) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString(CultureInfo.InvariantCulture)) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString(CultureInfo.InvariantCulture)) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString(CultureInfo.InvariantCulture)) * 2;
            var a = Convert.ToInt32(chArray[9].ToString(CultureInfo.InvariantCulture));

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        #endregion NationalCode

        private static string GetEnglishNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 1777; i < 1786; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(i - 1728));
            }
            return data;
        }
    }
}