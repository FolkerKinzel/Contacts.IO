using FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace FolkerKinzel.Contacts.IO
{
    public static partial class ContactPersistence
    {
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
        public static void WriteCsv(string fileName, IEnumerable<Contact> data, CsvMappingCollection mapping, CsvTarget platform = CsvTarget.NotSpecified)
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

            using var writer = new CsvWriter(fileName, mapping.Select(x => x.Item1).ToArray());
            var mapper = InitCsvRecordMapper(mapping, writer, platform);


            var keys = mapping.Select(x => x.Item2).ToArray();

            foreach (var contact in data)
            {
                if (contact is null) continue;
                contact.Clean();

                if (contact.IsEmpty) continue;

                FillCsvRecord(contact, keys,  mapper);
                writer.WriteRecord();
            }
        }

        
        /// <summary>
        /// Initialisiert ein <see cref="CsvRecordWrapper"/>-Objekt.
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="writer"></param>
        /// <param name="platform"></param>
        /// <returns>Ein <see cref="CsvRecordWrapper"/>-Objekt.</returns>
        private static CsvRecordWrapper InitCsvRecordMapper(IEnumerable<Tuple<string, ContactProperty>> mapping, CsvWriter writer, CsvTarget platform)
        {
            var mapper = new CsvRecordWrapper(writer.Record);

            var stringConverter = Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.String, nullable: true);

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
                    case ContactProperty.PhoneHome:
                    case ContactProperty.PhoneWork:
                    case ContactProperty.FaxHome:
                    case ContactProperty.FaxWork:
                    case ContactProperty.Cell:
                    case ContactProperty.CellWork:
                    case ContactProperty.PhoneOther1:
                    case ContactProperty.PhoneOther2:
                    case ContactProperty.PhoneOther3:
                    case ContactProperty.PhoneOther4:
                    case ContactProperty.PhoneOther5:
                    case ContactProperty.PhoneOther6:
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
                        mapper.AddProperty(
                            new CsvProperty(
                                tpl.Item2.ToString(),
                                new string[] { tpl.Item1 },
                                stringConverter));
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
                        mapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item2.ToString(),
                                    new string[] { tpl.Item1 },
                                    Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.DateTime, nullable: true)));
                        break;
                    case ContactProperty.TimeStamp:
                        mapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item2.ToString(),
                                    new string[] { tpl.Item1 },
                                    Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.DateTime, nullable: false)));
                        break;
                    default:
                        break;
                }
            }

            return mapper;
        }


        private static void FillCsvRecord(Contact contact, ContactProperty[] props, CsvRecordWrapper mapper)
        {
            for (int i = 0; i < props.Length; i++)
            {
                ContactProperty prop = props[i];

                switch (prop)
                {
                    case ContactProperty.DisplayName:
                        mapper[i] = contact.DisplayName;
                        break;
                    case ContactProperty.FirstName:
                        break;
                    case ContactProperty.MiddleName:
                        break;
                    case ContactProperty.LastName:
                        break;
                    case ContactProperty.NamePrefix:
                        break;
                    case ContactProperty.NameSuffix:
                        break;
                    case ContactProperty.NickName:
                        break;
                    case ContactProperty.Gender:
                        break;
                    case ContactProperty.BirthDay:
                        break;
                    case ContactProperty.Spouse:
                        break;
                    case ContactProperty.Anniversary:
                        break;
                    case ContactProperty.AddressHomeStreet:
                        break;
                    case ContactProperty.AddressHomePostalCode:
                        break;
                    case ContactProperty.AddressHomeCity:
                        break;
                    case ContactProperty.AddressHomeState:
                        break;
                    case ContactProperty.AddressHomeCountry:
                        break;
                    case ContactProperty.Email1:
                        break;
                    case ContactProperty.Email2:
                        break;
                    case ContactProperty.Email3:
                        break;
                    case ContactProperty.Email4:
                        break;
                    case ContactProperty.Email5:
                        break;
                    case ContactProperty.Email6:
                        break;
                    case ContactProperty.PhoneHome:
                        break;
                    case ContactProperty.PhoneWork:
                        break;
                    case ContactProperty.FaxHome:
                        break;
                    case ContactProperty.FaxWork:
                        break;
                    case ContactProperty.Cell:
                        break;
                    case ContactProperty.CellWork:
                        break;
                    case ContactProperty.PhoneOther1:
                        break;
                    case ContactProperty.PhoneOther2:
                        break;
                    case ContactProperty.PhoneOther3:
                        break;
                    case ContactProperty.PhoneOther4:
                        break;
                    case ContactProperty.PhoneOther5:
                        break;
                    case ContactProperty.PhoneOther6:
                        break;
                    case ContactProperty.InstantMessenger1:
                        break;
                    case ContactProperty.InstantMessenger2:
                        break;
                    case ContactProperty.InstantMessenger3:
                        break;
                    case ContactProperty.InstantMessenger4:
                        break;
                    case ContactProperty.InstantMessenger5:
                        break;
                    case ContactProperty.InstantMessenger6:
                        break;
                    case ContactProperty.HomePagePersonal:
                        break;
                    case ContactProperty.HomePageWork:
                        break;
                    case ContactProperty.WorkCompany:
                        break;
                    case ContactProperty.WorkDepartment:
                        break;
                    case ContactProperty.WorkOffice:
                        break;
                    case ContactProperty.WorkPosition:
                        break;
                    case ContactProperty.AddressWorkStreet:
                        break;
                    case ContactProperty.AddressWorkPostalCode:
                        break;
                    case ContactProperty.AddressWorkCity:
                        break;
                    case ContactProperty.AddressWorkState:
                        break;
                    case ContactProperty.AddressWorkCountry:
                        break;
                    case ContactProperty.Comment:
                        break;
                    case ContactProperty.TimeStamp:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
