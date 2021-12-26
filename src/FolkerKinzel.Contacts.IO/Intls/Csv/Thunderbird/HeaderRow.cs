namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird;

internal static class HeaderRow
{
    private static class En
    {
        internal const string FirstName = "First Name";
        internal const string LastName = "Last Name";
        internal const string DisplayName = "Display Name";
        internal const string Nickname = "Nickname";
        internal const string PrimaryEmail = "Primary Email";
        internal const string SecondaryEmail = "Secondary Email";
        internal const string ScreenName = "Screen Name";
        internal const string WorkPhone = "Work Phone";
        internal const string HomePhone = "Home Phone";
        internal const string FaxNumber = "Fax Number";
        internal const string PagerNumber = "Pager Number";
        internal const string MobileNumber = "Mobile Number";
        internal const string HomeAddress = "Home Address";
        internal const string HomeAddress2 = "Home Address2";
        internal const string HomeCity = "Home City";
        internal const string HomeState = "Home State";
        internal const string HomeZipcode = "Home Zipcode";
        internal const string HomeCountry = "Home Country";
        internal const string WorkAddress = "Work Address";
        internal const string WorkAddress2 = "Work Address2";
        internal const string WorkCity = "Work City";
        internal const string WorkState = "Work State";
        internal const string WorkZip = "Work Zip";
        internal const string WorkCountry = "Work Country";
        internal const string JobTitle = "Job Title";
        internal const string Department = "Department";
        internal const string Organization = "Organization";
        internal const string WebPage1 = "Web Page 1";
        internal const string WebPage2 = "Web Page 2";
        internal const string BirthYear = "Birth Year";
        internal const string BirthMonth = "Birth Month";
        internal const string BirthDay = "Birth Day";
        internal const string Custom1 = "Custom 1";
        internal const string Custom2 = "Custom 2";
        internal const string Custom3 = "Custom 3";
        internal const string Custom4 = "Custom 4";
        internal const string Notes = "Notes";
    }

