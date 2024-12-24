using System.Text;
using FolkerKinzel.Contacts.IO.Resources;
using FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Csv = FolkerKinzel.CsvTools;

namespace FolkerKinzel.Contacts.IO.Intls.Csv;

internal abstract class CsvReader : CsvIOBase
{
    internal static CsvReader GetInstance(CsvCompatibility platform, IFormatProvider? formatProvider, Encoding? textEncoding) => platform switch
    {
        CsvCompatibility.Unspecified => new Universal.UniversalCsvReader(formatProvider, textEncoding),
        CsvCompatibility.Outlook => new Outlook.OutlookCsvReader(formatProvider, textEncoding),
        CsvCompatibility.Google => new Google.GoogleCsvReader(textEncoding),
        CsvCompatibility.Thunderbird => new Thunderbird.ThunderbirdCsvReader(formatProvider, textEncoding),
        _ => throw new ArgumentException(Res.UndefinedEnumValue, nameof(platform)),
    };

    protected CsvReader(IFormatProvider? formatProvider, Encoding? textEncoding) : base(formatProvider, textEncoding) { }

    protected CsvAnalyzer Analyzer { get; } = new CsvAnalyzer();

    /// <summary> Liest die mit <paramref name="fileName" /></summary>
    /// <param name="fileName">Dateipfad der zu lesenden CSV-Datei.</param>
    /// <returns>Liste von <see cref="Contact" />-Objekten, die den Inhalt der CSV-Datei
    /// darstellen.</returns>
    /// <exception cref="ArgumentNullException"> <paramref name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException"> <paramref name="fileName" /> is not a valid
    /// file path.</exception>
    /// <exception cref="IOException">
    /// <para>
    /// The file cannot be accessed
    /// </para>
    /// <para>
    /// - or -
    /// </para>
    /// <para>
    /// the file contains invalid CSV.
    /// </para>
    /// </exception>
    public List<Contact> Read(string fileName)
    {
        var list = new List<Contact>();

        Analyzer.Analyze(fileName);

        if (!Analyzer.HasHeaderRow)
        {
            return list;
        }


        IList<Tuple<string, ContactProp?, IList<string>>>? mapping = CreateMapping();
        CsvRecordWrapper? wrapper = InitCsvRecordWrapper(mapping);

        Debug.Assert(wrapper.Count == mapping.Count);

        using var reader =
           new Csv::CsvReader(fileName, hasHeaderRow: true, options: Analyzer.Options | CsvOptions.DisableCaching, textEncoding: TextEncoding, fieldSeparator: Analyzer.FieldSeparator);

        try
        {
            foreach (CsvRecord? record in reader.Read())
            {
                wrapper.Record = record;
                list.Add(InitContact(wrapper, mapping));
            }
        }
        catch (InvalidCsvException e)
        {
            throw new IOException(e.Message, e);
        }

        return list;
    }

