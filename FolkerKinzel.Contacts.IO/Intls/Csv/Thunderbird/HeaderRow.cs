using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Thunderbird
{
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

        internal static Tuple<string, ContactProp, string>[] GetMappingEN() => new Tuple<string, ContactProp, string>[]
        {
            new Tuple<string, ContactProp, string>(nameof(En.FirstName),      ContactProp.FirstName  , En.FirstName  ),
            new Tuple<string, ContactProp, string>(nameof(En.LastName),       ContactProp.LastName  ,  En.LastName ), 
            new Tuple<string, ContactProp, string>(nameof(En.DisplayName),    ContactProp.DisplayName  ,  En.DisplayName ),
            new Tuple<string, ContactProp, string>(nameof(En.Nickname),       ContactProp.NickName  ,  En.Nickname ),
            new Tuple<string, ContactProp, string>(nameof(En.PrimaryEmail),   ContactProp.Email1  ,  En.PrimaryEmail ),  
            new Tuple<string, ContactProp, string>(nameof(En.SecondaryEmail), ContactProp.Email2  ,  En.SecondaryEmail ),   
            //new Tuple<string, ContactProp, string>(nameof(En.ScreenName),     ContactProp  ,   En.ScreenName), 
            new Tuple<string, ContactProp, string>(nameof(En.WorkPhone),      ContactProp.PhoneWork  ,  En.WorkPhone ), 
            new Tuple<string, ContactProp, string>(nameof(En.HomePhone),      ContactProp.PhoneHome  ,  En.HomePhone ), 
            new Tuple<string, ContactProp, string>(nameof(En.FaxNumber),      ContactProp.FaxHome  ,  En.FaxNumber ), 
            new Tuple<string, ContactProp, string>(nameof(En.PagerNumber),    ContactProp.PhoneOther1  ,  En.PagerNumber ),
            new Tuple<string, ContactProp, string>(nameof(En.MobileNumber),   ContactProp.Cell  ,  En.MobileNumber ),
            new Tuple<string, ContactProp, string>(nameof(En.HomeAddress),    ContactProp.AddressHomeStreet  ,  En.HomeAddress ),
            //new Tuple<string, ContactProp, string>(nameof(En.HomeAddress2),   ContactProp  ,  En.HomeAddress2 ),
            new Tuple<string, ContactProp, string>(nameof(En.HomeCity),       ContactProp.AddressHomeCity  ,  En.HomeCity ),
            new Tuple<string, ContactProp, string>(nameof(En.HomeState),      ContactProp.AddressHomeState  ,  En.HomeState ),
            new Tuple<string, ContactProp, string>(nameof(En.HomeZipcode),    ContactProp.AddressHomePostalCode  ,  En.HomeZipcode ),
            new Tuple<string, ContactProp, string>(nameof(En.HomeCountry),    ContactProp.AddressHomeCountry  ,  En.HomeCountry ),
            new Tuple<string, ContactProp, string>(nameof(En.WorkAddress),    ContactProp.AddressWorkStreet  ,  En.WorkAddress ),
            //new Tuple<string, ContactProp, string>(nameof(En.WorkAddress2),   ContactProp  ,  En.WorkAddress2 ),
            new Tuple<string, ContactProp, string>(nameof(En.WorkCity),       ContactProp.AddressWorkCity  ,  En.WorkCity ),
            new Tuple<string, ContactProp, string>(nameof(En.WorkState),      ContactProp.AddressWorkState  ,  En.WorkState ),
            new Tuple<string, ContactProp, string>(nameof(En.WorkZip),        ContactProp.AddressWorkPostalCode  ,  En.WorkZip ),
            new Tuple<string, ContactProp, string>(nameof(En.WorkCountry),    ContactProp.AddressWorkCountry  ,  En.WorkCountry ),
            new Tuple<string, ContactProp, string>(nameof(En.JobTitle),       ContactProp.WorkPosition  ,  En.JobTitle ),
            new Tuple<string, ContactProp, string>(nameof(En.Department),     ContactProp.WorkDepartment  ,  En.Department ),
            new Tuple<string, ContactProp, string>(nameof(En.Organization),   ContactProp.WorkCompany  ,   En.Organization),
            new Tuple<string, ContactProp, string>(nameof(En.WebPage1),       ContactProp.HomePagePersonal  ,  En.WebPage1 ),
            new Tuple<string, ContactProp, string>(nameof(En.WebPage2),       ContactProp.HomePageWork  ,  En.WebPage2 ),
            new Tuple<string, ContactProp, string>(nameof(En.BirthYear),      (ContactProp)AdditionalProps.BirthYear  ,  En.BirthYear ),
            new Tuple<string, ContactProp, string>(nameof(En.BirthMonth),     (ContactProp)AdditionalProps.BirthMonth  ,  En.BirthMonth ),
            new Tuple<string, ContactProp, string>(nameof(En.BirthDay),       (ContactProp)AdditionalProps.BirthDay  ,  En.BirthDay ),
            //new Tuple<string, ContactProp, string>(nameof(En.Custom1),        ContactProp  ,  En.Custom1 ),
            //new Tuple<string, ContactProp, string>(nameof(En.Custom2),        ContactProp  ,  En.Custom2 ),
            //new Tuple<string, ContactProp, string>(nameof(En.Custom3),        ContactProp  ,  En.Custom3 ),
            //new Tuple<string, ContactProp, string>(nameof(En.Custom4),        ContactProp  ,  En.Custom4 ),
            new Tuple<string, ContactProp, string>(nameof(En.Notes ),          ContactProp.Comment  ,  En.Notes )
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
}
