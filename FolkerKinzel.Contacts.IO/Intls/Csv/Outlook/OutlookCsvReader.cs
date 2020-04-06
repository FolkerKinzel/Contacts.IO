using FolkerKinzel.CsvTools.Helpers;
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
        internal OutlookCsvReader(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider, textEncoding) { }

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


        protected override ICsvTypeConverter InitNullableDateConverter() => new DateTimeConverter("M/d/yyyy", nullable:true, formatProvider: this.FormatProvider);





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
            Debug.Assert(wrapper[index] != null);

            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.BusinessStreet2:
                case AdditionalProp.BusinessStreet3:
                    {
                        contact.Work ??= new Work();
                        var adrWork = contact.Work.AddressWork;
                        adrWork ??= new Address();
                        contact.Work.AddressWork = adrWork;

                        adrWork.Street += $" {(string?)wrapper[index]}";
                    }
                    break;
                case AdditionalProp.HomeStreet2:
                case AdditionalProp.HomeStreet3:
                    {
                        var adrHome = contact.AddressHome ?? new Address();
                        contact.AddressHome = adrHome;

                        adrHome.Street += $" {(string?)wrapper[index]}";
                    }
                    break;
                case AdditionalProp.AssistantsPhone:
                case AdditionalProp.BusinessPhone2:
                case AdditionalProp.CompanyMainPhone:
                    {
                        var phones = (List<PhoneNumber>?)contact.PhoneNumbers ?? new List<PhoneNumber>();
                        contact.PhoneNumbers = phones;

                        phones.Add(new PhoneNumber((string?)wrapper[index], true));
                    }
                    break;
                case AdditionalProp.HomePhone2:   
                case AdditionalProp.ISDN:
                case AdditionalProp.RadioPhone:
                case AdditionalProp.TTY_TDD_Phone:
                    {
                        var phones = (List<PhoneNumber>?)contact.PhoneNumbers ?? new List<PhoneNumber>();
                        contact.PhoneNumbers = phones;

                        phones.Add(new PhoneNumber((string?)wrapper[index]));
                    }
                    break;
                case AdditionalProp.OtherFax:
                    {
                        var phones = (List<PhoneNumber>?)contact.PhoneNumbers ?? new List<PhoneNumber>();
                        contact.PhoneNumbers = phones;

                        phones.Add(new PhoneNumber((string?)wrapper[index], isFax: true));
                    }
                    break;
                
               
                
                default:
                    break;
            }
        }
    }
}
