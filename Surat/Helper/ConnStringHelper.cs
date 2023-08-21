namespace Surat.Helper
{
    public static class ConnStringHelper
    {
        public static string Get()
        {
            var connString = "Server=Jude7;Database=SuratKu;Trusted_Connection=True;";
            return connString;
        }
    }
}