    /// <summary> Initialisiert aus den Daten eine <see cref="CsvRecordWrapper" />-Objekts
    /// ein <see cref="Contact" />-Objekt. </summary>
    /// <param name="wrapper"> <see cref="CsvRecordWrapper" /> </param>
    /// <param name="mapping">Zuordnung zwischen Eigenschaftsnamen von <see cref="CsvRecordWrapper"
    /// />, Eigenschaft von <see cref="Contact" /> und Spaltenname der CSV-Datei. Für
    /// die Methode ist nur <see cref="Tuple{T1, T2, T3}.Item2" /> relevant (Eigenschaft
    /// von <see cref="Contact" />). <paramref name="mapping" /> muss die gleiche Länge
    /// haben wie <paramref name="wrapper" />.</param>
    /// <returns>Ein <see cref="Contact" />-Objekt.</returns>
    private Contact InitContact(CsvRecordWrapper wrapper, IList<Tuple<string, ContactProp?, IList<string>>> mapping)
    {
        var contact = new Contact();

        Person? person = null;
        Name? name = null;
        Address? addressHome = null;
        Work? work = null;
        Address? addressWork = null;
        List<string>? instMessengers = null;
        List<string>? emails = null;
        //List<PhoneNumber>? phones = null;



        for (int i = 0; i < wrapper.Count; i++)
        {
            object? val = wrapper[i];

            if (val is null)
            {
                continue;
            }

            ContactProp? prop = mapping[i].Item2;

            switch (prop)
            {
                case ContactProp.DisplayName:
                    contact.DisplayName = (string)val;
                    break;
                case ContactProp.FirstName:
                    InitName();
                    name!.FirstName = (string)val;
                    break;
                case ContactProp.MiddleName:
                    InitName();
                    name!.MiddleName = (string)val;
                    break;
                case ContactProp.LastName:
                    InitName();
                    name!.LastName = (string)val;
                    break;
                case ContactProp.NamePrefix:
                    InitName();
                    name!.Prefix = (string)val;
                    break;
                case ContactProp.NameSuffix:
                    InitName();
                    name!.Suffix = (string)val;
                    break;
                case ContactProp.NickName:
                    InitPerson();
                    person!.NickName = (string)val;
                    break;
                case ContactProp.Gender:
                    {
                        var sex = (Sex)val!;

                        if (sex != default)
                        {
                            InitPerson();
                            person!.Gender = sex;
                        }
                    }
                    break;
                case ContactProp.BirthDay:
                    InitPerson();
                    person!.BirthDay = ((DateTime)val).Date;
                    break;
                case ContactProp.Spouse:
                    InitPerson();
                    person!.Spouse = (string)val;
                    break;
                case ContactProp.Anniversary:
                    InitPerson();
                    person!.Anniversary = ((DateTime)val).Date;
                    break;
                case ContactProp.AddressHomeStreet:
                    InitAddressHome();
                    addressHome!.Street = (string)val;
                    break;
                case ContactProp.AddressHomePostalCode:
                    InitAddressHome();
                    addressHome!.PostalCode = (string)val;
                    break;
                case ContactProp.AddressHomeCity:
                    InitAddressHome();
                    addressHome!.City = (string)val;
                    break;
                case ContactProp.AddressHomeState:
                    InitAddressHome();
                    addressHome!.State = (string)val;
                    break;
                case ContactProp.AddressHomeCountry:
                    InitAddressHome();
                    addressHome!.Country = (string)val;
                    break;
                case ContactProp.Email1:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.Email2:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.Email3:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.Email4:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.Email5:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.Email6:
                    InitEmails();
                    emails!.Add((string)val);
                    break;
                case ContactProp.PhoneHome:
                    AddPhoneNumber(contact, new PhoneNumber((string)val));
                    break;
                case ContactProp.PhoneWork:
                    AddPhoneNumber(contact, new PhoneNumber((string)val, isWork: true));
                    break;
                case ContactProp.FaxHome:
                    AddPhoneNumber(contact, new PhoneNumber((string)val, isFax: true));
                    break;
                case ContactProp.FaxWork:
                    AddPhoneNumber(contact, new PhoneNumber((string)val, isWork: true, isFax: true));
                    break;
                case ContactProp.Cell:
                    AddPhoneNumber(contact, new PhoneNumber((string)val, isMobile: true));
                    break;
                case ContactProp.CellWork:
                    AddPhoneNumber(contact, new PhoneNumber((string)val, isWork: true, isMobile: true));
                    break;
                case ContactProp.PhoneOther1:
                    AddPhoneNumber(contact, new PhoneNumber((string)val));
                    break;
                case ContactProp.PhoneOther2:
                    AddPhoneNumber(contact, new PhoneNumber((string)val));
                    break;
                case ContactProp.PhoneOther3:
                    AddPhoneNumber(contact, new PhoneNumber((string)val));
                    break;
                //case ContactProp.PhoneOther4:
                //    InitPhones();
                //    phones[PHONE_OTHER_4] = new PhoneNumber((string)wrapper[i]);
                //    break;
                //case ContactProp.PhoneOther5:
                //    InitPhones();
                //    phones[PHONE_OTHER_5] = new PhoneNumber((string)wrapper[i]);
                //    break;
                //case ContactProp.PhoneOther6:
                //    InitPhones();
                //    phones[PHONE_OTHER_6] = new PhoneNumber((string)wrapper[i]);
                //    break;
                case ContactProp.InstantMessenger1:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.InstantMessenger2:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.InstantMessenger3:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.InstantMessenger4:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.InstantMessenger5:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.InstantMessenger6:
                    InitInstMessengers();
                    instMessengers!.Add((string)val);
                    break;
                case ContactProp.HomePagePersonal:
                    contact.WebPagePersonal = (string)val;
                    break;
                case ContactProp.HomePageWork:
                    contact.WebPageWork = (string)val;
                    break;
                case ContactProp.WorkCompany:
                    InitWork();
                    work!.Company = (string)val;
                    break;
                case ContactProp.WorkDepartment:
                    InitWork();
                    work!.Department = (string)val;
                    break;
                case ContactProp.WorkOffice:
                    InitWork();
                    work!.Office = (string)val;
                    break;
                case ContactProp.WorkPosition:
                    InitWork();
                    work!.JobTitle = (string)val;
                    break;
                case ContactProp.AddressWorkStreet:
                    InitAddressWork();
                    addressWork!.Street = (string)val;
                    break;
                case ContactProp.AddressWorkPostalCode:
                    InitAddressWork();
                    addressWork!.PostalCode = (string)val;
                    break;
                case ContactProp.AddressWorkCity:
                    InitAddressWork();
                    addressWork!.City = (string)val;
                    break;
                case ContactProp.AddressWorkState:
                    InitAddressWork();
                    addressWork!.State = (string)val;
                    break;
                case ContactProp.AddressWorkCountry:
                    InitAddressWork();
                    addressWork!.Country = (string)val;
                    break;
                case ContactProp.Comment:
                    contact.Comment = (string)val;
                    break;
                case ContactProp.TimeStamp:
                    contact.TimeStamp = (DateTime)val;
                    break;
                default:
                    if (prop.HasValue)
                    {
                        InitContactNonStandardProp(contact, prop.Value, wrapper, i);
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
            person!.Name = name;
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
            work!.AddressWork = addressWork;
        }

        void InitInstMessengers()
        {
            instMessengers ??= new List<string>();
            contact.InstantMessengerHandles = instMessengers;
        }

        void InitEmails()
        {
            emails ??= new List<string>();
            contact.EmailAddresses = emails;
        }
    }

    protected static void AddPhoneNumber(Contact contact, PhoneNumber newNumber)
    {
        IEnumerable<PhoneNumber?>? phones = contact.PhoneNumbers;

        if (phones is null)
        {
            contact.PhoneNumbers = newNumber;
        }
        else if (phones is PhoneNumber recentNumber)
        {
            contact.PhoneNumbers = new List<PhoneNumber>(2) { recentNumber, newNumber };
        }
        else
        {
            ((List<PhoneNumber>)phones).Add(newNumber);
        }
    }


    [ExcludeFromCodeCoverage]
    protected virtual void InitContactNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index) { }

}
