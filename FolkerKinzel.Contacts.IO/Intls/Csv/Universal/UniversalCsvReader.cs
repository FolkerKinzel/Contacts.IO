using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Linq;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal class UniversalCsvReader : CsvReader
    {
        
        protected override IList<Tuple<string, ContactProp?, IEnumerable<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

        
    }
}
