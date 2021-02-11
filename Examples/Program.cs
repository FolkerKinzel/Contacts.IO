using System;
using System.Globalization;
using System.Threading;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

             //CsvExample.ReadingAndWritingCsv();
             VCardExample.ReadingAndWritingVCard();

        }
    }
}
