using System.Globalization;

namespace Surat.Helper
{
    public static class DateTimeHelper
    {
        private const string YMD = "yyyy-MM-dd";
        private const string DMY = "dd-MM-yyyy";
        private const string YMD_HMS = "yyyy-MM-dd HH:mm:ss";
        private const string YMD_HM = "yyyy-MM-dd HH:mm";
        private const string HMS = "HH:mm:ss";
        private const string HM = "HH:mm";

        public static bool IsValidTgl(this string tgl, string format)
        {
            DateTime dummyDate;
            var result = DateTime.TryParseExact(tgl, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);
            return result;
        }

        public static DateTime ToDate(this string stringTgl)
        {
            DateTime dummyDate;
            //  coba parsing sebagai DMY
            bool isValid = DateTime.TryParseExact(stringTgl, DMY,
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out dummyDate);

            //  jika tidak berhasil, parsing sebagai YMD
            if (!isValid)
            {
                isValid = DateTime.TryParseExact(stringTgl, YMD,
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dummyDate);
            }

            if (isValid)
            {
                return dummyDate;
            }
            else
            {
                throw new InvalidOperationException("Invalid string date");
            }
        }
    }
}
