using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird
{
    internal class ThunderbirdCsvWriter : CsvWriter
    {
        private ICsvTypeConverter? _intConverter = null;

        internal ThunderbirdCsvWriter(Encoding? textEncoding) : base(textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            Debug.Assert(tpl.Item2.HasValue);

            //case (ContactProp)AdditionalProps.BirthYear:
            //case (ContactProp)AdditionalProps.BirthMonth:
            //case (ContactProp)AdditionalProps.BirthDay:

            
            _intConverter ??= CsvConverterFactory.CreateConverter(CsvTypeCode.Int32, nullable: true);
            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            _intConverter));
        }


        protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            var _birthDay = contact.Person?.BirthDay;

            wrapper[index] = _birthDay.HasValue ? prop switch
            {
                (ContactProp)AdditionalProp.BirthYear => _birthDay.Value.Year,
                (ContactProp)AdditionalProp.BirthMonth => _birthDay.Value.Month,
                (ContactProp)AdditionalProp.BirthDay => _birthDay.Value.Day,
                _ => null
            } : (int?)null;

        }

        

    }
}
