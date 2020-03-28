using FolkerKinzel.CsvTools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird
{
    internal class ThunderbirdCsvReader : CsvReader
    {
        private Conv::ICsvTypeConverter? _intConverter = null;


        internal ThunderbirdCsvReader(System.Text.Encoding? textEncoding) : base(textEncoding) { }


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            Debug.Assert(Analyzer.HasHeader == true);
            Debug.Assert(Analyzer.ColumnNames != null);


            // englische Spaltennamen
            var mapping = HeaderRow.GetMappingEN();

            if (mapping.Where(x => x.Item2.HasValue).All(x => Analyzer.ColumnNames.Contains(x.Item3[0])))
            {
                return mapping;
            }
            else
            {
                // deutsche Spaltennamen
                var germanColumnNames = HeaderRow.GetColumnNamesDe();

                Debug.Assert(germanColumnNames.Length == mapping.Count);


                for (int i = 0; i < mapping.Count; i++)
                {
                    mapping[i].Item3[0] = germanColumnNames[i];
                }

                if (mapping.Where(x => x.Item2.HasValue).All(x => Analyzer.ColumnNames.Contains(x.Item3[0])))
                {
                    return mapping;
                }
                else
                {
                    // Spaltenreihenfolge

                    var end = Math.Min(mapping.Count, Analyzer.ColumnNames.Count);

                    for (int i = 0; i < mapping.Count; i++)
                    {
                        mapping[i].Item3[0] = germanColumnNames[i];
                    }

                    for (int i = end; i < mapping.Count; i++)
                    {
                        var currentTpl = mapping[i];

#if NET40
                        mapping[i] = new Tuple<string, ContactProp?, IList<string>>(currentTpl.Item1, null, currentTpl.Item3);

#else
                        mapping[i] = new Tuple<string, ContactProp?, IList<string>>(currentTpl.Item1, null, Array.Empty<string>());
#endif
                    }
                }
            }


            return mapping;
        }


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            Debug.Assert(tpl.Item2.HasValue);

            //case (ContactProp)AdditionalProps.BirthYear:
            //case (ContactProp)AdditionalProps.BirthMonth:
            //case (ContactProp)AdditionalProps.BirthDay:


            _intConverter ??= Conv::CsvConverterFactory.CreateConverter(Conv.CsvTypeCode.Int32, nullable: true);
            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            _intConverter));
        }



        protected override void InitContactNonStandardProp(Contact contact, ContactProp prop, object? value)
        {
            if (value is null) return;

            int intValue = (int)value;

            var person = contact.Person ?? new Person();
            contact.Person = person;

            var birthDay = person.BirthDay;

            // Der CsvRecordWrapper bestimmt die Reihenfolge der Auswertung. Deshalb wird immer zuerst Year, dann Month, dann Day initialisiert.

            try
            {
                switch (prop)
                {
                    case (ContactProp)AdditionalProp.BirthYear:
                        person.BirthDay = new DateTime(intValue, 1, 1);
                        break;
                    case (ContactProp)AdditionalProp.BirthMonth:
                        if (birthDay.HasValue)
                        {
                            person.BirthDay = new DateTime(birthDay.Value.Year, intValue, birthDay.Value.Day);
                        }
                        break;
                    case (ContactProp)AdditionalProp.BirthDay:
                        if (birthDay.HasValue)
                        {
                            person.BirthDay = new DateTime(birthDay.Value.Year, birthDay.Value.Month, intValue);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

    }
}
