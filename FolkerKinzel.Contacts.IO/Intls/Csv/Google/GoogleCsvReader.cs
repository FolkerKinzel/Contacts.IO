using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{
    internal class GoogleCsvReader : CsvReader
    {
        internal GoogleCsvReader(Encoding? textEncoding) : base(textEncoding) { }


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

       
    }
}
