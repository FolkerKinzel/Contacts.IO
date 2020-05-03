using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;

namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Statische Klasse zum Laden und Speichern von <see cref="Contact"/>-Objekten.
    /// </summary>
    /// <example>
    /// <note type="important">Die Beispiele enthalten - der besseren Lesbarkeit wegen - keine Ausnahmebehandlung.</note>
    /// <para>Initialisieren von <see cref="Contact"/>-Objekten:</para>
    /// <code language="cs" source="..\Examples\ContactExample.cs"/>
    /// <para>
    /// Lesen und Schreiben von vCards (*.vcf):
    /// </para>
    /// <code language="cs" source="..\Examples\VCardExample.cs" />
    /// <para>
    /// Lesen und Schreiben von CSV-Dateien:
    /// </para>
    /// <code language="cs" source="..\Examples\CsvExample.cs" />
    /// </example>
    public static class ContactPersistence
    {
        /// <summary>
        /// Lädt den Inhalt einer CSV-Datei als <see cref="List{T}"/> von <see cref="Contact"/>-Objekten.
        /// </summary>
        /// <param name="fileName">Dateipfad der CSV-Datei.</param>
        /// <param name="platform">Die Plattform, von der die CSV-Datei stammt.</param>
        /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>
        /// für <see cref="CultureInfo.InvariantCulture"/>.</param>
        /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für <see cref="Encoding.UTF8"/>.</param>
        /// <returns>Inhalt der CSV-Datei als <see cref="List{T}"/> von <see cref="Contact"/>-Objekten.</returns>
        ///<exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">
        /// <para>Es kann nicht auf den Datenträger zugegriffen werden</para>
        /// <para>- oder -</para>
        /// <para>die Datei enthält ungültiges CSV.</para></exception>
        public static List<Contact> LoadCsv(string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        {
            return CsvReader.GetInstance(platform, formatProvider, textEncoding).Read(fileName);
        }


        /// <summary>
        /// Speichert den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine CSV-Datei.
        /// </summary>
        /// <param name="fileName">Dateipfad der CSV-Datei.</param>
        /// <param name="contacts">Die zu speichernde Sammlung von <see cref="Contact"/>-Objekten.</param>
        /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
        /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>
        /// für <see cref="CultureInfo.InvariantCulture"/>.</param>
        /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für UTF-8 mit BOM (<see cref="Encoding.UTF8"/>).</param>
        /// <exception cref = "ArgumentNullException"><paramref name="fileName"/> oder <paramref name="contacts"/> ist<c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
        /// <para>- oder -</para>
        /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
        /// </exception>
        /// <exception cref="IOException">E/A-Fehler.</exception>
        public static void SaveCsv(string fileName, IEnumerable<Contact> contacts, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        {
            CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, contacts);
        }


 

        /// <summary>
        /// Lädt eine vCard-Datei (*.vcf) und gibt ihre Daten als <see cref="Contact"/>-Array zurück. (Eine vcf-Datei kann
        /// mehrere aneinandergehängte vCards enthalten.) Enthält die Datei keinen Text, wird ein leeres Array zurückgegeben.
        /// </summary>
        /// <param name="fileName">Der vollständige Pfad der vCard-Datei.</param>
        /// <returns>Die geladenen Daten als <see cref="Contact"/>-Array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
        public static List<Contact> LoadVCard(string fileName)
        {
            return VcfReader.Read(fileName);
        }





        /// <summary>
        /// Speichert den Inhalt einer Auflistung  von <see cref="Contact"/>-Objekten in eine gemeinsame 
        /// vCard-Datei (*.vcf).
        /// </summary>
        /// /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="contacts">Auflistung der in eine gemeinsame vCard-Datei zu schreibenden <see cref="Contact"/>-Objekte.
        /// Die Auflistung darf leer sein oder <c>null</c>-Werte
        /// enthalten. Wenn die Auflistung kein <see cref="Contact"/>-Objekt enthält, das Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        /// <remarks><paramref name="contacts"/> darf nicht <c>null</c> sein, aber <c>null</c>-Werte enthalten.</remarks>
        public static void SaveVCard(string fileName, IEnumerable<Contact?> contacts, VCardVersion version = VCardVersion.V3_0)
        {
            VcfWriter.Write(contacts, fileName, version);
        }


    }
}
