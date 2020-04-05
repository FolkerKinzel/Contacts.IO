using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal class UniversalCsvReader : CsvReader
    {

        internal UniversalCsvReader(Encoding? textEncoding) : base(textEncoding) { }


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => new Tuple<string, ContactProp?, IList<string>>[]
        {
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.DisplayName),           ContactProp.DisplayName,           new string[]{"Display?Name", "Name", "*zeig*name"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FirstName),             ContactProp.FirstName,             new string[]{"First?Name", "Given?Name", "Vorname"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.MiddleName),            ContactProp.MiddleName,            new string[]{"Middle?Name", "Additional?Name", "Second?Name", "2.*name"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.LastName),              ContactProp.LastName,              new string[]{"Last?Name", "Fam*Name", "Nachname"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NamePrefix),            ContactProp.NamePrefix,            new string[]{"*Pr?fix*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NameSuffix),            ContactProp.NameSuffix,            new string[]{"*Suffix*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NickName),              ContactProp.NickName,              new string[]{ColumnName.NickName, "Spitzname", "Rufname"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Gender),                ContactProp.Gender,                new string[]{ColumnName.Gender, "Sex", "Geschlecht"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.BirthDay),              ContactProp.BirthDay,              new string[]{ColumnName.BirthDay, "Geburtstag"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Spouse),                ContactProp.Spouse,                new string[]{"*Spouse", "*Partner"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Anniversary),           ContactProp.Anniversary,           new string[]{"*Anniversary", "*Jubil*", "Hochzeitstag"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeStreet),     ContactProp.AddressHomeStreet,     new string[]{"*Str*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomePostalCode), ContactProp.AddressHomePostalCode, new string[]{"*Postal*", "*Zip*", "*PLZ*", "*Postleit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCity),       ContactProp.AddressHomeCity,       new string[]{"*City*", "*Locality*", "*Ort*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeState),      ContactProp.AddressHomeState,      new string[]{"*State*", "*Bundesl*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCountry),    ContactProp.AddressHomeCountry,    new string[]{"*Country*", "*Region*", "*Staat*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email1),                ContactProp.Email1,                new string[]{"*E?mail*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email2),                ContactProp.Email2,                new string[]{"*E?mail*2*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email3),                ContactProp.Email3,                new string[]{"*E?mail*3*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email4),                ContactProp.Email4,                new string[]{"*E?mail*4*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email5),                ContactProp.Email5,                new string[]{"*E?mail*5*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email6),                ContactProp.Email6,                new string[]{"*E?mail*6*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneHome),             ContactProp.PhoneHome,             new string[]{"*Phon*", "*Tel*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneWork),             ContactProp.PhoneWork,             new string[]{ "*Work*Phon*", "*Bus*Phon*", "*Phon*Work*", "*Phon*Bus*", "*Tel*Geschäft*", "*Geschäft*Tel*", "*Tel*Arbeit*", "*Arbeit*Tel*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FaxHome),               ContactProp.FaxHome,               new string[]{"*Fax*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FaxWork),               ContactProp.FaxWork,               new string[]{"*Work*Fax*", "*Bus*Fax*", "*Fax*Work*", "*Fax*Bus*", "*Fax*Geschäft*", "*Geschäft*Fax*", "*Fax*Arbeit*", "*Arbeit*Fax*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Cell),                  ContactProp.Cell,                  new string[]{"*Mobil*", "*Cell*", "*Handy*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.CellWork),              ContactProp.CellWork,              new string[]{"*Pager*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther1),           ContactProp.PhoneOther1,           new string[]{"Other*Phon*", "Phon*Other*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther2),           ContactProp.PhoneOther2,           new string[]{"*Phon*2*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther3),           ContactProp.PhoneOther3,           new string[]{"*Phon*3*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther4),           ContactProp.PhoneOther4,           new string[]{"*Phon*4*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther5),           ContactProp.PhoneOther5,           new string[]{"*Phon*5*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther6),           ContactProp.PhoneOther6,           new string[]{"*Phon*6*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger1),     ContactProp.InstantMessenger1,     new string[]{"*Inst*Mess*", "*Chat*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger2),     ContactProp.InstantMessenger2,     new string[]{"*Inst*Mess*2*", "*Chat*2*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger3),     ContactProp.InstantMessenger3,     new string[]{"*Inst*Mess*3*", "*Chat*3*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger4),     ContactProp.InstantMessenger4,     new string[]{"*Inst*Mess*4*", "*Chat*4*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger5),     ContactProp.InstantMessenger5,     new string[]{"*Inst*Mess*5*", "*Chat*5*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger6),     ContactProp.InstantMessenger6,     new string[]{"*Inst*Mess*6*", "*Chat*6*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebPagePersonal),       ContactProp.HomePagePersonal,      new string[]{"*Web*", "*page*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebPageWork),           ContactProp.HomePageWork,          new string[]{ "*Work*Web*", "*Bus*Web*", "*Web*Work*", "*Web*Bus*", "*Web*Geschäft*", "*Geschäft*Web*", "*Web*Arbeit*", "*Arbeit*Web*",
                                                                                                                                                       "*Work*page*", "*Bus*page*", "*page*Work*", "*page*Bus*", "*page*Geschäft*", "*Geschäft*page*", "*page*Arbeit*", "*Arbeit*page*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkCompany),           ContactProp.WorkCompany,           new string[]{"*Company", "Org*", "Firma"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkDepartment),        ContactProp.WorkDepartment,        new string[]{"*Department*", "*Abteilung*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkOffice),            ContactProp.WorkOffice,            new string[]{"*Office*", "*Büro*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkJobTitle),          ContactProp.WorkPosition,          new string[]{"*Job?Title", "Organization*Title", "Work*Title", "*Position"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkStreet),     ContactProp.AddressWorkStreet,     new string[]{"*Work*Str*", "*Bus*Str*", "*Str*Work*", "*Str*Bus*", "*Geschäft*Str*", "*Str*Geschäft*", "*Arbeit*Str*", "*Str*Arbeit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkPostalCode), ContactProp.AddressWorkPostalCode, new string[]{"*Work*Postal*","*Bus*Postal*","*Postal*Work*", "*Postal*Bus*",
                                                                                                                                                     "*Work*Zip*","*Bus*Zip*","*Zip*Work*", "*Zip*Bus*",
                                                                                                                                                      "*Geschäft*PLZ*", "*PLZ*Geschäft*","*Arbeit*PLZ*", "*PLZ*Arbeit*",
                                                                                                                                                      "*Geschäft*Postleit*", "*Postleit*Geschäft*","*Arbeit*Postleit*", "*Postleit*Arbeit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCity),       ContactProp.AddressWorkCity,       new string[]{"*Work*City*", "*Bus*City*", "*City*Work*", "*City*Bus*",
                                                                                                                                                     "*Work*Locality*", "*Bus*Locality*", "*Locality*Work*", "*Locality*Bus*",
                                                                                                                                                      "*Geschäft*Ort*", "*Ort*Geschäft*", "*Arbeit*Ort*", "*Ort*Arbeit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkState),      ContactProp.AddressWorkState,      new string[]{"*Work*State*", "*State*Work*", "*Bus*State*", "*State*Bus*",
                                                                                                                                                    "*Geschäft*Bundesl*", "*Bundesl*Geschäft*", "*Arbeit*Bundesl*", "*Bundesl*Arbeit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCountry),    ContactProp.AddressWorkCountry,    new string[]{"*Work*Country*", "*Country*Work*", "*Bus*Country*", "*Country*Bus*",
                                                                                                                                                     "*Work*Region*", "*Region*Work*", "*Bus*Region*", "*Region*Bus*",
                                                                                                                                                     "*Geschäft*Region*", "*Region*Geschäft*", "*Arbeit*Region*", "*Region*Arbeit*",
                                                                                                                                                     "*Geschäft*Staat*", "*Staat*Geschäft*", "*Arbeit*Staat*", "*Staat*Arbeit*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Comment),               ContactProp.Comment,               new string[]{"Note?", "Comment?", "Annotation?", "Kommentar*", "Anmerkung*", "Notiz*"}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.TimeStamp),             ContactProp.TimeStamp,             new string[]{ColumnName.TimeStamp, "*Change*", "*Revision*", "*Änder*"})
        };



    }
}
