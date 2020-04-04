using System;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal static class HeaderRow
    {
        private static class ColumnName
        {
            internal const string DisplayName = "Display Name";
            internal const string FirstName = "First Name";
            internal const string MiddleName = "Middle Name";
            internal const string LastName = "Last Name";
            internal const string NamePrefix = "Name Prefix";
            internal const string NameSuffix = "Name Suffix";
            internal const string NickName = "Nickname";
            internal const string Gender = "Gender";
            internal const string BirthDay = "Birthday";
            internal const string Spouse = "Spouse";
            internal const string Anniversary = "Anniversary";
            internal const string AddressHomeStreet = "Home Street";
            internal const string AddressHomePostalCode = "Home Postal Code";
            internal const string AddressHomeCity = "Home City";
            internal const string AddressHomeState = "Home State";
            internal const string AddressHomeCountry = "Home Country";
            internal const string Email1 = "E-mail 1";
            internal const string Email2 = "E-mail 2";
            internal const string Email3 = "E-mail 3";
            internal const string Email4 = "E-mail 4";
            internal const string Email5 = "E-mail 5";
            internal const string Email6 = "E-mail 6";
            internal const string PhoneHome = "Phone";
            internal const string PhoneWork = "Work Phone";
            internal const string FaxHome = "Fax";
            internal const string FaxWork = "Work Fax";
            internal const string Cell = "Mobile Phone";
            internal const string CellWork = "Work Mobile Phone";
            internal const string PhoneOther1 = "Other Phone 1";
            internal const string PhoneOther2 = "Other Phone 2";
            internal const string PhoneOther3 = "Other Phone 3";
            internal const string PhoneOther4 = "Other Phone 4";
            internal const string PhoneOther5 = "Other Phone 5";
            internal const string PhoneOther6 = "Other Phone 6";
            internal const string InstantMessenger1 = "InstantMessenger1";
            internal const string InstantMessenger2 = "InstantMessenger2";
            internal const string InstantMessenger3 = "InstantMessenger3";
            internal const string InstantMessenger4 = "InstantMessenger4";
            internal const string InstantMessenger5 = "InstantMessenger5";
            internal const string InstantMessenger6 = "InstantMessenger6";
            internal const string WebPagePersonal = "Web Page";
            internal const string WebPageWork = "Work Web Page";
            internal const string WorkCompany = "Company";
            internal const string WorkDepartment = "Department";
            internal const string WorkOffice = "Office";
            internal const string WorkJobTitle = "Job Title";
            internal const string AddressWorkStreet = "Work Street";
            internal const string AddressWorkPostalCode = "Work Postal Code";
            internal const string AddressWorkCity = "Work City";
            internal const string AddressWorkState = "Work State";
            internal const string AddressWorkCountry = "Work Country";
            internal const string Comment = "Notes";
            internal const string TimeStamp = "Timestamp";
        }


        internal static string[] GetColumnNames() => new string[]
        {
            ColumnName.DisplayName,
            ColumnName.FirstName,
            ColumnName.MiddleName,
            ColumnName.LastName,
            ColumnName.NamePrefix,
            ColumnName.NameSuffix,
            ColumnName.NickName,
            ColumnName.Gender,
            ColumnName.BirthDay,
            ColumnName.Spouse,
            ColumnName.Anniversary,
            ColumnName.AddressHomeStreet,
            ColumnName.AddressHomePostalCode,
            ColumnName.AddressHomeCity,
            ColumnName.AddressHomeState,
            ColumnName.AddressHomeCountry,
            ColumnName.Email1,
            ColumnName.Email2,
            ColumnName.Email3,
            ColumnName.Email4,
            ColumnName.Email5,
            ColumnName.Email6,
            ColumnName.PhoneHome,
            ColumnName.PhoneWork,
            ColumnName.FaxHome,
            ColumnName.FaxWork,
            ColumnName.Cell,
            ColumnName.CellWork,
            ColumnName.PhoneOther1,
            ColumnName.PhoneOther2,
            ColumnName.PhoneOther3,
            ColumnName.PhoneOther4,
            ColumnName.PhoneOther5,
            ColumnName.PhoneOther6,
            ColumnName.InstantMessenger1,
            ColumnName.InstantMessenger2,
            ColumnName.InstantMessenger3,
            ColumnName.InstantMessenger4,
            ColumnName.InstantMessenger5,
            ColumnName.InstantMessenger6,
            ColumnName.WebPagePersonal,
            ColumnName.WebPageWork,
            ColumnName.WorkCompany,
            ColumnName.WorkDepartment,
            ColumnName.WorkOffice,
            ColumnName.WorkJobTitle,
            ColumnName.AddressWorkStreet,
            ColumnName.AddressWorkPostalCode,
            ColumnName.AddressWorkCity,
            ColumnName.AddressWorkState,
            ColumnName.AddressWorkCountry,
            ColumnName.Comment,
            ColumnName.TimeStamp
        };

    }
}
