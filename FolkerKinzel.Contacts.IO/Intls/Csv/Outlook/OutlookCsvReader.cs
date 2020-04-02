﻿using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using FolkerKinzel.CsvTools.Helpers.Converters.Specialized;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{
    internal class OutlookCsvReader : CsvReader
    {
        internal OutlookCsvReader(Encoding? textEncoding) : base(textEncoding) { }

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            Debug.Assert(Analyzer.HasHeader == true);
            Debug.Assert(Analyzer.ColumnNames != null);

            var mapping = HeaderRow.GetMappingEN();

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
                    mapping[i].Item3[0] = Analyzer.ColumnNames[i];
                }

//                for (int i = end; i < mapping.Count; i++)
//                {
//                    var currentTpl = mapping[i];

//#if NET40
//                    mapping[i] = new Tuple<string, ContactProp?, IList<string>>(currentTpl.Item1, null, currentTpl.Item3);

//#else
//                    mapping[i] = new Tuple<string, ContactProp?, IList<string>>(currentTpl.Item1, null, Array.Empty<string>());
//#endif
//                }

            }


            return mapping;
        }

        protected override SexConverter InitSexConverter() => new OutlookSexConverter();


        protected override ICsvTypeConverter InitNullableDateTimeConverter() => new DateTimeConverter("M/d/yyyy", true);


        


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            StringConverter));
        }


        protected override void InitContactNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.BusinessStreet2:
                    break;
                case AdditionalProp.BusinessStreet3:
                    break;
                case AdditionalProp.HomeStreet2:
                    break;
                case AdditionalProp.HomeStreet3:
                    break;
                case AdditionalProp.AssistantsPhone:
                    break;
                case AdditionalProp.BusinessPhone2:
                    break;
                case AdditionalProp.Callback:
                    break;
                case AdditionalProp.CompanyMainPhone:
                    break;
                case AdditionalProp.HomePhone2:
                    break;
                case AdditionalProp.ISDN:
                    break;
                case AdditionalProp.OtherFax:
                    break;
                case AdditionalProp.Pager:
                    break;
                case AdditionalProp.RadioPhone:
                    break;
                case AdditionalProp.TTY_TDD_Phone:
                    break;
                case AdditionalProp.Telex:
                    break;
                case AdditionalProp.Email3Address:
                    break;
                default:
                    break;
            }
        }
    }
}
