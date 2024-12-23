using System.ComponentModel;
using System.Globalization;
using System.Text;
using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;

namespace FolkerKinzel.Contacts.IO;

/// <summary>
/// Erweiterungsmethoden für die <see cref="Contact"/>-Klasse.
/// </summary>
public static class ContactExtension
{
    /// <summary>
    /// Speichert den Inhalt eines <see cref="Contact"/>-Objekts als CSV-Datei.
    /// </summary>
    /// <remarks>
    /// <para>Die Methode erzeugt bei jedem Aufruf eine neue CSV-Datei. Wenn Sie mehrere <see cref="Contact"/>-Objekte in einer 
    /// gemeinsamen CSV-Datei speichern möchten, eignet sich die Methode 
    /// <see cref="ContactPersistence.SaveCsv(string, IEnumerable{Contact?}, CsvCompatibility, IFormatProvider?, Encoding?)"/>
    /// oder die Erweiterungsmethode <see cref="ContactCollectionExtension.SaveCsv(IEnumerable{Contact?}, string, CsvCompatibility, IFormatProvider?, Encoding?)"/>.</para>
    /// <para>
    /// Die Methode ruft auf <paramref name="contact"/>&#160;<see cref="Contact.Clean"/> auf. Wenn
    /// die Eigenschaft <see cref="Contact.IsEmpty"/> von <paramref name="contact"/> danach <c>true</c> zurückgibt, wird eine leere Datei erzeugt.
    /// Falls es unerwünscht ist, dass die Methode <paramref name="contact"/> durch den Aufruf von <see cref="Contact.Clean"/> ändert,
    /// können Sie vorher mit <see cref="Contact.Clone"/> eine
    /// Kopie von <paramref name="contact"/> erstellen und der Methode dann die Kopie übergeben.
    /// </para>
    /// </remarks>
    /// <param name="contact">Das zu speichernde <see cref="Contact"/>-Objekt.</param>
    /// <param name="fileName">Der Dateipfad der zu erzeugenden CSV-Datei. Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
    /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>, um automatisch 
    /// die am besten geeignete <see cref="CultureInfo"/> aussuchen zu lassen.</param>
    /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für UTF-8 mit BOM (<see cref="Encoding.UTF8"/>).</param>
    /// <exception cref = "ArgumentNullException"><paramref name="contact"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">E/A-Fehler.</exception>
    public static void SaveCsv(this Contact contact, string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
    {
        if (contact is null)
        {
            throw new ArgumentNullException(nameof(contact));
        }

        CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, new Contact[] { contact });
    }

//    [Obsolete("Use SaveVcf instead.")]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    [Browsable(false)]
//#pragma warning disable CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element
//    public static void SaveVCard(this Contact contact, string fileName, VCardVersion version = VCardVersion.V3_0)
//#pragma warning restore CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element
//            => SaveVcf(contact, fileName, version);

    /// <summary>
    /// Speichert den Inhalt eines <see cref="Contact"/>-Objekts als vCard-Datei (.vcf).
    /// </summary>
    /// <param name="contact">Das zu speichernde <see cref="Contact"/>-Objekt. Wenn <paramref name="contact"/> keine Daten enthält,
    /// wird keine Datei erzeugt.</param>
    /// <param name="fileName">Der Dateipfad der zu erzeugenden VCF-Datei. 
    /// Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="version">Dateiversion der zu speichernden VCF-Datei.</param>
    /// 
    /// <remarks>
    /// <para>
    /// Die Methode ruft auf <paramref name="contact"/>&#160;<see cref="Contact.Clean"/> auf. Wenn
    /// die Eigenschaft <see cref="Contact.IsEmpty"/> von <paramref name="contact"/> danach <c>true</c> zurückgibt, wird eine keine Datei erzeugt.
    /// Falls es unerwünscht ist, dass die Methode <paramref name="contact"/> durch den Aufruf von <see cref="Contact.Clean"/> ändert,
    /// können Sie vorher mit <see cref="Contact.Clone"/> eine
    /// Kopie von <paramref name="contact"/> erstellen und der Methode dann die Kopie übergeben.
    /// </para>
    /// <para>Zum Speichern mehrerer <see cref="Contact"/>-Objekte in einer gemeinsamen VCF-Datei eignet sich die
    /// Methode <see cref="ContactPersistence.SaveVcf(string, IEnumerable{Contact?}, VCardVersion)"/> oder die 
    /// Erweiterungsmethode <see cref="ContactCollectionExtension.SaveVcf(IEnumerable{Contact?}, string, VCardVersion)"/>.
    /// </para>
    /// </remarks>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="contact"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="version"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
    public static void SaveVcf(this Contact contact, string fileName, VCardVersion version = VCardVersion.V3_0)
        => VcfWriter.Write(contact, fileName, version);
}
