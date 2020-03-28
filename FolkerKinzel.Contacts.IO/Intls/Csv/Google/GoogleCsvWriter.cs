using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal class GoogleCsvWriter : CsvWriter
    {
        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IEnumerable<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }
    }
}
