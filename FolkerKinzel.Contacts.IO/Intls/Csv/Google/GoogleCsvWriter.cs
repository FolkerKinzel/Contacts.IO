using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal class GoogleCsvWriter : CsvWriter
    {
        internal GoogleCsvWriter(Encoding? textEncoding) : base(textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();
    }
}
