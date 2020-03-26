using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv
{
    internal abstract class CsvWriter
    {
        private const int TWO_CELL_PROPERTIES = 0;
        private const int TWO_PHONE_PROPERTIES = 1;
        private const int PROPINFO_LENGTH = 2;


        internal void Write(string fileName, IEnumerable<Contact> data)
        {
            //string[] google = new string[] {
            //    "Name",
            //    "Given Name",
            //    "Additional Name",
            //    "Family Name",
            //    "Yomi Name",
            //    "Given Name Yomi",
            //    "Additional Name Yomi",
            //    "Family Name Yomi",
            //    "Name Prefix",
            //    "Name Suffix",
            //    "Initials",
            //    "Nickname",
            //    "Short Name",
            //    "Maiden Name",
            //    "Birthday",
            //    "Gender",
            //    "Location",
            //    "Billing Information",
            //    "Directory Server",
            //    "Mileage",
            //    "Occupation",
            //    "Hobby",
            //    "Sensitivity",
            //    "Priority",
            //    "Subject",
            //    "Notes",
            //    "Language",
            //    "Photo",
            //    "Group Membership",
            //    "E-mail 1 - Type",
            //    "E-mail 1 - Value",
            //    "E-mail 2 - Type",
            //    "E-mail 2 - Value",
            //    "IM 1 - Type",
            //    "IM 1 - Service",
            //    "IM 1 - Value",
            //    "Phone 1 - Type",
            //    "Phone 1 - Value",
            //    "Phone 2 - Type",
            //    "Phone 2 - Value",
            //    "Phone 3 - Type",
            //    "Phone 3 - Value",
            //    "Phone 4 - Type",
            //    "Phone 4 - Value",
            //    "Phone 5 - Type",
            //    "Phone 5 - Value",
            //    "Phone 6 - Type",
            //    "Phone 6 - Value",
            //    "Phone 7 - Type",
            //    "Phone 7 - Value",
            //    "Address 1 - Type",
            //    "Address 1 - Formatted",
            //    "Address 1 - Street",
            //    "Address 1 - City",
            //    "Address 1 - PO Box",
            //    "Address 1 - Region",
            //    "Address 1 - Postal Code",
            //    "Address 1 - Country",
            //    "Address 1 - Extended Address",
            //    "Address 2 - Type",
            //    "Address 2 - Formatted",
            //    "Address 2 - Street",
            //    "Address 2 - City",
            //    "Address 2 - PO Box",
            //    "Address 2 - Region",
            //    "Address 2 - Postal Code",
            //    "Address 2 - Country",
            //    "Address 2 - Extended Address",
            //    "Address 3 - Type",
            //    "Address 3 - Formatted",
            //    "Address 3 - Street",
            //    "Address 3 - City",
            //    "Address 3 - PO Box",
            //    "Address 3 - Region",
            //    "Address 3 - Postal Code",
            //    "Address 3 - Country",
            //    "Address 3 - Extended Address",
            //    "Organization 1 - Type",
            //    "Organization 1 - Name",
            //    "Organization 1 - Yomi Name",
            //    "Organization 1 - Title",
            //    "Organization 1 - Department",
            //    "Organization 1 - Symbol",
            //    "Organization 1 - Location",
            //    "Organization 1 - Job Description",
            //    "Relation 1 - Type",
            //    "Relation 1 - Value",
            //    "Website 1 - Type",
            //    "Website 1 - Value",
            //    "Website 2 - Type",
            //    "Website 2 - Value" };


            //string[] outlook = new string[] {
            //    "First Name",
            //    "Middle Name",
            //    "Last Name",
            //    "Title",
            //    "Suffix",
            //    "Initials",
            //    "Web Page",
            //    "Gender",
            //    "Birthday",
            //    "Anniversary",
            //    "Location",
            //    "Language",
            //    "Internet Free Busy",
            //    "Notes",
            //    "E-mail Address",
            //    "E-mail 2 Address",
            //    "E-mail 3 Address",
            //    "Primary Phone",
            //    "Home Phone","Home Phone 2",
            //    "Mobile Phone",
            //    "Pager",
            //    "Home Fax",
            //    "Home Address",
            //    "Home Street",
            //    "Home Street 2",
            //    "Home Street 3",
            //    "Home Address PO Box",
            //    "Home City",
            //    "Home State",
            //    "Home Postal Code",
            //    "Home Country",
            //    "Spouse",
            //    "Children",
            //    "Manager's Name",
            //    "Assistant's Name",
            //    "Referred By",
            //    "Company Main Phone",
            //    "Business Phone",
            //    "Business Phone 2",
            //    "Business Fax",
            //    "Assistant's Phone",
            //    "Company",
            //    "Job Title",
            //    "Department",
            //    "Office Location",
            //    "Organizational ID Number",
            //    "Profession",
            //    "Account",
            //    "Business Address",
            //    "Business Street",
            //    "Business Street 2",
            //    "Business Street 3",
            //    "Business Address PO Box",
            //    "Business City",
            //    "Business State",
            //    "Business Postal Code",
            //    "Business Country",
            //    "Other Phone",
            //    "Other Fax",
            //    "Other Address",
            //    "Other Street",
            //    "Other Street 2",
            //    "Other Street 3",
            //    "Other Address PO Box",
            //    "Other City",
            //    "Other State",
            //    "Other Postal Code",
            //    "Other Country",
            //    "Callback",
            //    "Car Phone",
            //    "ISDN",
            //    "Radio Phone",
            //    "TTY/TDD Phone",
            //    "Telex",
            //    "User 1",
            //    "User 2",
            //    "User 3",
            //    "User 4",
            //    "Keywords",
            //    "Mileage",
            //    "Hobby",
            //    "Billing Information",
            //    "Directory Server",
            //    "Sensitivity",
            //    "Priority",
            //    "Private",
            //    "Categories"


            //    };

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var mapping = new List<Tuple<string, ContactProp?>>();

            InitMapping(mapping);

            


            using var writer = new Csv::CsvWriter(fileName, mapping.Select(x => x.Item1).ToArray());

            bool[] propInfo = new bool[PROPINFO_LENGTH];

            var mapper = InitCsvRecordWrapper(mapping, writer.Record, propInfo);

            var props = mapping.Where(x => x.Item2.HasValue).Select(x => x.Item2!.Value).ToArray();

            foreach (var contact in data)
            {
                if (contact is null) continue;
                contact.Clean();

                if (contact.IsEmpty) continue;

                FillCsvRecord(contact, props, mapper, propInfo);
                writer.WriteRecord();
            }

        }

        protected abstract void InitMapping(List<Tuple<string, ContactProp?>> mapping);


        protected virtual SexConverter InitSexConverter() => new SexConverter();
        



        public static CsvWriter GetInstance(CsvTarget platform) => platform switch
        {
            CsvTarget.Unspecified => new Universal.UniversalCsvWriter(),
            CsvTarget.Outlook => new Outlook.OutlookCsvWriter(),
            CsvTarget.Google => new Google.GoogleCsvWriter(),
            CsvTarget.Thunderbird => new Thunderbird.ThunderbirdCsvWriter(),
            _ => throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform)),
        };




        /// <summary>
        /// Initialisiert ein <see cref="CsvRecordWrapper"/>-Objekt.
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="record"></param>
        /// <param name="propInfo">Ein <see cref="bool"/>-Array, das Informationen über das doppelte Vorkommen ähnlicher Parameter sammelt.</param>
        /// <returns>Ein <see cref="CsvRecordWrapper"/>-Objekt.</returns>
        protected CsvRecordWrapper InitCsvRecordWrapper(IEnumerable<Tuple<string, ContactProp?>> mapping, Csv::CsvRecord record, bool[] propInfo)
        {
            var wrapper = new CsvRecordWrapper(record);

            var stringConverter = Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.String, nullable: true);

            int cellProperties = 0;
            int phoneProperties = 0;

            foreach (var tpl in mapping)
            {
                switch (tpl.Item2)
                {
                    case ContactProp.AddressHomeStreet:
                    case ContactProp.AddressHomePostalCode:
                    case ContactProp.AddressHomeCity:
                    case ContactProp.AddressHomeState:
                    case ContactProp.AddressHomeCountry:
                    case ContactProp.Email1:
                    case ContactProp.Email2:
                    case ContactProp.Email3:
                    case ContactProp.Email4:
                    case ContactProp.Email5:
                    case ContactProp.Email6:
                    case ContactProp.InstantMessenger1:
                    case ContactProp.InstantMessenger2:
                    case ContactProp.InstantMessenger3:
                    case ContactProp.InstantMessenger4:
                    case ContactProp.InstantMessenger5:
                    case ContactProp.InstantMessenger6:
                    case ContactProp.HomePagePersonal:
                    case ContactProp.HomePageWork:
                    case ContactProp.WorkCompany:
                    case ContactProp.WorkDepartment:
                    case ContactProp.WorkOffice:
                    case ContactProp.WorkPosition:
                    case ContactProp.AddressWorkStreet:
                    case ContactProp.AddressWorkPostalCode:
                    case ContactProp.AddressWorkCity:
                    case ContactProp.AddressWorkState:
                    case ContactProp.AddressWorkCountry:
                    case ContactProp.Comment:
                    case ContactProp.Spouse:
                    case ContactProp.DisplayName:
                    case ContactProp.FirstName:
                    case ContactProp.MiddleName:
                    case ContactProp.LastName:
                    case ContactProp.NamePrefix:
                    case ContactProp.NameSuffix:
                    case ContactProp.NickName:
                    case ContactProp.PhoneWork:
                    case ContactProp.FaxHome:
                    case ContactProp.FaxWork:
                        wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        break;
                    case ContactProp.Cell:
                    case ContactProp.CellWork:
                        wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        cellProperties++;
                        break;
                    case ContactProp.PhoneHome:
                    case ContactProp.PhoneOther1:
                    case ContactProp.PhoneOther2:
                    case ContactProp.PhoneOther3:
                    case ContactProp.PhoneOther4:
                    case ContactProp.PhoneOther5:
                    case ContactProp.PhoneOther6:
                        wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        phoneProperties++;
                        break;
                    case ContactProp.Gender:
                        wrapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item2.ToString(),
                                    new string[] { tpl.Item1 },
                                    InitSexConverter()));
                        break;
                    case ContactProp.BirthDay:
                    case ContactProp.Anniversary:
                    case ContactProp.TimeStamp:
                        wrapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item2.ToString(),
                                    new string[] { tpl.Item1 },
                                    Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.DateTime, nullable: true)));
                        break;
                    default:
                        break;
                }
            }

            propInfo[TWO_CELL_PROPERTIES] = (cellProperties >= 2);
            propInfo[TWO_PHONE_PROPERTIES] = (cellProperties >= 2);

            return wrapper;
        }


        protected void FillCsvRecord(Contact contact, ContactProp[] props, CsvRecordWrapper wrapper, bool[] propInfo)
        {
            var person = contact.Person;
            var name = person?.Name;
            var homeAddress = contact.AddressHome;
            var emails = contact.EmailAddresses;
            var ims = contact.InstantMessengerHandles;
            var work = contact.Work;
            var workAddress = work?.AddressWork;

            var phones = contact.PhoneNumbers;
            var otherPhones = phones?.Where(x => !(x is null || x.IsCell || x.IsFax || x.IsWork)).ToArray();

            for (int i = 0; i < props.Length; i++)
            {
                ContactProp prop = props[i];

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
                        wrapper[i] = emails?.FirstOrDefault();
                        break;
                    case ContactProp.Email2:
                        wrapper[i] = emails?.ElementAtOrDefault(1);
                        break;
                    case ContactProp.Email3:
                        wrapper[i] = emails?.ElementAtOrDefault(2);
                        break;
                    case ContactProp.Email4:
                        wrapper[i] = emails?.ElementAtOrDefault(3);
                        break;
                    case ContactProp.Email5:
                        wrapper[i] = emails?.ElementAtOrDefault(4);
                        break;
                    case ContactProp.Email6:
                        wrapper[i] = emails?.ElementAtOrDefault(5);
                        break;
                    
#nullable disable
                    case ContactProp.PhoneWork:
                        wrapper[i] = phones?.FirstOrDefault(x => x.IsWork);
                        break;
                    case ContactProp.FaxHome:
                        wrapper[i] = phones?.FirstOrDefault(x => x.IsFax && !x.IsWork);
                        break;
                    case ContactProp.FaxWork:
                        wrapper[i] = phones?.FirstOrDefault(x => x.IsFax && x.IsWork);
                        break;
                    case ContactProp.Cell:
                        if (propInfo[TWO_CELL_PROPERTIES])
                        {
                            wrapper[i] = phones?.FirstOrDefault(x => x.IsCell && !x.IsWork);
                        }
                        else
                        {
                            wrapper[i] = phones?.FirstOrDefault(x => x.IsCell);
                        }
                        break;
                    case ContactProp.CellWork:
                        wrapper[i] = phones?.FirstOrDefault(x => x.IsCell && x.IsWork);
                        break;
#nullable enable
                    case ContactProp.PhoneHome:
                        wrapper[i] = otherPhones?.FirstOrDefault();
                        break;
                    case ContactProp.PhoneOther1:
                        if (propInfo[TWO_PHONE_PROPERTIES])
                        {
                            wrapper[i] = otherPhones?.ElementAtOrDefault(1);
                        }
                        else
                        {
                            wrapper[i] = otherPhones?.FirstOrDefault();
                        }
                        break;
                    case ContactProp.PhoneOther2:
                        wrapper[i] = otherPhones?.ElementAtOrDefault(2);
                        break;
                    case ContactProp.PhoneOther3:
                        wrapper[i] = otherPhones?.ElementAtOrDefault(3);
                        break;
                    case ContactProp.PhoneOther4:
                        wrapper[i] = otherPhones?.ElementAtOrDefault(4);
                        break;
                    case ContactProp.PhoneOther5:
                        wrapper[i] = otherPhones?.ElementAtOrDefault(5);
                        break;
                    case ContactProp.PhoneOther6:
                        wrapper[i] = otherPhones?.ElementAtOrDefault(6);
                        break;
                    case ContactProp.InstantMessenger1:
                        wrapper[i] = ims?.FirstOrDefault();
                        break;
                    case ContactProp.InstantMessenger2:
                        wrapper[i] = ims?.ElementAtOrDefault(1);
                        break;
                    case ContactProp.InstantMessenger3:
                        wrapper[i] = ims?.ElementAtOrDefault(2);
                        break;
                    case ContactProp.InstantMessenger4:
                        wrapper[i] = ims?.ElementAtOrDefault(3);
                        break;
                    case ContactProp.InstantMessenger5:
                        wrapper[i] = ims?.ElementAtOrDefault(4);
                        break;
                    case ContactProp.InstantMessenger6:
                        wrapper[i] = ims?.ElementAtOrDefault(5);
                        break;
                    case ContactProp.HomePagePersonal:
                        wrapper[i] = contact.HomePagePersonal;
                        break;
                    case ContactProp.HomePageWork:
                        wrapper[i] = contact.HomePageWork;
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
                        wrapper[i] = work?.Position;
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
                        wrapper[i] = timeStamp == default(DateTime) ? null : timeStamp;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
