using System.Globalization;

namespace Solution.Api.Configurations.Setup
{
    public static class DataFormatSetup
    {
        public static void SetDateAndNumberFormats()
        {
            // utilizamos a cultura da máquina, com exceção do formato de data e número (copiamos de pt-BR)
            CultureInfo ptBRCulture = new CultureInfo("pt-BR");
            CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            DateTimeFormatInfo taticDateFormat = (DateTimeFormatInfo)currentCulture.DateTimeFormat.Clone();

            taticDateFormat.FullDateTimePattern = ptBRCulture.DateTimeFormat.FullDateTimePattern;
            taticDateFormat.LongDatePattern = ptBRCulture.DateTimeFormat.LongDatePattern;
            taticDateFormat.LongTimePattern = ptBRCulture.DateTimeFormat.LongTimePattern;
            taticDateFormat.MonthDayPattern = ptBRCulture.DateTimeFormat.MonthDayPattern;
            taticDateFormat.ShortDatePattern = ptBRCulture.DateTimeFormat.ShortDatePattern;
            taticDateFormat.ShortTimePattern = ptBRCulture.DateTimeFormat.ShortTimePattern;
            taticDateFormat.YearMonthPattern = ptBRCulture.DateTimeFormat.YearMonthPattern;

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture.Name)
            {
                NumberFormat = ptBRCulture.NumberFormat,
                DateTimeFormat = taticDateFormat
            };

            CultureInfo.DefaultThreadCurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }
    }
}
