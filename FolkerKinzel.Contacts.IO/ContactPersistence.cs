using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;
using System;
using System.Collections.Generic;
using System.IO;

namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Statische Klasse zum Importieren und Exportieren von Kontaktdaten.
    /// </summary>
    public static class ContactPersistence
    {

        public static List<Contact> ReadCsv(string fileName, CsvTarget platform = CsvTarget.Unspecified)
        {
            return CsvReader.GetInstance(platform).Read(fileName);
        }


        /// <summary>
        /// Schreibt den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine CSV-Datei.
        /// </summary>
        /// <param name="fileName">Dateipfad der zu schreibenden CSV-Datei.</param>
        /// <param name="data">Die zu persistierenden <see cref="Contact"/>-Objekte.</param>
        /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
        /// <exception cref = "ArgumentNullException"><paramref name="fileName"/> oder <paramref name="data"/> ist<c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
        /// <para>- oder -</para>
        /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
        /// </exception>
        /// <exception cref="IOException">E/A-Fehler.</exception>
        public static void WriteCsv(string fileName, IEnumerable<Contact> data, CsvTarget platform = CsvTarget.Unspecified)
        {
            CsvWriter.GetInstance(platform).Write(fileName, data);
        }

        /// <summary>
        /// Liest eine vCard-Datei und gibt ihre Daten als <see cref="Contact"/>-Array zurück. (Eine vCard-Datei kann
        /// mehrere aneinandergehängte vCards enthalten.) Enthält die Datei keinen Text, wird ein leeres Array zurückgegeben.
        /// </summary>
        /// <param name="fileName">Der vollständige Pfad der vCard-Datei.</param>
        /// <returns>Die Daten der vCard als <see cref="Contact"/>-Array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
        public static Contact[] ReadVCard(string fileName)
        {
            return VcfReader.Read(fileName);
        }


        /// <summary>
        /// Schreibt den Inhalt eines <see cref="Contact"/>-Objekts in eine vCard-Datei.
        /// </summary>
        /// <param name="contact">Das zu serialisierende <see cref="Contact"/>-Objekt. Wenn <paramref name="contact"/>&#160;<c>null</c> ist
        /// oder keine Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        public static void WriteVCard(Contact? contact, string fileName, VCardVersion version = VCardVersion.V3_0)
        {
            VcfWriter.Write(contact, fileName, version);
        }


        /// <summary>
        /// Schreibt den Inhalt einer Auflistung  von <see cref="Contact"/>-Objekten in eine gemeinsame 
        /// vCard-Datei.
        /// </summary>
        /// <param name="contacts">Auflistung der in eine gemeinsame vCard-Datei zu schreibenden <see cref="Contact"/>-Objekte.
        /// Die Auflistung darf leer sein oder <c>null</c>-Werte
        /// enthalten. Wenn die Auflistung kein <see cref="Contact"/>-Objekt enthält, das Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        /// <remarks><paramref name="contacts"/> darf nicht null sein, aber null-Werte enthalten.</remarks>
        public static void WriteVCard(IEnumerable<Contact?> contacts, string fileName, VCardVersion version = VCardVersion.V3_0)
        {
            VcfWriter.Write(contacts, fileName, version);
        }


    }
}
