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
        private bool _birthDayInitalized;
        public DateTime? _birthDay;

        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IEnumerable<Tuple<string, ContactProp?, IEnumerable<string>>> CreateMapping() => HeaderRow.GetMappingEN();


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


        protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            if (!_birthDayInitalized)
            {
                this._birthDay = contact.Person?.BirthDay;
                _birthDayInitalized = true;
            }

            if (_birthDay.HasValue)
            {
                wrapper[index] = prop switch
                {
                    (ContactProp)AdditionalProps.BirthYear => _birthDay.Value.Year,
                    (ContactProp)AdditionalProps.BirthMonth => _birthDay.Value.Month,
                    (ContactProp)AdditionalProps.BirthDay => _birthDay.Value.Day,
                    _ => 0
                };
            }
        }

    }
}
