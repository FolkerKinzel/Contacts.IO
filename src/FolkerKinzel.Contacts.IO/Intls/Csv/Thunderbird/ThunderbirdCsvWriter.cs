using System.Text;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird;

internal class ThunderbirdCsvWriter : CsvWriter
{
    private readonly ICsvTypeConverter _intConverter;

    internal ThunderbirdCsvWriter(IFormatProvider? formatProvider, Encoding? textEncoding)
        : base(formatProvider, textEncoding)
        => _intConverter = CsvConverterFactory.CreateConverter(CsvTypeCode.Int32,
                                                               nullable: true,
                                                               formatProvider: this.FormatProvider);


    protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


    protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();


    protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
    {
        Debug.Assert(tpl.Item2.HasValue);

        //case (ContactProp)AdditionalProps.BirthYear:
        //case (ContactProp)AdditionalProps.BirthMonth:
        //case (ContactProp)AdditionalProps.BirthDay:


        wrapper.AddProperty(
                    new CsvProperty(
                        tpl.Item1,
                        tpl.Item3,
                        _intConverter));
    }


    protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
    {
        DateTime? _birthDay = contact.Person?.BirthDay;

        wrapper[index] = _birthDay.HasValue ? prop switch
        {
            (ContactProp)AdditionalProp.BirthYear => _birthDay.Value.Year,
            (ContactProp)AdditionalProp.BirthMonth => _birthDay.Value.Month,
            (ContactProp)AdditionalProp.BirthDay => _birthDay.Value.Day,
            _ => null
        } : (int?)null;

    }



}
