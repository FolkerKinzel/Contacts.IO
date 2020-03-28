using System;
using System.Collections.Generic;
using System.Text;
using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools;
using System.Diagnostics;
using FolkerKinzel.Contacts.IO.Resources;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Intls.Csv
{
    internal abstract class CsvReader : CsvIOBase
    {

        protected CsvReader(Encoding? enc = null)
        {
            this.Encoding = enc;
        }

        internal static CsvReader GetInstance(CsvTarget platform) => platform switch
        {
            CsvTarget.Unspecified => new Universal.UniversalCsvReader(),
            CsvTarget.Outlook => new Outlook.OutlookCsvReader(),
            CsvTarget.Google => new Google.GoogleCsvReader(),
            CsvTarget.Thunderbird => new Thunderbird.ThunderbirdCsvReader(),
            _ => throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform)),
        };


        protected CsvAnalyzer Analyzer { get; } = new CsvAnalyzer();

        protected Encoding? Encoding { get; }


        /// <summary>
        /// Liest die mit <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">Dateipfad der zu lesenden CSV-Datei.</param>
        /// <returns>Liste von <see cref="Contact"/>-Objekten, die den Inhalt der CSV-Datei darstellen.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">
        /// <para>Es kann nicht auf den Datenträger zugegriffen werden</para>
        /// <para>- oder -</para>
        /// <para>die Datei enthält ungültiges CSV.</para></exception>
        public List<Contact> Read(string fileName)
        {
            var list = new List<Contact>();
    
            Analyzer.Analyze(fileName);

            if (!Analyzer.HasHeader)
            {
                return list;
            }


            var mapping = CreateMapping();
            var wrapper = InitCsvRecordWrapper(mapping);

            Debug.Assert(wrapper.Count == mapping.Count);

            using Csv::CsvReader reader =
               new Csv::CsvReader(fileName, hasHeaderRow: true, options: Analyzer.Options | CsvOptions.DisableCaching, enc: null, fieldSeparator: Analyzer.FieldSeparator);

            try
            {
                foreach (var record in reader.Read())
                {
                    wrapper.Record = record;
                    list.Add(InitContact(wrapper, mapping));
                }
            }
            catch(InvalidCsvException e)
            {
                throw new IOException(e.Message, e);
            }

            return list;
        }



        /// <summary>
        /// Initialisiert aus den Daten eine <see cref="CsvRecordWrapper"/>-Objekts ein <see cref="Contact"/>-Objekt.
        /// </summary>
        /// <param name="wrapper"><see cref="CsvRecordWrapper"/></param>
        /// <param name="mapping">Zuordnung zwischen Eigenschaftsnamen von <see cref="CsvRecordWrapper"/>, Eigenschaft von <see cref="Contact"/> und Spaltenname der CSV-Datei. 
        /// Für die Methode ist nur <see cref="Tuple{T1, T2, T3}.Item2"/> relevant (Eigenschaft von <see cref="Contact"/>). <paramref name="mapping"/> muss die gleiche Länge
        /// haben wie <paramref name="wrapper"/>.</param>
        /// <returns>Ein <see cref="Contact"/>-Objekt.</returns>
        private Contact InitContact(CsvRecordWrapper wrapper, IList<Tuple<string, ContactProp?, IList<string>>> mapping)
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
                ContactProp? prop = mapping[i].Item2;
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
                        if(prop.HasValue)
                        {
                            InitContactNonStandardProp(contact, prop.Value, wrapper[i]);
                        }
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




        protected virtual void InitContactNonStandardProp(Contact contact, ContactProp prop, object? value) { }
       
    }
}
