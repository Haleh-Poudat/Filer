using System.Globalization;

namespace eBooks.Utility.Converter
{
    public static class ConvertString
    {
        public static DateTime ConvertStringToDateTime(string stringDateTime)
        {
            string format = "yyyy/MM/dd";
            CultureInfo culture = new CultureInfo("fa-IR");
            DateTime result = DateTime.ParseExact(stringDateTime, format, culture);
            return result;
        }

        public static string ToCurrencyStyle(this string text)
        {
            string s = "";

            if (text == null)
            {
                return "";
            }
            int start = text.Length % 3;

            string outt = "";

            outt += text.Substring(0, start);
            int length = text.Length;
            for (int i = start; i < length; i += 3)
            {
                if (i != 0)
                {
                    outt += ",";
                }
                outt += (text.Substring(i, 3));
            }
            return outt;
        }

    }
}
