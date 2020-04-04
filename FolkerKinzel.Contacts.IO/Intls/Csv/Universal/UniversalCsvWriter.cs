using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{

    internal class UniversalCsvWriter : CsvWriter
    {
        internal UniversalCsvWriter(Encoding? textEncoding) : base(textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNames();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }
    }
}
