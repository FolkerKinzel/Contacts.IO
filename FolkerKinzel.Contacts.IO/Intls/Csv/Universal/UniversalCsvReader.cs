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
        public CsvOptions Options { get; private set; }

        public char FieldSeparator { get; private set; }


        protected override bool Analyze(string fileName)
        {
            var analyzer = new CsvAnalyzer();
            analyzer.Analyze(fileName);

            if (!analyzer.HasHeader)
            {
                return false;
            }

            this.Options = analyzer.Options | CsvOptions.DisableCaching;
            this.FieldSeparator = analyzer.FieldSeparatorChar;

            return true;
        }


        protected override CsvTools.CsvReader? InitReader(string fileName) => new Csv::CsvReader(fileName, hasHeaderRow: true, options: Options, enc: null, FieldSeparator);


        protected override void InitWrapperAndProperties(CsvRecordWrapper wrapper, List<ContactProp?> properties)
        {
            throw new NotImplementedException();
        }
    }
}
