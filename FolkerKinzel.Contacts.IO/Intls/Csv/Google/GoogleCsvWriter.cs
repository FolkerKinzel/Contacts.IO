using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal class GoogleCsvWriter : CsvWriter
    {
        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }
    }
}
