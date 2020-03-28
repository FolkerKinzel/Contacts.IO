﻿using Csv = FolkerKinzel.CsvTools;
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

    internal abstract class CsvWriter : CsvIOBase
    {
       
        public static CsvWriter GetInstance(CsvTarget platform) => platform switch
        {
            CsvTarget.Unspecified => new Universal.UniversalCsvWriter(),
            CsvTarget.Outlook => new Outlook.OutlookCsvWriter(),
            CsvTarget.Google => new Google.GoogleCsvWriter(),
            CsvTarget.Thunderbird => new Thunderbird.ThunderbirdCsvWriter(),
            _ => throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform)),
        };


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

        internal void Write(string fileName, IEnumerable<Contact> data)
        {
            
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string[] columnNames = CreateColumnNames();

            using var writer = new Csv::CsvWriter(fileName, columnNames);

            var mapping = CreateMapping();
            var wrapper = InitCsvRecordWrapper(mapping);
            wrapper.Record = writer.Record;

            var props = mapping.Select(x => x.Item2).ToArray();

            Debug.Assert(wrapper.Count == props.Length);

            foreach (var contact in data)
            {
                if (contact is null) continue;
                contact.Clean();

                if (contact.IsEmpty) continue;

                FillCsvRecord(contact, props, wrapper, propInfo);
                writer.WriteRecord();
            }
        }

        protected abstract string[] CreateColumnNames(); 

        private void FillCsvRecord(Contact contact, ContactProp?[] props, CsvRecordWrapper wrapper, bool[] propInfo)
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
                ContactProp? prop = props[i];

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
                        if (prop.HasValue)
                        {
                            wrapper[i] = FillCsvRecordNonStandardProp(contact, prop.Value);
                        }
                        break;
                }
            }
        }


        protected virtual object? FillCsvRecordNonStandardProp(Contact contact, ContactProp prop) => null;
        

    }
}