    internal static IList<Tuple<string, ContactProp?, IList<string>>> GetMappingEN() => new Tuple<string, ContactProp?, IList<string>>[]
    {
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.FirstName),      ContactProp.FirstName,                   new string[]{En.FirstName}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.LastName),       ContactProp.LastName,                    new string[]{En.LastName}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.DisplayName),    ContactProp.DisplayName,                 new string[]{En.DisplayName}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Nickname),       ContactProp.NickName,                    new string[]{En.Nickname}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.PrimaryEmail),   ContactProp.Email1,                      new string[]{En.PrimaryEmail}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.SecondaryEmail), ContactProp.Email2,                      new string[]{En.SecondaryEmail}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.ScreenName),     ContactProp.InstantMessenger1,           new string[]{En.ScreenName}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkPhone),      ContactProp.PhoneWork,                   new string[]{En.WorkPhone}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomePhone),      ContactProp.PhoneHome,                   new string[]{En.HomePhone}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.FaxNumber),      ContactProp.FaxHome,                     new string[]{En.FaxNumber}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.PagerNumber),    ContactProp.PhoneOther1,                 new string[]{En.PagerNumber}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.MobileNumber),   ContactProp.Cell,                        new string[]{En.MobileNumber}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeAddress),    ContactProp.AddressHomeStreet,           new string[]{En.HomeAddress}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeAddress2),   null,                                    new string[]{En.HomeAddress2}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeCity),       ContactProp.AddressHomeCity,             new string[]{En.HomeCity}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeState),      ContactProp.AddressHomeState,            new string[]{En.HomeState}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeZipcode),    ContactProp.AddressHomePostalCode,       new string[]{En.HomeZipcode}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.HomeCountry),    ContactProp.AddressHomeCountry,          new string[]{En.HomeCountry}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkAddress),    ContactProp.AddressWorkStreet,           new string[]{En.WorkAddress}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkAddress2),   null,                                    new string[]{En.WorkAddress2}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkCity),       ContactProp.AddressWorkCity,             new string[]{En.WorkCity}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkState),      ContactProp.AddressWorkState,            new string[]{En.WorkState}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkZip),        ContactProp.AddressWorkPostalCode,       new string[]{En.WorkZip}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WorkCountry),    ContactProp.AddressWorkCountry ,         new string[]{En.WorkCountry}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.JobTitle),       ContactProp.WorkPosition  ,              new string[]{En.JobTitle}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Department),     ContactProp.WorkDepartment  ,            new string[]{En.Department}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Organization),   ContactProp.WorkCompany  ,               new string[]{En.Organization}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WebPage1),       ContactProp.HomePagePersonal  ,          new string[]{En.WebPage1}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.WebPage2),       ContactProp.HomePageWork  ,              new string[]{En.WebPage2}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.BirthYear),      (ContactProp)AdditionalProp.BirthYear,   new string[]{En.BirthYear}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.BirthMonth),     (ContactProp)AdditionalProp.BirthMonth,  new string[]{En.BirthMonth}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.BirthDay),       (ContactProp)AdditionalProp.BirthDay,    new string[]{En.BirthDay}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Custom1),        null,                                    new string[]{En.Custom1}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Custom2),        null,                                    new string[]{En.Custom2}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Custom3),        null,                                    new string[]{En.Custom3}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Custom4),        null,                                    new string[]{En.Custom4}),
            new Tuple<string, ContactProp?,   IList<string>>(nameof(En.Notes ),         ContactProp.Comment,                     new string[]{En.Notes})
    };


    internal static string[] GetColumnNamesEn() => new string[]
    {
            En.FirstName,
            En.LastName,
            En.DisplayName,
            En.Nickname,
            En.PrimaryEmail,
            En.SecondaryEmail,
            En.ScreenName,
            En.WorkPhone,
            En.HomePhone,
            En.FaxNumber,
            En.PagerNumber,
            En.MobileNumber,
            En.HomeAddress,
            En.HomeAddress2,
            En.HomeCity,
            En.HomeState,
            En.HomeZipcode,
            En.HomeCountry,
            En.WorkAddress,
            En.WorkAddress2,
            En.WorkCity,
            En.WorkState,
            En.WorkZip,
            En.WorkCountry,
            En.JobTitle,
            En.Department,
            En.Organization,
            En.WebPage1,
            En.WebPage2,
            En.BirthYear,
            En.BirthMonth,
            En.BirthDay,
            En.Custom1,
            En.Custom2,
            En.Custom3,
            En.Custom4,
            En.Notes
    };





    internal static string[] GetColumnNamesDe() => new string[]
    {
            "Vorname",
            "Nachname",
            "Anzeigename",
            "Spitzname",
            "Primäre E-Mail-Adresse",
            "Sekundäre E-Mail-Adresse",
            "Messenger-Name",
            "Tel. dienstlich",
            "Tel. privat",
            "Fax-Nummer",
            "Pager-Nummer",
            "Mobil-Tel.-Nr.",
            "Privat: Adresse",
            "Privat: Adresse 2",
            "Privat: Ort",
            "Privat: Bundesland",
            "Privat: PLZ",
            "Privat: Land",
            "Dienstlich: Adresse",
            "Dienstlich: Adresse 2",
            "Dienstlich: Ort",
            "Dienstlich: Bundesland",
            "Dienstlich: PLZ",
            "Dienstlich: Land",
            "Arbeitstitel",
            "Abteilung",
            "Organisation",
            "Webseite 1",
            "Webseite 2",
            "Geburtsjahr",
            "Geburtsmonat",
            "Geburtstag",
            "Benutzerdef. 1",
            "Benutzerdef. 2",
            "Benutzerdef. 3",
            "Benutzerdef. 4",
            "Notizen"
    };
}
