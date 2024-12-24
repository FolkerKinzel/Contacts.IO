using System.Text;
using FolkerKinzel.Contacts.IO.Resources;
using FolkerKinzel.CsvTools.Helpers;
using Csv = FolkerKinzel.CsvTools;

namespace FolkerKinzel.Contacts.IO.Intls.Csv;

internal abstract class CsvWriter(IFormatProvider? formatProvider, Encoding? textEncoding) : CsvIOBase(formatProvider, textEncoding)
{
    internal static CsvWriter GetInstance(CsvCompatibility platform, IFormatProvider? formatProvider, Encoding? textEncoding) => platform switch
    {
        CsvCompatibility.Unspecified => new Universal.UniversalCsvWriter(formatProvider, textEncoding),
        CsvCompatibility.Outlook => new Outlook.OutlookCsvWriter(formatProvider, textEncoding),
        CsvCompatibility.Google => new Google.GoogleCsvWriter(textEncoding),
        CsvCompatibility.Thunderbird => new Thunderbird.ThunderbirdCsvWriter(formatProvider, textEncoding),
        _ => throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform)),
    };

    internal void Write(string fileName, IEnumerable<Contact?> data)
    {
        _ArgumentNullException.ThrowIfNull(data, nameof(data));

        string[] columnNames = CreateColumnNames();

        using var writer = new Csv::CsvWriter(fileName, columnNames, textEncoding: TextEncoding);

        IList<Tuple<string, ContactProp?, IList<string>>>? mapping = CreateMapping();
        CsvRecordWrapper? wrapper = InitCsvRecordWrapper(mapping);
        wrapper.Record = writer.Record;

        Debug.Assert(wrapper.Count == mapping.Count);

        foreach (Contact? cont in data)
        {
            if (cont is null)
            {
                continue;
            }

            cont.Clean();

            if (cont.IsEmpty)
            {
                continue;
            }

            FillCsvRecord(cont, wrapper, mapping);
            writer.WriteRecord();
        }
    }

    protected abstract string[] CreateColumnNames();

    private void FillCsvRecord(Contact contact, CsvRecordWrapper wrapper, IList<Tuple<string, ContactProp?, IList<string>>> mapping)
    {
        Person? person = contact.Person;
        Name? name = person?.Name;
        Address? homeAddress = contact.AddressHome;
        IEnumerable<string?>? emails = contact.EmailAddresses ?? EmptyStringArray;
        IEnumerable<string?>? ims = contact.InstantMessengerHandles ?? EmptyStringArray;
        Work? work = contact.Work;
        Address? workAddress = work?.AddressWork;

        IEnumerable<PhoneNumber?>? phones = contact.PhoneNumbers ?? EmptyPhonesArray;
        PhoneNumber?[]? otherPhones = phones.Where(x => !(x is null || x.IsMobile || x.IsFax || x.IsWork)).ToArray();

        for (int i = 0; i < mapping.Count; i++)
        {
            ContactProp? prop = mapping[i].Item2;

            switch (prop)
            {
                case ContactProp.DisplayName:
                    wrapper[i] = contact.DisplayName;
                    break;
                case ContactProp.FirstName:
                    wrapper[i] = name?.FirstName;
                    break;
                case ContactProp.MiddleName:
                    wrapper[i] = name?.MiddleName;
                    break;
                case ContactProp.LastName:
                    wrapper[i] = name?.LastName;
                    break;
                case ContactProp.NamePrefix:
                    wrapper[i] = name?.Prefix;
                    break;
                case ContactProp.NameSuffix:
                    wrapper[i] = name?.Suffix;
                    break;
                case ContactProp.NickName:
                    wrapper[i] = person?.NickName;
                    break;
                case ContactProp.Gender:
                    wrapper[i] = person?.Gender;
                    break;
                case ContactProp.BirthDay:
                    wrapper[i] = person?.BirthDay;
                    break;
                case ContactProp.Spouse:
                    wrapper[i] = person?.Spouse;
                    break;
                case ContactProp.Anniversary:
                    wrapper[i] = person?.Anniversary;
                    break;
                case ContactProp.AddressHomeStreet:
                    wrapper[i] = homeAddress?.Street;
                    break;
                case ContactProp.AddressHomePostalCode:
                    wrapper[i] = homeAddress?.PostalCode;
                    break;
                case ContactProp.AddressHomeCity:
                    wrapper[i] = homeAddress?.City;
                    break;
                case ContactProp.AddressHomeState:
                    wrapper[i] = homeAddress?.State;
                    break;
                case ContactProp.AddressHomeCountry:
                    wrapper[i] = homeAddress?.Country;
                    break;
                case ContactProp.Email1:
                    wrapper[i] = emails.FirstOrDefault();
                    break;
                case ContactProp.Email2:
                    wrapper[i] = emails.ElementAtOrDefault(1);
                    break;
                case ContactProp.Email3:
                    wrapper[i] = emails.ElementAtOrDefault(2);
                    break;
                case ContactProp.Email4:
                    wrapper[i] = emails.ElementAtOrDefault(3);
                    break;
                case ContactProp.Email5:
                    wrapper[i] = emails.ElementAtOrDefault(4);
                    break;
                case ContactProp.Email6:
                    wrapper[i] = emails.ElementAtOrDefault(5);
                    break;
                case ContactProp.PhoneWork:
                    wrapper[i] = phones.FirstOrDefault(x => x != null && x.IsWork)?.Value;
                    break;
                case ContactProp.FaxHome:
                    wrapper[i] = phones.FirstOrDefault(x => x != null && x.IsFax && !x.IsWork)?.Value;
                    break;
                case ContactProp.FaxWork:
                    wrapper[i] = phones.FirstOrDefault(x => x != null && x.IsFax && x.IsWork)?.Value;
                    break;
                case ContactProp.Cell:
                    wrapper[i] = _propInfo[TWO_CELL_PROPERTIES]
                        ? (phones.FirstOrDefault(x => x != null && x.IsMobile && !x.IsWork)?.Value)
                        : (phones.FirstOrDefault(x => x != null && x.IsMobile)?.Value);
                    break;
                case ContactProp.CellWork:
                    wrapper[i] = phones.FirstOrDefault(x => x != null && x.IsMobile && x.IsWork)?.Value;
                    break;
                case ContactProp.PhoneHome:
                    wrapper[i] = otherPhones.FirstOrDefault()?.Value;
                    break;
                case ContactProp.PhoneOther1:
                    wrapper[i] = _propInfo[TWO_PHONE_PROPERTIES] ? (otherPhones.ElementAtOrDefault(1)?.Value) : (otherPhones.FirstOrDefault()?.Value);
                    break;
                case ContactProp.PhoneOther2:
                    wrapper[i] = otherPhones.ElementAtOrDefault(2)?.Value;
                    break;
                case ContactProp.PhoneOther3:
                    wrapper[i] = otherPhones.ElementAtOrDefault(3)?.Value;
                    break;
                case ContactProp.PhoneOther4:
                    wrapper[i] = otherPhones.ElementAtOrDefault(4)?.Value;
                    break;
                case ContactProp.PhoneOther5:
                    wrapper[i] = otherPhones.ElementAtOrDefault(5)?.Value;
                    break;
                case ContactProp.PhoneOther6:
                    wrapper[i] = otherPhones.ElementAtOrDefault(6)?.Value;
                    break;
                case ContactProp.InstantMessenger1:
                    wrapper[i] = ims.FirstOrDefault();
                    break;
                case ContactProp.InstantMessenger2:
                    wrapper[i] = ims.ElementAtOrDefault(1);
                    break;
                case ContactProp.InstantMessenger3:
                    wrapper[i] = ims.ElementAtOrDefault(2);
                    break;
                case ContactProp.InstantMessenger4:
                    wrapper[i] = ims.ElementAtOrDefault(3);
                    break;
                case ContactProp.InstantMessenger5:
                    wrapper[i] = ims.ElementAtOrDefault(4);
                    break;
                case ContactProp.InstantMessenger6:
                    wrapper[i] = ims.ElementAtOrDefault(5);
                    break;
                case ContactProp.HomePagePersonal:
                    wrapper[i] = contact.WebPagePersonal;
                    break;
                case ContactProp.HomePageWork:
                    wrapper[i] = contact.WebPageWork;
                    break;
                case ContactProp.WorkCompany:
                    wrapper[i] = work?.Company;
                    break;
                case ContactProp.WorkDepartment:
                    wrapper[i] = work?.Department;
                    break;
                case ContactProp.WorkOffice:
                    wrapper[i] = work?.Office;
                    break;
                case ContactProp.WorkPosition:
                    wrapper[i] = work?.JobTitle;
                    break;
                case ContactProp.AddressWorkStreet:
                    wrapper[i] = workAddress?.Street;
                    break;
                case ContactProp.AddressWorkPostalCode:
                    wrapper[i] = workAddress?.PostalCode;
                    break;
                case ContactProp.AddressWorkCity:
                    wrapper[i] = workAddress?.City;
                    break;
                case ContactProp.AddressWorkState:
                    wrapper[i] = workAddress?.State;
                    break;
                case ContactProp.AddressWorkCountry:
                    wrapper[i] = workAddress?.Country;
                    break;
                case ContactProp.Comment:
                    wrapper[i] = contact.Comment;
                    break;
                case ContactProp.TimeStamp:
                    DateTime? timeStamp = contact.TimeStamp;
                    wrapper[i] = timeStamp;
                    break;
                default:
                    if (prop.HasValue)
                    {
                        FillCsvRecordNonStandardProp(contact, prop.Value, wrapper, i);
                    }
                    break;
            }
        }
    }

    [ExcludeFromCodeCoverage]
    protected virtual void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index) { }

}
