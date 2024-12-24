using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal;

internal class UniversalCsvWriter : CsvWriter
{
    internal UniversalCsvWriter(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider, textEncoding) { }

    protected override string[] CreateColumnNames() => HeaderRow.GetColumnNames();

    [SuppressMessage("Style", "IDE0300:Simplify collection initialization", Justification = "Performance")]
    protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() =>
    [
            new(nameof(ColumnName.DisplayName),           ContactProp.DisplayName,           new string[]{ColumnName.DisplayName}),
            new(nameof(ColumnName.FirstName),             ContactProp.FirstName,             new string[]{ColumnName.FirstName}),
            new(nameof(ColumnName.MiddleName),            ContactProp.MiddleName,            new string[]{ColumnName.MiddleName}),
            new(nameof(ColumnName.LastName),              ContactProp.LastName,              new string[]{ColumnName.LastName}),
            new(nameof(ColumnName.NamePrefix),            ContactProp.NamePrefix,            new string[]{ColumnName.NamePrefix}),
            new(nameof(ColumnName.NameSuffix),            ContactProp.NameSuffix,            new string[]{ColumnName.NameSuffix}),
            new(nameof(ColumnName.NickName),              ContactProp.NickName,              new string[]{ColumnName.NickName}),
            new(nameof(ColumnName.Gender),                ContactProp.Gender,                new string[]{ColumnName.Gender}),
            new(nameof(ColumnName.BirthDay),              ContactProp.BirthDay,              new string[]{ColumnName.BirthDay}),
            new(nameof(ColumnName.Spouse),                ContactProp.Spouse,                new string[]{ColumnName.Spouse}),
            new(nameof(ColumnName.Anniversary),           ContactProp.Anniversary,           new string[]{ColumnName.Anniversary}),
            new(nameof(ColumnName.AddressHomeStreet),     ContactProp.AddressHomeStreet,     new string[]{ColumnName.AddressHomeStreet}),
            new(nameof(ColumnName.AddressHomePostalCode), ContactProp.AddressHomePostalCode, new string[]{ColumnName.AddressHomePostalCode}),
            new(nameof(ColumnName.AddressHomeCity),       ContactProp.AddressHomeCity,       new string[]{ColumnName.AddressHomeCity}),
            new(nameof(ColumnName.AddressHomeState),      ContactProp.AddressHomeState,      new string[]{ColumnName.AddressHomeState}),
            new(nameof(ColumnName.AddressHomeCountry),    ContactProp.AddressHomeCountry,    new string[]{ColumnName.AddressHomeCountry}),
            new(nameof(ColumnName.Email1),                ContactProp.Email1,                new string[]{ColumnName.Email1}),
            new(nameof(ColumnName.Email2),                ContactProp.Email2,                new string[]{ColumnName.Email2}),
            new(nameof(ColumnName.Email3),                ContactProp.Email3,                new string[]{ColumnName.Email3}),
            new(nameof(ColumnName.Email4),                ContactProp.Email4,                new string[]{ColumnName.Email4}),
            new(nameof(ColumnName.Email5),                ContactProp.Email5,                new string[]{ColumnName.Email5}),
            new(nameof(ColumnName.Email6),                ContactProp.Email6,                new string[]{ColumnName.Email6}),
            new(nameof(ColumnName.PhoneHome),             ContactProp.PhoneHome,             new string[]{ColumnName.PhoneHome}),
            new(nameof(ColumnName.PhoneWork),             ContactProp.PhoneWork,             new string[]{ColumnName.PhoneWork}),
            new(nameof(ColumnName.FaxHome),               ContactProp.FaxHome,               new string[]{ColumnName.FaxHome}),
            new(nameof(ColumnName.FaxWork),               ContactProp.FaxWork,               new string[]{ColumnName.FaxWork}),
            new(nameof(ColumnName.Cell),                  ContactProp.Cell,                  new string[]{ColumnName.Cell}),
            new(nameof(ColumnName.CellWork),              ContactProp.CellWork,              new string[]{ColumnName.CellWork}),
            new(nameof(ColumnName.PhoneOther1),           ContactProp.PhoneOther1,           new string[]{ColumnName.PhoneOther1}),
            new(nameof(ColumnName.PhoneOther2),           ContactProp.PhoneOther2,           new string[]{ColumnName.PhoneOther2}),
            new(nameof(ColumnName.PhoneOther3),           ContactProp.PhoneOther3,           new string[]{ColumnName.PhoneOther3}),
            new(nameof(ColumnName.PhoneOther4),           ContactProp.PhoneOther4,           new string[]{ColumnName.PhoneOther4}),
            new(nameof(ColumnName.PhoneOther5),           ContactProp.PhoneOther5,           new string[]{ColumnName.PhoneOther5}),
            new(nameof(ColumnName.PhoneOther6),           ContactProp.PhoneOther6,           new string[]{ColumnName.PhoneOther6}),
            new(nameof(ColumnName.InstantMessenger1),     ContactProp.InstantMessenger1,     new string[]{ColumnName.InstantMessenger1}),
            new(nameof(ColumnName.InstantMessenger2),     ContactProp.InstantMessenger2,     new string[]{ColumnName.InstantMessenger2}),
            new(nameof(ColumnName.InstantMessenger3),     ContactProp.InstantMessenger3,     new string[]{ColumnName.InstantMessenger3}),
            new(nameof(ColumnName.InstantMessenger4),     ContactProp.InstantMessenger4,     new string[]{ColumnName.InstantMessenger4}),
            new(nameof(ColumnName.InstantMessenger5),     ContactProp.InstantMessenger5,     new string[]{ColumnName.InstantMessenger5}),
            new(nameof(ColumnName.InstantMessenger6),     ContactProp.InstantMessenger6,     new string[]{ColumnName.InstantMessenger6}),
            new(nameof(ColumnName.WebPagePersonal),       ContactProp.HomePagePersonal,      new string[]{ColumnName.WebPagePersonal}),
            new(nameof(ColumnName.WebPageWork),           ContactProp.HomePageWork,          new string[]{ColumnName.WebPageWork}),
            new(nameof(ColumnName.WorkCompany),           ContactProp.WorkCompany,           new string[]{ColumnName.WorkCompany}),
            new(nameof(ColumnName.WorkDepartment),        ContactProp.WorkDepartment,        new string[]{ColumnName.WorkDepartment}),
            new(nameof(ColumnName.WorkOffice),            ContactProp.WorkOffice,            new string[]{ColumnName.WorkOffice}),
            new(nameof(ColumnName.WorkJobTitle),          ContactProp.WorkPosition,          new string[]{ColumnName.WorkJobTitle}),
            new(nameof(ColumnName.AddressWorkStreet),     ContactProp.AddressWorkStreet,     new string[]{ColumnName.AddressWorkStreet}),
            new(nameof(ColumnName.AddressWorkPostalCode), ContactProp.AddressWorkPostalCode, new string[]{ColumnName.AddressWorkPostalCode}),
            new(nameof(ColumnName.AddressWorkCity),       ContactProp.AddressWorkCity,       new string[]{ColumnName.AddressWorkCity}),
            new(nameof(ColumnName.AddressWorkState),      ContactProp.AddressWorkState,      new string[]{ColumnName.AddressWorkState}),
            new(nameof(ColumnName.AddressWorkCountry),    ContactProp.AddressWorkCountry,    new string[]{ColumnName.AddressWorkCountry}),
            new(nameof(ColumnName.Comment),               ContactProp.Comment,               new string[]{ColumnName.Comment}),
            new(nameof(ColumnName.TimeStamp),             ContactProp.TimeStamp,             new string[]{ColumnName.TimeStamp})
    ];
}
