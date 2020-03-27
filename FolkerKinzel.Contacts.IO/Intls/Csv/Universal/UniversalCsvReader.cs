using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Linq;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal class UniversalCsvReader : CsvReader
    {
        protected override CsvTools.CsvReader? InitReader(string fileName)
        {
            var analyzer = new CsvAnalyzer();
            analyzer.Analyze(fileName);
           
            return analyzer.HasHeader ? new Csv::CsvReader(fileName, hasHeaderRow: true, options: analyzer.Options | CsvOptions.DisableCaching, enc:  null, fieldSeparator: analyzer.FieldSeparatorChar) : null;
        }

        protected override void InitWrapperAndProperties(CsvRecordWrapper wrapper, List<ContactProp?> properties)
        {
            throw new NotImplementedException();
        }
    }
}
