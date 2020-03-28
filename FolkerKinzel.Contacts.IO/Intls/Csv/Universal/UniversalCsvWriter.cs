using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{

    internal class UniversalCsvWriter : CsvWriter
    {
        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }
    }
}
