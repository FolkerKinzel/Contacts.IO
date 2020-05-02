using System;
using System.Globalization;
using System.Threading;

namespace Examples
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // CsvExample.ReadingAndWritingCsv();
             VCardExample.ReadingAndWritingVCard();

        }
    }
}
