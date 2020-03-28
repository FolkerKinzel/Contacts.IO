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

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird
{
    internal class ThunderbirdCsvWriter : CsvWriter
    {
        private Conv::ICsvTypeConverter? _intConverter = null;
     

        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IList<Tuple<string, ContactProp?, IEnumerable<string>>> CreateMapping() => HeaderRow.GetMappingEN();


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IEnumerable<string>> tpl, CsvRecordWrapper wrapper)
        {
            Debug.Assert(tpl.Item2.HasValue);

            //case (ContactProp)AdditionalProps.BirthYear:
            //case (ContactProp)AdditionalProps.BirthMonth:
            //case (ContactProp)AdditionalProps.BirthDay:

            
            _intConverter ??= Conv::CsvConverterFactory.CreateConverter(Conv.CsvTypeCode.Int32);
            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            _intConverter));
        }


        protected override object? FillCsvRecordNonStandardProp(Contact contact, ContactProp prop)
        {
            var _birthDay = contact.Person?.BirthDay;
           
            return _birthDay.HasValue ? prop switch
                {
                    (ContactProp)AdditionalProp.BirthYear => _birthDay.Value.Year,
                    (ContactProp)AdditionalProp.BirthMonth => _birthDay.Value.Month,
                    (ContactProp)AdditionalProp.BirthDay => _birthDay.Value.Day,
                    _ => null
                } : (object?)null;
            
        }

    }
}
