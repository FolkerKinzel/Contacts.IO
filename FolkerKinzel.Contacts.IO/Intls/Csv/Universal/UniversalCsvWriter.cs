using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{

    internal class UniversalCsvWriter : CsvWriter
    {
        internal UniversalCsvWriter(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider, textEncoding) { }

        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNames();

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => new Tuple<string, ContactProp?, IList<string>>[]
        {
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.DisplayName),           ContactProp.DisplayName,           new string[]{ColumnName.DisplayName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FirstName),             ContactProp.FirstName,             new string[]{ColumnName.FirstName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.MiddleName),            ContactProp.MiddleName,            new string[]{ColumnName.MiddleName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.LastName),              ContactProp.LastName,              new string[]{ColumnName.LastName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NamePrefix),            ContactProp.NamePrefix,            new string[]{ColumnName.NamePrefix}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NameSuffix),            ContactProp.NameSuffix,            new string[]{ColumnName.NameSuffix}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NickName),              ContactProp.NickName,              new string[]{ColumnName.NickName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Gender),                ContactProp.Gender,                new string[]{ColumnName.Gender}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.BirthDay),              ContactProp.BirthDay,              new string[]{ColumnName.BirthDay}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Spouse),                ContactProp.Spouse,                new string[]{ColumnName.Spouse}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Anniversary),           ContactProp.Anniversary,           new string[]{ColumnName.Anniversary}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeStreet),     ContactProp.AddressHomeStreet,     new string[]{ColumnName.AddressHomeStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomePostalCode), ContactProp.AddressHomePostalCode, new string[]{ColumnName.AddressHomePostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCity),       ContactProp.AddressHomeCity,       new string[]{ColumnName.AddressHomeCity}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeState),      ContactProp.AddressHomeState,      new string[]{ColumnName.AddressHomeState}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCountry),    ContactProp.AddressHomeCountry,    new string[]{ColumnName.AddressHomeCountry}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email1),                ContactProp.Email1,                new string[]{ColumnName.Email1}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email2),                ContactProp.Email2,                new string[]{ColumnName.Email2}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email3),                ContactProp.Email3,                new string[]{ColumnName.Email3}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email4),                ContactProp.Email4,                new string[]{ColumnName.Email4}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email5),                ContactProp.Email5,                new string[]{ColumnName.Email5}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email6),                ContactProp.Email6,                new string[]{ColumnName.Email6}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneHome),             ContactProp.PhoneHome,             new string[]{ColumnName.PhoneHome}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneWork),             ContactProp.PhoneWork,             new string[]{ColumnName.PhoneWork}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FaxHome),               ContactProp.FaxHome,               new string[]{ColumnName.FaxHome}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FaxWork),               ContactProp.FaxWork,               new string[]{ColumnName.FaxWork}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Cell),                  ContactProp.Cell,                  new string[]{ColumnName.Cell}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.CellWork),              ContactProp.CellWork,              new string[]{ColumnName.CellWork}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther1),           ContactProp.PhoneOther1,           new string[]{ColumnName.PhoneOther1}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther2),           ContactProp.PhoneOther2,           new string[]{ColumnName.PhoneOther2}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther3),           ContactProp.PhoneOther3,           new string[]{ColumnName.PhoneOther3}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther4),           ContactProp.PhoneOther4,           new string[]{ColumnName.PhoneOther4}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther5),           ContactProp.PhoneOther5,           new string[]{ColumnName.PhoneOther5}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.PhoneOther6),           ContactProp.PhoneOther6,           new string[]{ColumnName.PhoneOther6}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger1),     ContactProp.InstantMessenger1,     new string[]{ColumnName.InstantMessenger1}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger2),     ContactProp.InstantMessenger2,     new string[]{ColumnName.InstantMessenger2}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger3),     ContactProp.InstantMessenger3,     new string[]{ColumnName.InstantMessenger3}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger4),     ContactProp.InstantMessenger4,     new string[]{ColumnName.InstantMessenger4}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger5),     ContactProp.InstantMessenger5,     new string[]{ColumnName.InstantMessenger5}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.InstantMessenger6),     ContactProp.InstantMessenger6,     new string[]{ColumnName.InstantMessenger6}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebPagePersonal),       ContactProp.HomePagePersonal,      new string[]{ColumnName.WebPagePersonal}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebPageWork),           ContactProp.HomePageWork,          new string[]{ColumnName.WebPageWork}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkCompany),           ContactProp.WorkCompany,           new string[]{ColumnName.WorkCompany}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkDepartment),        ContactProp.WorkDepartment,        new string[]{ColumnName.WorkDepartment}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkOffice),            ContactProp.WorkOffice,            new string[]{ColumnName.WorkOffice}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WorkJobTitle),          ContactProp.WorkPosition,          new string[]{ColumnName.WorkJobTitle}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkStreet),     ContactProp.AddressWorkStreet,     new string[]{ColumnName.AddressWorkStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkPostalCode), ContactProp.AddressWorkPostalCode, new string[]{ColumnName.AddressWorkPostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCity),       ContactProp.AddressWorkCity,       new string[]{ColumnName.AddressWorkCity}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkState),      ContactProp.AddressWorkState,      new string[]{ColumnName.AddressWorkState}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCountry),    ContactProp.AddressWorkCountry,    new string[]{ColumnName.AddressWorkCountry}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Comment),               ContactProp.Comment,               new string[]{ColumnName.Comment}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.TimeStamp),             ContactProp.TimeStamp,             new string[]{ColumnName.TimeStamp})
        };
    }
}
