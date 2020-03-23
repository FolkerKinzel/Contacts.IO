using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Intls
{
    internal static class CsvWriter
    {
        private const int TWO_CELL_PROPERTIES = 0;
        private const int TWO_PHONE_PROPERTIES = 1;

        /// <summary>
        /// Schreibt den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine CSV-Datei.
        /// </summary>
        /// <param name="fileName">Dateipfad der zu schreibenden CSV-Datei.</param>
        /// <param name="data">Die zu persistierenden <see cref="Contact"/>-Objekte.</param>
        /// <param name="mapping">Eine Liste, die mit den in ihr enthaltenen <see cref="Tuple{T1, T2}"/>-Objekten die Reihenfolge der Spaltennamen der CSV-Datei (<see cref="Tuple{T1, T2}.Item1"/>)
        /// und die Zuordnung dieser Spaltenamen zu Eigenschaften der <see cref="Contact"/>-Klasse (<see cref="Tuple{T1, T2}.Item2"/>) beschreibt. In <paramref name="mapping"/> darf kein 
        /// Spaltenname doppelt vorkommen!</param>
        /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
        /// <exception cref = "ArgumentNullException"><paramref name="fileName"/> oder <paramref name="data"/> oder <paramref name="mapping"/> ist<c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
        /// <para>- oder -</para>
        /// <para>ein Spaltenname (<see cref="Tuple{T1, T2}.Item1"/>) in <paramref name="mapping"/> kommt doppelt vor. Der Vergleich ignoriert die Groß- und Kleinschreibung!</para></exception>
        /// <exception cref="IOException">E/A-Fehler.</exception>
        public static void WriteCsv(string fileName, IEnumerable<Contact> data, CsvMappingCollection mapping, CsvTarget platform)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (mapping is null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            // CsvMappingCollection verhindert, dass die Collection NULL-Werte enthalten kann und dass
            // Spaltennamen NULL sind oder nur aus Leerraum bestehen:
            Debug.Assert(mapping.All(x => x != null && !string.IsNullOrWhiteSpace(x.Item1)));

            using var writer = new Csv::CsvWriter(fileName, mapping.Select(x => x.Item1).ToArray());

            bool[] propInfo = new bool[2];

            var mapper = InitCsvRecordMapper(mapping, writer, platform, propInfo);


            var keys = mapping.Select(x => x.Item2).ToArray();

            foreach (var contact in data)
            {
                if (contact is null) continue;
                contact.Clean();

                if (contact.IsEmpty) continue;

                FillCsvRecord(contact, keys,  mapper, propInfo);
                writer.WriteRecord();
            }
        }

        
        /// <summary>
        /// Initialisiert ein <see cref="CsvRecordWrapper"/>-Objekt.
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="writer"></param>
        /// <param name="platform"></param>
        /// <param name="propInfo">Ein <see cref="bool"/>-Array, das Informationen über das doppelte Vorkommen ähnlicher Parameter sammelt.</param>
        /// <returns>Ein <see cref="CsvRecordWrapper"/>-Objekt.</returns>
        private static CsvRecordWrapper InitCsvRecordMapper(IEnumerable<Tuple<string, ContactProperty>> mapping, Csv::CsvWriter writer, CsvTarget platform, bool[] propInfo)
        {
            var mapper = new CsvRecordWrapper(writer.Record);

            var stringConverter = Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.String, nullable: true);

            int cellProperties = 0;
            int phoneProperties = 0;

            foreach (var tpl in mapping)
            {
                switch (tpl.Item2)
                {
                    case ContactProperty.AddressHomeStreet:
                    case ContactProperty.AddressHomePostalCode:
                    case ContactProperty.AddressHomeCity:
                    case ContactProperty.AddressHomeState:
                    case ContactProperty.AddressHomeCountry:
                    case ContactProperty.Email1:
                    case ContactProperty.Email2:
                    case ContactProperty.Email3:
                    case ContactProperty.Email4:
                    case ContactProperty.Email5:
                    case ContactProperty.Email6:
                    case ContactProperty.InstantMessenger1:
                    case ContactProperty.InstantMessenger2:
                    case ContactProperty.InstantMessenger3:
                    case ContactProperty.InstantMessenger4:
                    case ContactProperty.InstantMessenger5:
                    case ContactProperty.InstantMessenger6:
                    case ContactProperty.HomePagePersonal:
                    case ContactProperty.HomePageWork:
                    case ContactProperty.WorkCompany:
                    case ContactProperty.WorkDepartment:
                    case ContactProperty.WorkOffice:
                    case ContactProperty.WorkPosition:
                    case ContactProperty.AddressWorkStreet:
                    case ContactProperty.AddressWorkPostalCode:
                    case ContactProperty.AddressWorkCity:
                    case ContactProperty.AddressWorkState:
                    case ContactProperty.AddressWorkCountry:
                    case ContactProperty.Comment:
                    case ContactProperty.Spouse:
                    case ContactProperty.DisplayName:
                    case ContactProperty.FirstName:
                    case ContactProperty.MiddleName:
                    case ContactProperty.LastName:
                    case ContactProperty.NamePrefix:
                    case ContactProperty.NameSuffix:
                    case ContactProperty.NickName:
                    case ContactProperty.PhoneWork:
                    case ContactProperty.FaxHome:
                    case ContactProperty.FaxWork:
                        mapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        break;
                    case ContactProperty.Cell:
                    case ContactProperty.CellWork:
                        mapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        cellProperties++;
                        break;
                    case ContactProperty.PhoneHome:
                    case ContactProperty.PhoneOther1:
                    case ContactProperty.PhoneOther2:
                    case ContactProperty.PhoneOther3:
                    case ContactProperty.PhoneOther4:
                    case ContactProperty.PhoneOther5:
                    case ContactProperty.PhoneOther6:
                        mapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
                        phoneProperties++;
                        break;
                    case ContactProperty.Gender:
                        mapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item2.ToString(),
                                    new string[] { tpl.Item1 },
                                    new SexConverter(platform)));
                        break;
                    case ContactProperty.BirthDay:
                    case ContactProperty.Anniversary:
                    case ContactProperty.TimeStamp:
                        mapper.AddProperty(
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

            return mapper;
        }


        private static void FillCsvRecord(Contact contact, ContactProperty[] props, CsvRecordWrapper mapper, bool[] propInfo)
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
                ContactProperty prop = props[i];

                switch (prop)
                {
                    case ContactProperty.DisplayName:
                        mapper[i] = contact.DisplayName;
                        break;
                    case ContactProperty.FirstName:
                        mapper[i] = name?.FirstName;
                        break;
                    case ContactProperty.MiddleName:
                        mapper[i] = name?.MiddleName;
                        break;
                    case ContactProperty.LastName:
                        mapper[i] = name?.LastName;
                        break;
                    case ContactProperty.NamePrefix:
                        mapper[i] = name?.Prefix;
                        break;
                    case ContactProperty.NameSuffix:
                        mapper[i] = name?.Suffix;
                        break;
                    case ContactProperty.NickName:
                        mapper[i] = person?.NickName;
                        break;
                    case ContactProperty.Gender:
                        mapper[i] = person?.Gender;
                        break;
                    case ContactProperty.BirthDay:
                        mapper[i] = person?.BirthDay;
                        break;
                    case ContactProperty.Spouse:
                        mapper[i] = person?.Spouse;
                        break;
                    case ContactProperty.Anniversary:
                        mapper[i] = person?.Anniversary;
                        break;
                    case ContactProperty.AddressHomeStreet:
                        mapper[i] = homeAddress?.Street;
                        break;
                    case ContactProperty.AddressHomePostalCode:
                        mapper[i] = homeAddress?.PostalCode;
                        break;
                    case ContactProperty.AddressHomeCity:
                        mapper[i] = homeAddress?.City;
                        break;
                    case ContactProperty.AddressHomeState:
                        mapper[i] = homeAddress?.State;
                        break;
                    case ContactProperty.AddressHomeCountry:
                        mapper[i] = homeAddress?.Country;
                        break;
                    case ContactProperty.Email1:
                        mapper[i] = emails?.FirstOrDefault();
                        break;
                    case ContactProperty.Email2:
                        mapper[i] = emails?.ElementAtOrDefault(1);
                        break;
                    case ContactProperty.Email3:
                        mapper[i] = emails?.ElementAtOrDefault(2);
                        break;
                    case ContactProperty.Email4:
                        mapper[i] = emails?.ElementAtOrDefault(3);
                        break;
                    case ContactProperty.Email5:
                        mapper[i] = emails?.ElementAtOrDefault(4);
                        break;
                    case ContactProperty.Email6:
                        mapper[i] = emails?.ElementAtOrDefault(5);
                        break;
                    
#nullable disable
                    case ContactProperty.PhoneWork:
                        mapper[i] = phones?.FirstOrDefault(x => x.IsWork);
                        break;
                    case ContactProperty.FaxHome:
                        mapper[i] = phones?.FirstOrDefault(x => x.IsFax && !x.IsWork);
                        break;
                    case ContactProperty.FaxWork:
                        mapper[i] = phones?.FirstOrDefault(x => x.IsFax && x.IsWork);
                        break;
                    case ContactProperty.Cell:
                        if (propInfo[TWO_CELL_PROPERTIES])
                        {
                            mapper[i] = phones?.FirstOrDefault(x => x.IsCell && !x.IsWork);
                        }
                        else
                        {
                            mapper[i] = phones?.FirstOrDefault(x => x.IsCell);
                        }
                        break;
                    case ContactProperty.CellWork:
                        mapper[i] = phones?.FirstOrDefault(x => x.IsCell && x.IsWork);
                        break;
#nullable enable
                    case ContactProperty.PhoneHome:
                        mapper[i] = otherPhones?.FirstOrDefault();
                        break;
                    case ContactProperty.PhoneOther1:
                        if (propInfo[TWO_PHONE_PROPERTIES])
                        {
                            mapper[i] = otherPhones?.ElementAtOrDefault(1);
                        }
                        else
                        {
                            mapper[i] = otherPhones?.FirstOrDefault();
                        }
                        break;
                    case ContactProperty.PhoneOther2:
                        mapper[i] = otherPhones?.ElementAtOrDefault(2);
                        break;
                    case ContactProperty.PhoneOther3:
                        mapper[i] = otherPhones?.ElementAtOrDefault(3);
                        break;
                    case ContactProperty.PhoneOther4:
                        mapper[i] = otherPhones?.ElementAtOrDefault(4);
                        break;
                    case ContactProperty.PhoneOther5:
                        mapper[i] = otherPhones?.ElementAtOrDefault(5);
                        break;
                    case ContactProperty.PhoneOther6:
                        mapper[i] = otherPhones?.ElementAtOrDefault(6);
                        break;
                    case ContactProperty.InstantMessenger1:
                        mapper[i] = ims?.FirstOrDefault();
                        break;
                    case ContactProperty.InstantMessenger2:
                        mapper[i] = ims?.ElementAtOrDefault(1);
                        break;
                    case ContactProperty.InstantMessenger3:
                        mapper[i] = ims?.ElementAtOrDefault(2);
                        break;
                    case ContactProperty.InstantMessenger4:
                        mapper[i] = ims?.ElementAtOrDefault(3);
                        break;
                    case ContactProperty.InstantMessenger5:
                        mapper[i] = ims?.ElementAtOrDefault(4);
                        break;
                    case ContactProperty.InstantMessenger6:
                        mapper[i] = ims?.ElementAtOrDefault(5);
                        break;
                    case ContactProperty.HomePagePersonal:
                        mapper[i] = contact.HomePagePersonal;
                        break;
                    case ContactProperty.HomePageWork:
                        mapper[i] = contact.HomePageWork;
                        break;
                    case ContactProperty.WorkCompany:
                        mapper[i] = work?.Company;
                        break;
                    case ContactProperty.WorkDepartment:
                        mapper[i] = work?.Department;
                        break;
                    case ContactProperty.WorkOffice:
                        mapper[i] = work?.Office;
                        break;
                    case ContactProperty.WorkPosition:
                        mapper[i] = work?.Position;
                        break;
                    case ContactProperty.AddressWorkStreet:
                        mapper[i] = workAddress?.Street;
                        break;
                    case ContactProperty.AddressWorkPostalCode:
                        mapper[i] = workAddress?.PostalCode;
                        break;
                    case ContactProperty.AddressWorkCity:
                        mapper[i] = workAddress?.City;
                        break;
                    case ContactProperty.AddressWorkState:
                        mapper[i] = workAddress?.State;
                        break;
                    case ContactProperty.AddressWorkCountry:
                        mapper[i] = workAddress?.Country;
                        break;
                    case ContactProperty.Comment:
                        mapper[i] = contact.Comment;
                        break;
                    case ContactProperty.TimeStamp:
                        DateTime? timeStamp = contact.TimeStamp;
                        mapper[i] = timeStamp == default(DateTime) ? null : timeStamp;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
