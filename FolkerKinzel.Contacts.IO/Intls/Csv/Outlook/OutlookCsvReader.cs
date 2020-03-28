using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{
    internal class OutlookCsvReader : CsvReader
    {
        internal OutlookCsvReader(Encoding? textEncoding) : base(textEncoding) { }

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

        
    }
}
