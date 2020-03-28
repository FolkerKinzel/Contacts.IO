using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{

    internal class OutlookCsvWriter : CsvWriter
    {


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

        protected override SexConverter InitSexConverter()
        {
            return new OutlookSexConverter();
        }
    }
}
