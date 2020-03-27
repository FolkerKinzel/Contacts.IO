using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Linq;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird
{
    internal class ThunderbirdCsvReader : CsvReader
    {
        protected override IEnumerable<Tuple<string, ContactProp?, IEnumerable<string>>> CreateMapping()
        {
            throw new NotImplementedException();
        }

        protected override CsvRecordWrapper InitWrapperAndProperties(List<ContactProp?> properties)
        {
            throw new NotImplementedException();
        }
    }
}
