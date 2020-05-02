using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Enthält Erweiterungsmethoden für die <see cref="Contact"/>-Klasse.
    /// </summary>
    public static class ContactExtension
    {
        /// <summary>
        /// Speichert den Inhalt eines <see cref="Contact"/>-Objekts als CSV-Datei.
        /// </summary>
        /// <param name="contact">Das zu speichernde <see cref="Contact"/>-Objekt.</param>
        /// <param name="fileName">Dateipfad der CSV-Datei.</param>
        /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
        /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>
        /// für <see cref="CultureInfo.InvariantCulture"/>.</param>
        /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für UTF-8 mit BOM (<see cref="Encoding.UTF8"/>).</param>
        /// <exception cref = "ArgumentNullException"><paramref name="contact"/> oder <paramref name="fileName"/> ist<c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
        /// <para>- oder -</para>
        /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
        /// </exception>
        /// <exception cref="IOException">E/A-Fehler.</exception>
        /// <remarks>
        /// Die Methode kann nur ein einzelnes <see cref="Contact"/>-Objekt pro CSV-Datei speichern. Das wird
        /// nur selten benötigt. Meistens werden Sie zum Speichern von CSV-Dateien die Methode 
        /// <see cref="ContactPersistence.SaveCsv(string, System.Collections.Generic.IEnumerable{Contact}, CsvCompatibility, IFormatProvider?, Encoding?)"/>
        /// verwenden.
        /// </remarks>
        public static void SaveCsv(this Contact contact, string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        {
            if (contact is null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, new Contact[] { contact });
        }


        /// <summary>
        /// Speichert den Inhalt eines <see cref="Contact"/>-Objekts als vCard-Datei (.vcf).
        /// </summary>
        /// <param name="contact">Das zu speichernde <see cref="Contact"/>-Objekt. Wenn <paramref name="contact"/>&#160;<c>null</c> ist
        /// oder keine Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="contact"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        /// <remarks>Verwenden Sie zum Speichern mehrerer <see cref="Contact"/>-Objekte in einer gemeinsamen vcf-Datei die
        /// Methode <see cref="ContactPersistence.SaveVCard(string, System.Collections.Generic.IEnumerable{Contact?}, VCardVersion)"/>!</remarks>
        public static void SaveVCard(this Contact contact, string fileName, VCardVersion version = VCardVersion.V3_0)
        {
            VcfWriter.Write(contact, fileName, version);
        }

    }
}
