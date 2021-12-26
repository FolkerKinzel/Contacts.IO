using System.Diagnostics;
using System.Globalization;
using System.Text;
using FolkerKinzel.CsvTools.Helpers;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook;

internal class OutlookCsvReader : CsvReader
{
    internal OutlookCsvReader(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider ?? CultureInfo.CurrentCulture, textEncoding) { }

    protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
    {
        Debug.Assert(Analyzer.HasHeaderRow == true);
        Debug.Assert(Analyzer.ColumnNames != null);

        IList<Tuple<string, ContactProp?, IList<string>>>? mapping = HeaderRow.GetMappingEN();

        if (Analyzer.ColumnNames.Where(s => mapping.Any(tpl => tpl.Item2.HasValue && StringComparer.OrdinalIgnoreCase.Equals(tpl.Item3[0], s))).Count() > 3)
        {
            return mapping;
        }
        else
        {
            // Spaltenreihenfolge

            var end = Math.Min(mapping.Count, Analyzer.ColumnNames.Count);

            for (int i = 0; i < end; i++)
            {
                mapping[i].Item3[0] = Analyzer.ColumnNames[i];
            }

            for (int i = end; i < mapping.Count; i++)
            {
                Tuple<string, ContactProp?, IList<string>>? currentTpl = mapping[i];
                mapping[i] = new Tuple<string, ContactProp?, IList<string>>(currentTpl.Item1, null, EmptyStringArray);
            }

        }

        return mapping;
    }

    protected override SexConverter InitSexConverter() => new OutlookSexConverter();


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
                    Address? adrWork = contact.Work.AddressWork;
                    adrWork ??= new Address();
                    contact.Work.AddressWork = adrWork;

                    adrWork.Street += $" {(string?)wrapper[index]}";
                }
                break;
            case AdditionalProp.HomeStreet2:
            case AdditionalProp.HomeStreet3:
                {
                    Address? adrHome = contact.AddressHome ?? new Address();
                    contact.AddressHome = adrHome;

                    adrHome.Street += $" {(string?)wrapper[index]}";
                }
                break;
            case AdditionalProp.AssistantsPhone:
            case AdditionalProp.BusinessPhone2:
            case AdditionalProp.CompanyMainPhone:
                {
                    AddPhoneNumber(contact, new PhoneNumber((string?)wrapper[index], true));
                }
                break;
            case AdditionalProp.HomePhone2:
            case AdditionalProp.ISDN:
            case AdditionalProp.RadioPhone:
            case AdditionalProp.TTY_TDD_Phone:
                {
                    AddPhoneNumber(contact, new PhoneNumber((string?)wrapper[index]));
                }
                break;
            case AdditionalProp.OtherFax:
                {
                    AddPhoneNumber(contact, new PhoneNumber((string?)wrapper[index], isFax: true));
                }
                break;

            default:
                break;
        }
    }
}
