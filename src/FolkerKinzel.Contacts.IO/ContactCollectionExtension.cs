using System.Globalization;
using System.Text;

namespace FolkerKinzel.Contacts.IO;

/// <summary>
/// Erweiterungsmethoden für <see cref="IEnumerable{T}">IEnumerable&lt;Contact?&gt;</see>.
/// </summary>
public static class ContactCollectionExtension
{
    /// <summary>
    /// Speichert den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten als CSV-Datei.
    /// </summary>
    /// 
    /// <param name="contacts">Die zu speichernde Sammlung von <see cref="Contact"/>-Objekten.</param>
    /// <param name="fileName">Der Dateipfad der zu erzeugenden CSV-Datei. Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
    /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>, um automatisch 
    /// die am besten geeignete <see cref="CultureInfo"/> aussuchen zu lassen.</param>
    /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für UTF-8 mit BOM (<see cref="Encoding.UTF8"/>).</param>
    /// 
    /// <remarks>
    /// Die Methode ruft auf jedem <see cref="Contact"/>-Objekt in <paramref name="contacts"/>&#160;<see cref="Contact.Clean"/> auf.
    /// </remarks>
    /// 
    /// <exception cref = "ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">E/A-Fehler.</exception>
    public static void SaveCsv(this IEnumerable<Contact?> contacts,
                               string fileName,
                               CsvCompatibility platform,
                               IFormatProvider? formatProvider = null,
                               Encoding? textEncoding = null)
        => ContactPersistence.SaveCsv(fileName, contacts, platform, formatProvider, textEncoding);


    /// <summary>
    /// Speichert den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten als gemeinsame vCard-Datei (.vcf).
    /// </summary>
    /// <param name="contacts">Die zu speichernde Sammlung von <see cref="Contact"/>-Objekten. 
    /// Wenn <paramref name="contacts"/> keine Daten enthält, wird keine Datei erzeugt.</param>
    /// <param name="fileName">Der Dateipfad der zu erzeugenden VCF-Datei. 
    /// Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="version">Dateiversion der zu speichernden VCF-Datei.</param>
    /// 
    /// <remarks>
    /// Die Methode ruft auf jedem <see cref="Contact"/>-Objekt in <paramref name="contacts"/>&#160;<see cref="Contact.Clean"/> auf.
    /// </remarks>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="version"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
    public static void SaveVcf(this IEnumerable<Contact?> contacts,
                                 string fileName,
                                 VCardVersion version = VCardVersion.V3_0)
        => ContactPersistence.SaveVcf(fileName, contacts, version);
}
