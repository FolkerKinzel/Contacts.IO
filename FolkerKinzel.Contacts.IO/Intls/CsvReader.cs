﻿using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Linq;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls
{
    internal static class CsvReader
    {
        public static List<Contact> Read(string fileName, CsvTarget platform)
        {
            var wrapper = new CsvRecordWrapper();
            List<ContactProp> properties = new List<ContactProp>();

            Csv::CsvReader? reader = null;
            var list = new List<Contact>();

            try
            {
                switch (platform)
                {
                    case CsvTarget.Unspecified:
                        var analyzer = new CsvAnalyzer();
                        analyzer.Analyze(fileName);
                        if(!analyzer.HasHeader)
                        {
                            return list;
                        }
                        reader = new Csv::CsvReader(fileName, analyzer.FieldSeparatorChar, true, analyzer.Options, null, true);
                        InitUnspecified(wrapper, properties);
                        break;
                    case CsvTarget.Outlook:
                        reader = new Csv::CsvReader(fileName, disableCaching: true);
                        InitOutlook(wrapper, properties);
                        break;
                    case CsvTarget.Google:
                        reader = new Csv::CsvReader(fileName, disableCaching: true);
                        InitGoogle(wrapper, properties);
                        break;
                    case CsvTarget.Thunderbird:
                        reader = new Csv::CsvReader(fileName, disableCaching: true);
                        InitThunderbird(wrapper, properties);
                        break;
                    default:
                        throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform));

                }

                foreach (var record in reader.Read())
                {
                    wrapper.SetRecord(record);
                    list.Add(InitContact(wrapper, properties));
                }

                return list;
            }
            finally
            {
                reader?.Dispose();
            }
        }

        private static void InitUnspecified(CsvRecordWrapper wrapper, List<ContactProp> properties)
        {
            throw new NotImplementedException();
        }

        private static void InitOutlook(CsvRecordWrapper wrapper, List<ContactProp> properties)
        {
            throw new NotImplementedException();
        }


        private static void InitGoogle(CsvRecordWrapper wrapper, List<ContactProp> properties)
        {
            throw new NotImplementedException();
        }


        private static void InitThunderbird(CsvRecordWrapper wrapper, List<ContactProp> properties)
        {
            throw new NotImplementedException();
        }

       

        private static Contact InitContact(CsvRecordWrapper wrapper, IList<ContactProp> properties)
        {
            const int INST_MESSENGER_1 = 0;
            const int INST_MESSENGER_2 = 1;
            const int INST_MESSENGER_3 = 2;
            const int INST_MESSENGER_4 = 3;
            const int INST_MESSENGER_5 = 4;
            const int INST_MESSENGER_6 = 5;
            const int INST_MESSENGERS_LENGTH = 6;

            const int EMAIL_1 = 0;
            const int EMAIL_2 = 1;
            const int EMAIL_3 = 2;
            const int EMAIL_4 = 3;
            const int EMAIL_5 = 4;
            const int EMAIL_6 = 5;
            const int EMAILS_LENGTH = 6;

            const int PHONE_HOME = 0;
            const int PHONE_WORK = 1;
            const int CELL = 2;
            const int CELL_WORK = 3;
            const int FAX_HOME = 4;
            const int FAX_WORK = 5;
            const int PHONE_OTHER_1 = 6;
            const int PHONE_OTHER_2 = 7;
            const int PHONE_OTHER_3 = 8;
            const int PHONE_OTHER_4 = 9;
            const int PHONE_OTHER_5 = 10;
            const int PHONE_OTHER_6 = 11;
            const int PHONES_LENGTH = 12;

            var contact = new Contact();

            Person? person = null;
            Name? name = null;
            Address? addressHome = null;
            Work? work = null;
            Address? addressWork = null;
            string?[]? instMessengers = null;
            string?[]? emails = null;
            PhoneNumber?[]? phones = null;



            for (int i = 0; i < wrapper.Count; i++)
            {
                ContactProp prop = properties[i];


#nullable disable
                switch (prop)
                {
                    case ContactProp.DisplayName:
                        contact.DisplayName = (string)wrapper[i];
                        break;
                    case ContactProp.FirstName:
                        InitName();
                        name.FirstName = (string)wrapper[i];
                        break;
                    case ContactProp.MiddleName:
                        InitName();
                        name.MiddleName = (string)wrapper[i];
                        break;
                    case ContactProp.LastName:
                        InitName();
                        name.LastName = (string)wrapper[i]; 
                        break;
                    case ContactProp.NamePrefix:
                        InitName();
                        name.Prefix = (string)wrapper[i]; 
                        break;
                    case ContactProp.NameSuffix:
                        InitName();
                        name.Suffix = (string)wrapper[i]; 
                        break;
                    case ContactProp.NickName:
                        InitPerson();
                        person.NickName = (string)wrapper[i];
                        break;
                    case ContactProp.Gender:
                        InitPerson();
                        person.Gender = (Sex)wrapper[i]; 
                        break;
                    case ContactProp.BirthDay:
                        InitPerson();
                        person.BirthDay = (DateTime?)wrapper[i]; 
                        break;
                    case ContactProp.Spouse:
                        InitPerson();
                        person.Spouse = (string)wrapper[i];
                        break;
                    case ContactProp.Anniversary:
                        InitPerson();
                        person.Anniversary = (DateTime?)wrapper[i]; 
                        break;
                    case ContactProp.AddressHomeStreet:
                        InitAddressHome();
                        addressHome.Street = (string)wrapper[i];
                        break;
                    case ContactProp.AddressHomePostalCode:
                        InitAddressHome();
                        addressHome.PostalCode = (string)wrapper[i]; 
                        break;
                    case ContactProp.AddressHomeCity:
                        InitAddressHome();
                        addressHome.City = (string)wrapper[i]; 
                        break;
                    case ContactProp.AddressHomeState:
                        InitAddressHome();
                        addressHome.State = (string)wrapper[i]; 
                        break;
                    case ContactProp.AddressHomeCountry:
                        InitAddressHome();
                        addressHome.Country = (string)wrapper[i]; 
                        break;
                    case ContactProp.Email1:
                        InitEmails();
                        emails[EMAIL_1] = (string)wrapper[i];
                        break;
                    case ContactProp.Email2:
                        InitEmails();
                        emails[EMAIL_2] = (string)wrapper[i];
                        break;
                    case ContactProp.Email3:
                        InitEmails();
                        emails[EMAIL_3] = (string)wrapper[i];
                        break;
                    case ContactProp.Email4:
                        InitEmails();
                        emails[EMAIL_4] = (string)wrapper[i];
                        break;
                    case ContactProp.Email5:
                        InitEmails();
                        emails[EMAIL_5] = (string)wrapper[i];
                        break;
                    case ContactProp.Email6:
                        InitEmails();
                        emails[EMAIL_6] = (string)wrapper[i];
                        break;
                    case ContactProp.PhoneHome:
                        InitPhones();
                        phones[PHONE_HOME] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneWork:
                        InitPhones();
                        phones[PHONE_WORK] = new PhoneNumber((string)wrapper[i], isWork: true);
                        break;
                    case ContactProp.FaxHome:
                        InitPhones();
                        phones[FAX_HOME] = new PhoneNumber((string)wrapper[i], isFax: true);
                        break;
                    case ContactProp.FaxWork:
                        InitPhones();
                        phones[FAX_WORK] = new PhoneNumber((string)wrapper[i], isWork: true, isFax: true);
                        break;
                    case ContactProp.Cell:
                        InitPhones();
                        phones[CELL] = new PhoneNumber((string)wrapper[i], isCell: true);
                        break;
                    case ContactProp.CellWork:
                        InitPhones();
                        phones[CELL_WORK] = new PhoneNumber((string)wrapper[i], isWork: true, isCell: true);
                        break;
                    case ContactProp.PhoneOther1:
                        InitPhones();
                        phones[PHONE_OTHER_1] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneOther2:
                        InitPhones();
                        phones[PHONE_OTHER_2] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneOther3:
                        InitPhones();
                        phones[PHONE_OTHER_3] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneOther4:
                        InitPhones();
                        phones[PHONE_OTHER_4] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneOther5:
                        InitPhones();
                        phones[PHONE_OTHER_5] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.PhoneOther6:
                        InitPhones();
                        phones[PHONE_OTHER_6] = new PhoneNumber((string)wrapper[i]);
                        break;
                    case ContactProp.InstantMessenger1:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_1] = (string)wrapper[i];
                        break;
                    case ContactProp.InstantMessenger2:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_2] = (string)wrapper[i];
                        break;
                    case ContactProp.InstantMessenger3:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_3] = (string)wrapper[i];
                        break;
                    case ContactProp.InstantMessenger4:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_4] = (string)wrapper[i];
                        break;
                    case ContactProp.InstantMessenger5:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_5] = (string)wrapper[i];
                        break;
                    case ContactProp.InstantMessenger6:
                        InitInstMessengers();
                        instMessengers[INST_MESSENGER_6] = (string)wrapper[i];
                        break;
                    case ContactProp.HomePagePersonal:
                        contact.HomePagePersonal = (string)wrapper[i];
                        break;
                    case ContactProp.HomePageWork:
                        contact.HomePageWork = (string)wrapper[i];
                        break;
                    case ContactProp.WorkCompany:
                        InitWork();
                        work.Company = (string)wrapper[i];
                        break;
                    case ContactProp.WorkDepartment:
                        InitWork();
                        work.Department = (string)wrapper[i];
                        break;
                    case ContactProp.WorkOffice:
                        InitWork();
                        work.Office = (string)wrapper[i];
                        break;
                    case ContactProp.WorkPosition:
                        InitWork();
                        work.Position = (string)wrapper[i];
                        break;
                    case ContactProp.AddressWorkStreet:
                        InitAddressWork();
                        addressWork.Street = (string)wrapper[i];
                        break;
                    case ContactProp.AddressWorkPostalCode:
                        InitAddressWork();
                        addressWork.PostalCode = (string)wrapper[i];
                        break;
                    case ContactProp.AddressWorkCity:
                        InitAddressWork();
                        addressWork.City = (string)wrapper[i];
                        break;
                    case ContactProp.AddressWorkState:
                        InitAddressWork();
                        addressWork.State = (string)wrapper[i];
                        break;
                    case ContactProp.AddressWorkCountry:
                        InitAddressWork();
                        addressWork.Country = (string)wrapper[i];
                        break;
                    case ContactProp.Comment:
                        contact.Comment = (string)wrapper[i];
                        break;
                    case ContactProp.TimeStamp:
                        contact.TimeStamp = (DateTime)wrapper[i];
                        break;
                    default:
                        break;
                }
            }

            contact.Clean();
            return contact;

            //////////////////////////
            

            void InitPerson()
            {
                person ??= new Person();
                contact.Person = person;
            }

            void InitName()
            {
                InitPerson();

                name ??= new Name();
                person.Name = name;
            }

            void InitAddressHome()
            {
                addressHome ??= new Address();
                contact.AddressHome = addressHome;
            }


            void InitWork()
            {
                work ??= new Work();
                contact.Work = work;
            }

            void InitAddressWork()
            {
                InitWork();

                addressWork ??= new Address();
                work.AddressWork = addressWork;
            }

            void InitInstMessengers()
            {
                instMessengers ??= new string[INST_MESSENGERS_LENGTH];
                contact.InstantMessengerHandles = instMessengers;
            }

            void InitEmails()
            {
                emails ??= new string[EMAILS_LENGTH];
                contact.EmailAddresses = emails;
            }

            void InitPhones()
            {
                phones ??= new PhoneNumber[PHONES_LENGTH];
                contact.PhoneNumbers = phones;
            }

#nullable enable
        }


        
    }
}