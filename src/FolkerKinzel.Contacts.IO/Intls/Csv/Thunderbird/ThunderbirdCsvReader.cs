using System.Text;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird;

internal class ThunderbirdCsvReader : CsvReader
{
    private readonly Conv::ICsvTypeConverter _intConverter;


    internal ThunderbirdCsvReader(IFormatProvider? formatProvider, Encoding? textEncoding)
        : base(formatProvider, textEncoding)
        => _intConverter = Conv::CsvConverterFactory.CreateConverter(Conv.CsvTypeCode.Int32,
                                                                     nullable: false,
                                                                     formatProvider: this.FormatProvider);

    protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
    {
        Debug.Assert(Analyzer.HasHeaderRow == true);
        Debug.Assert(Analyzer.ColumnNames != null);


        // englische Spaltennamen
        IList<Tuple<string, ContactProp?, IList<string>>>? mapping = HeaderRow.GetMappingEN();

        if (Analyzer.ColumnNames.Where(s => mapping.Any(tpl => tpl.Item2.HasValue && StringComparer.OrdinalIgnoreCase.Equals(tpl.Item3[0], s))).Count() > 3)
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
        }


        return mapping;
    }

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

    protected override void InitContactNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
    {
        var value = (int)wrapper[index]!;

        if (value <= 0)
        {
            return;
        }

        Person? person = contact.Person ?? new Person();
        contact.Person = person;

        DateTime? birthDay = person.BirthDay;

        // Der CsvRecordWrapper bestimmt die Reihenfolge der Auswertung. Deshalb wird immer zuerst Year, dann Month, dann Day initialisiert.

        try
        {
            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.BirthYear:
                    person.BirthDay = new DateTime(value, 1, 1);
                    break;
                case AdditionalProp.BirthMonth:
                    person.BirthDay = birthDay.HasValue
                                        ? (DateTime?)new DateTime(birthDay.Value.Year, value, birthDay.Value.Day)
                                        : (DateTime?)new DateTime(1, value, 1);
                    break;
                case AdditionalProp.BirthDay:
                    if (birthDay.HasValue)
                    {
                        person.BirthDay = new DateTime(birthDay.Value.Year, birthDay.Value.Month, value);
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
