using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal;

internal class UniversalCsvReader : CsvReader
{
    internal UniversalCsvReader(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider, textEncoding) { }

    [SuppressMessage("Style", "IDE0300:Simplify collection initialization", Justification = "Performance")]
    [SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "Too much data for a singleton.")]
    protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() =>
    [
            new(nameof(ColumnName.DisplayName),           ContactProp.DisplayName,           new string[]{"Display?Name", "Name", "*zeig*name"}),
            new(nameof(ColumnName.FirstName),             ContactProp.FirstName,             new string[]{"First?Name", "Given?Name", "Vorname"}),
            new(nameof(ColumnName.MiddleName),            ContactProp.MiddleName,            new string[]{"Middle?Name", "Additional?Name", "Second?Name", "2.*name"}),
            new(nameof(ColumnName.LastName),              ContactProp.LastName,              new string[]{"Last?Name", "Fam*Name", "Nachname"}),
            new(nameof(ColumnName.NamePrefix),            ContactProp.NamePrefix,            new string[]{"*Pr?fix*"}),
            new(nameof(ColumnName.NameSuffix),            ContactProp.NameSuffix,            new string[]{"*Suffix*"}),
            new(nameof(ColumnName.NickName),              ContactProp.NickName,              new string[]{ColumnName.NickName, "Spitzname", "Rufname"}),
            new(nameof(ColumnName.Gender),                ContactProp.Gender,                new string[]{ColumnName.Gender, "Sex", "Geschlecht"}),
            new(nameof(ColumnName.BirthDay),              ContactProp.BirthDay,              new string[]{ColumnName.BirthDay, "Geburtstag"}),
            new(nameof(ColumnName.Spouse),                ContactProp.Spouse,                new string[]{"*Spouse", "*Partner"}),
            new(nameof(ColumnName.Anniversary),           ContactProp.Anniversary,           new string[]{"*Anniversary", "*Jubil*", "Hochzeitstag"}),
            new(nameof(ColumnName.AddressHomeStreet),     ContactProp.AddressHomeStreet,     new string[]{"*Str*"}),
            new(nameof(ColumnName.AddressHomePostalCode), ContactProp.AddressHomePostalCode, new string[]{"*Postal*", "*Zip*", "*PLZ*", "*Postleit*"}),
            new(nameof(ColumnName.AddressHomeCity),       ContactProp.AddressHomeCity,       new string[]{"*City*", "*Locality*", "*Ort*"}),
            new(nameof(ColumnName.AddressHomeState),      ContactProp.AddressHomeState,      new string[]{"*State*", "*Bundesl*"}),
            new(nameof(ColumnName.AddressHomeCountry),    ContactProp.AddressHomeCountry,    new string[]{"*Country*", "*Region*", "*Staat*"}),
            new(nameof(ColumnName.Email1),                ContactProp.Email1,                new string[]{"*E?mail*"}),
            new(nameof(ColumnName.Email2),                ContactProp.Email2,                new string[]{"*E?mail*2*"}),
            new(nameof(ColumnName.Email3),                ContactProp.Email3,                new string[]{"*E?mail*3*"}),
            new(nameof(ColumnName.Email4),                ContactProp.Email4,                new string[]{"*E?mail*4*"}),
            new(nameof(ColumnName.Email5),                ContactProp.Email5,                new string[]{"*E?mail*5*"}),
            new(nameof(ColumnName.Email6),                ContactProp.Email6,                new string[]{"*E?mail*6*"}),
            new(nameof(ColumnName.PhoneHome),             ContactProp.PhoneHome,             new string[]{"*Phon*", "*Tel*"}),
            new(nameof(ColumnName.PhoneWork),             ContactProp.PhoneWork,             new string[]{ "*Work*Phon*", "*Bus*Phon*", "*Phon*Work*", "*Phon*Bus*", "*Tel*Geschäft*", "*Geschäft*Tel*", "*Tel*Arbeit*", "*Arbeit*Tel*"}),
            new(nameof(ColumnName.FaxHome),               ContactProp.FaxHome,               new string[]{"*Fax*"}),
            new(nameof(ColumnName.FaxWork),               ContactProp.FaxWork,               new string[]{"*Work*Fax*", "*Bus*Fax*", "*Fax*Work*", "*Fax*Bus*", "*Fax*Geschäft*", "*Geschäft*Fax*", "*Fax*Arbeit*", "*Arbeit*Fax*"}),
            new(nameof(ColumnName.Cell),                  ContactProp.Cell,                  new string[]{"*Mobil*", "*Cell*", "*Handy*"}),
            new(nameof(ColumnName.CellWork),              ContactProp.CellWork,              new string[]{"*Pager*"}),
            new(nameof(ColumnName.PhoneOther1),           ContactProp.PhoneOther1,           new string[]{"Other*Phon*", "Phon*Other*"}),
            new(nameof(ColumnName.PhoneOther2),           ContactProp.PhoneOther2,           new string[]{"*Phon*2*"}),
            new(nameof(ColumnName.PhoneOther3),           ContactProp.PhoneOther3,           new string[]{"*Phon*3*"}),
            new(nameof(ColumnName.PhoneOther4),           ContactProp.PhoneOther4,           new string[]{"*Phon*4*"}),
            new(nameof(ColumnName.PhoneOther5),           ContactProp.PhoneOther5,           new string[]{"*Phon*5*"}),
            new(nameof(ColumnName.PhoneOther6),           ContactProp.PhoneOther6,           new string[]{"*Phon*6*"}),
            new(nameof(ColumnName.InstantMessenger1),     ContactProp.InstantMessenger1,     new string[]{"*Inst*Mess*", "*Chat*"}),
            new(nameof(ColumnName.InstantMessenger2),     ContactProp.InstantMessenger2,     new string[]{"*Inst*Mess*2*", "*Chat*2*"}),
            new(nameof(ColumnName.InstantMessenger3),     ContactProp.InstantMessenger3,     new string[]{"*Inst*Mess*3*", "*Chat*3*"}),
            new(nameof(ColumnName.InstantMessenger4),     ContactProp.InstantMessenger4,     new string[]{"*Inst*Mess*4*", "*Chat*4*"}),
            new(nameof(ColumnName.InstantMessenger5),     ContactProp.InstantMessenger5,     new string[]{"*Inst*Mess*5*", "*Chat*5*"}),
            new(nameof(ColumnName.InstantMessenger6),     ContactProp.InstantMessenger6,     new string[]{"*Inst*Mess*6*", "*Chat*6*"}),
            new(nameof(ColumnName.WebPagePersonal),       ContactProp.HomePagePersonal,      new string[]{"*Web*", "*page*"}),
            new(nameof(ColumnName.WebPageWork),           ContactProp.HomePageWork,          new string[]{ "*Work*Web*", "*Bus*Web*", "*Web*Work*", "*Web*Bus*", "*Web*Geschäft*", "*Geschäft*Web*", "*Web*Arbeit*", "*Arbeit*Web*",
                                                                                                                                                       "*Work*page*", "*Bus*page*", "*page*Work*", "*page*Bus*", "*page*Geschäft*", "*Geschäft*page*", "*page*Arbeit*", "*Arbeit*page*"}),
            new(nameof(ColumnName.WorkCompany),           ContactProp.WorkCompany,           new string[]{"*Company", "Org*", "Firma"}),
            new(nameof(ColumnName.WorkDepartment),        ContactProp.WorkDepartment,        new string[]{"*Department*", "*Abteilung*"}),
            new(nameof(ColumnName.WorkOffice),            ContactProp.WorkOffice,            new string[]{"*Office*", "*Büro*"}),
            new(nameof(ColumnName.WorkJobTitle),          ContactProp.WorkPosition,          new string[]{"*Job?Title", "Organization*Title", "Work*Title", "*Position"}),
            new(nameof(ColumnName.AddressWorkStreet),     ContactProp.AddressWorkStreet,     new string[]{"*Work*Str*", "*Bus*Str*", "*Str*Work*", "*Str*Bus*", "*Geschäft*Str*", "*Str*Geschäft*", "*Arbeit*Str*", "*Str*Arbeit*"}),
            new(nameof(ColumnName.AddressWorkPostalCode), ContactProp.AddressWorkPostalCode, new string[]{"*Work*Postal*","*Bus*Postal*","*Postal*Work*", "*Postal*Bus*",
                                                                                                                                                     "*Work*Zip*","*Bus*Zip*","*Zip*Work*", "*Zip*Bus*",
                                                                                                                                                      "*Geschäft*PLZ*", "*PLZ*Geschäft*","*Arbeit*PLZ*", "*PLZ*Arbeit*",
                                                                                                                                                      "*Geschäft*Postleit*", "*Postleit*Geschäft*","*Arbeit*Postleit*", "*Postleit*Arbeit*"}),
            new(nameof(ColumnName.AddressWorkCity),       ContactProp.AddressWorkCity,       new string[]{"*Work*City*", "*Bus*City*", "*City*Work*", "*City*Bus*",
                                                                                                                                                     "*Work*Locality*", "*Bus*Locality*", "*Locality*Work*", "*Locality*Bus*",
                                                                                                                                                      "*Geschäft*Ort*", "*Ort*Geschäft*", "*Arbeit*Ort*", "*Ort*Arbeit*"}),
            new(nameof(ColumnName.AddressWorkState),      ContactProp.AddressWorkState,      new string[]{"*Work*State*", "*State*Work*", "*Bus*State*", "*State*Bus*",
                                                                                                                                                    "*Geschäft*Bundesl*", "*Bundesl*Geschäft*", "*Arbeit*Bundesl*", "*Bundesl*Arbeit*"}),
            new(nameof(ColumnName.AddressWorkCountry),    ContactProp.AddressWorkCountry,    new string[]{"*Work*Country*", "*Country*Work*", "*Bus*Country*", "*Country*Bus*",
                                                                                                                                                     "*Work*Region*", "*Region*Work*", "*Bus*Region*", "*Region*Bus*",
                                                                                                                                                     "*Geschäft*Region*", "*Region*Geschäft*", "*Arbeit*Region*", "*Region*Arbeit*",
                                                                                                                                                     "*Geschäft*Staat*", "*Staat*Geschäft*", "*Arbeit*Staat*", "*Staat*Arbeit*"}),
            new(nameof(ColumnName.Comment),               ContactProp.Comment,               new string[]{"Note?", "Comment?", "Annotation?", "Kommentar*", "Anmerkung*", "Notiz*"}),
            new(nameof(ColumnName.TimeStamp),             ContactProp.TimeStamp,             new string[]{ColumnName.TimeStamp, "*Change*", "*Revision*", "*Änder*"})
    ];

}
