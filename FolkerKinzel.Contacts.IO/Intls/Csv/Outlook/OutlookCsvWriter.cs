using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{
    internal class OutlookCsvWriter : CsvWriter
    {
        protected override void InitMapping(List<Tuple<string, ContactProp?>> mapping)
        {
            throw new NotImplementedException();
        }

        protected override SexConverter InitSexConverter()
        {
            return new OutlookSexConverter();
        }
    }
}
