using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Linq;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{
    internal class OutlookCsvReader : CsvReader
    {
        protected override void InitWrapperAndProperties(CsvRecordWrapper wrapper, List<ContactProp> properties)
        {
            throw new NotImplementedException();
        }
    }
}
