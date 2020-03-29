using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv
{
    internal abstract class CsvIOBase
    {
        protected const int TWO_CELL_PROPERTIES = 0;
        protected const int TWO_PHONE_PROPERTIES = 1;
        private const int PROPINFO_LENGTH = 2;

        private Conv::ICsvTypeConverter? _nullableDateTimeConverter;


        protected Conv::ICsvTypeConverter StringConverter { get; } = Conv::CsvConverterFactory.CreateConverter(Conv::CsvTypeCode.String, nullable: true);


        protected Conv::ICsvTypeConverter NullableDateTimeConverter
        {
            get
            {
                this._nullableDateTimeConverter ??= InitNullableDateTimeConverter();

                return this._nullableDateTimeConverter;
            }
        }

        protected virtual ICsvTypeConverter InitNullableDateTimeConverter() => Conv::CsvConverterFactory.CreateConverter(CsvTypeCode.DateTime, true);


        protected virtual ICsvTypeConverter InitNonNullableDateTimeConverter() => Conv::CsvConverterFactory.CreateConverter(CsvTypeCode.DateTime, false);


        /// <summary>
        /// Ein <see cref="bool"/>-Array, das Informationen über das doppelte Vorkommen ähnlicher Parameter sammelt.
        /// </summary>
        protected readonly bool[] PropInfo = new bool[PROPINFO_LENGTH];


        /// <summary>
        /// Erzeugt die Zuordnung zwischen Eigenschaftsnamen von <see cref="CsvRecordWrapper"/>, Eigenschaft von <see cref="Contact"/> und Spaltenname der CSV-Datei.
        /// </summary>
        /// <returns>
        /// <para>Eine Auflistung von <see cref="Tuple{T1, T2, T3}"/>.</para>
        /// <para>Inhalt:</para>
        /// <list type="bullet">
        /// <item><see cref="Tuple{T1, T2, T3}.Item1"/>: Eigenschaftsname von <see cref="CsvRecordWrapper"/>.</item>
        /// <item><see cref="Tuple{T1, T2, T3}.Item2"/>: Eigenschaft von <see cref="Contact"/>.</item>
        /// <item><see cref="Tuple{T1, T2, T3}.Item3"/>: Spaltenname der CSV-Datei und evtl. Aliase</item>
        /// </list>
        /// </returns>
        protected abstract IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping();



        /// <summary>
        /// Initialisiert ein <see cref="CsvRecordWrapper"/>-Objekt.
        /// </summary>
        /// <param name="mapping">
        /// <para>Eine Collection von <see cref="Tuple{T1, T2, T3}"/>.</para>
        /// <para>Inhalt:</para>
        /// <list type="bullet">
        /// <item><see cref="Tuple{T1, T2, T3}.Item1"/>: Eigenschaftsname von <see cref="CsvRecordWrapper"/>.</item>
        /// <item><see cref="Tuple{T1, T2, T3}.Item2"/>: Eigenschaft von <see cref="Contact"/>.</item>
        /// <item><see cref="Tuple{T1, T2, T3}.Item3"/>: Spaltenname der CSV-Datei und evtl. Aliase</item>
        /// </list>
        /// </param>
        /// <returns>Ein <see cref="CsvRecordWrapper"/>-Objekt.</returns>
        protected CsvRecordWrapper InitCsvRecordWrapper(IEnumerable<Tuple<string, ContactProp?, IList<string>>> mapping)
        {
            var wrapper = new CsvRecordWrapper();


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
                    //case ContactProp.Email3:
                    //case ContactProp.Email4:
                    //case ContactProp.Email5:
                    //case ContactProp.Email6:
                    case ContactProp.InstantMessenger1:
                    case ContactProp.InstantMessenger2:
                    //case ContactProp.InstantMessenger3:
                    //case ContactProp.InstantMessenger4:
                    //case ContactProp.InstantMessenger5:
                    //case ContactProp.InstantMessenger6:
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
                                tpl.Item1,
                                tpl.Item3,
                                StringConverter));
                        break;
                    case ContactProp.Cell:
                    case ContactProp.CellWork:
                        wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item1,
                                tpl.Item3,
                                StringConverter));
                        cellProperties++;
                        break;
                    case ContactProp.PhoneHome:
                    case ContactProp.PhoneOther1:
                    case ContactProp.PhoneOther2:
                    case ContactProp.PhoneOther3:
                    //case ContactProp.PhoneOther4:
                    //case ContactProp.PhoneOther5:
                    //case ContactProp.PhoneOther6:
                        wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item1,
                                tpl.Item3,
                                StringConverter));
                        phoneProperties++;
                        break;
                    case ContactProp.Gender:
                        wrapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item1,
                                    tpl.Item3,
                                    InitSexConverter()));
                        break;
                    case ContactProp.BirthDay:
                    case ContactProp.Anniversary:
                        wrapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item1,
                                    tpl.Item3,
                                    NullableDateTimeConverter));
                        break;
                    case ContactProp.TimeStamp:
                        wrapper.AddProperty(
                                new CsvProperty(
                                    tpl.Item1,
                                    tpl.Item3,
                                    InitNonNullableDateTimeConverter()));
                        break;
                    default:
                        if (tpl.Item2.HasValue)
                        {
                            InitCsvRecordWrapperUndefinedValues(tpl, wrapper);
                        }
                        else
                        {
                            wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item1,
                                tpl.Item3,
                                StringConverter));
                        }
                        break;
                }
            }

            PropInfo[TWO_CELL_PROPERTIES] = (cellProperties >= 2);
            PropInfo[TWO_PHONE_PROPERTIES] = (cellProperties >= 2);

            return wrapper;
        }


        protected virtual SexConverter InitSexConverter() => new SexConverter();


        /// <summary>
        /// Abgeleitete Klassen können dem Mapping nicht definierte Werte der <see cref="ContactProp"/>-Enum hinzufügen, um inkompatible CSV-Spalten zu befüllen
        /// oder aus diesen zu lesen.
        /// Diese Spalten werden dem <see cref="CsvRecordWrapper"/> hiermit hinzugefügt.
        /// </summary>
        /// <param name="tpl">Tuple aus dem Mapping mit nichtdefiniertem Wert der <see cref="ContactProp"/>-Enum.</param>
        /// <param name="wrapper">Zu initialisierendes <see cref="Csv::Helpers.CsvRecordWrapper"/>-Objekt.</param>
        protected virtual void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper) { }
        


    }
}
