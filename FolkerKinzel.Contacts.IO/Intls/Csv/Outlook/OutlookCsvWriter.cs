using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using FolkerKinzel.CsvTools.Helpers.Converters.Specialized;
using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{

    internal class OutlookCsvWriter : CsvWriter
    {
        internal OutlookCsvWriter(Encoding? textEncoding) : base(textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();
        

        protected override SexConverter InitSexConverter() => new OutlookSexConverter();


        protected override ICsvTypeConverter InitNullableDateTimeConverter() => new DateTimeConverter("M/d/yyyy", true);


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            throw new NotImplementedException();
        }


        protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            throw new NotImplementedException();
        }
    }
}
