using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using FolkerKinzel.CsvTools.Helpers.Converters.Specialized;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook
{

    internal class OutlookCsvWriter : CsvWriter
    {
        internal OutlookCsvWriter(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider ?? CultureInfo.CurrentCulture, textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();
        

        protected override SexConverter InitSexConverter() => new OutlookSexConverter();


        //protected override ICsvTypeConverter InitNullableDateConverter() => new DateTimeConverter("M/d/yyyy", nullable: true, formatProvider: this.FormatProvider);


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            StringConverter));
        }


        protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.BusinessStreet2:
                case AdditionalProp.BusinessStreet3:
                case AdditionalProp.HomeStreet2:
                case AdditionalProp.HomeStreet3:
                    break;
                case AdditionalProp.AssistantsPhone:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && x.IsWork && !x.IsFax), 2);
                    break;
                case AdditionalProp.BusinessPhone2:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && x.IsWork && !x.IsFax), 3);
                    break;
                case AdditionalProp.CompanyMainPhone:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && x.IsWork && !x.IsFax), 4);
                    break;
                case AdditionalProp.HomePhone2:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && !x.IsWork && !x.IsFax), 7);

                    break;
                case AdditionalProp.ISDN:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && !x.IsWork && !x.IsFax), 8);

                    break;
                case AdditionalProp.RadioPhone:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && !x.IsWork && !x.IsFax), 9);

                    break;
                case AdditionalProp.TTY_TDD_Phone:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && !x.IsWork && !x.IsFax), 10);

                    break;
                case AdditionalProp.OtherFax:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers?.Where(x => x != null && x.IsFax), 2);
                    break;
                default:
                    break;
            }
        }


        private static string? GetValueFromPhone(IEnumerable<PhoneNumber?>? phones, int index)
        {
            var phone = phones?.ElementAtOrDefault(index);

            return phone?.Value;
        }
    }
}
