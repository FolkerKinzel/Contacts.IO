using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal class UniversalCsvReader : CsvReader
    {

        internal UniversalCsvReader(Encoding? textEncoding) : base(textEncoding) { }


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

        
    }
}
