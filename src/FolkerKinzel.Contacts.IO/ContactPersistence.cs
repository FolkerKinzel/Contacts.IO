using System.ComponentModel;
using System.Globalization;
using System.Text;
using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;

namespace FolkerKinzel.Contacts.IO;

/// <summary>
/// Statische Klasse zum Laden und Speichern von <see cref="Contact"/>-Objekten als vCard (*.vcf) oder CSV.
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
    /// Lädt den Inhalt einer CSV-Datei als <see cref="List{T}">Liste</see> von <see cref="Contact"/>-Objekten.
    /// </summary>
    /// 
    /// <param name="fileName">Der Dateipfad der CSV-Datei.</param>
    /// <param name="platform">Die Plattform, von der die CSV-Datei stammt.</param>
    /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>,
    /// um automatisch die am besten geeignete <see cref="CultureInfo"/> aussuchen zu lassen.</param>
    /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für <see cref="Encoding.UTF8"/>.</param>
    /// 
    /// <returns>Inhalt der CSV-Datei als <see cref="List{T}">Liste</see> von <see cref="Contact"/>-Objekten.</returns>
    /// 
    /// <remarks>Die Methode führt auf jedem zurückgegebenen <see cref="Contact"/>-Objekt <see cref="Contact.Clean"/> aus,
    /// weshalb es in der Regel nicht nötig ist, <see cref="Contact.Clean"/> in eigenem Code aufzurufen.</remarks>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">
    /// <para>Es kann nicht auf den Datenträger zugegriffen werden</para>
    /// <para>- oder -</para>
    /// <para>die Datei enthält ungültiges CSV.</para>
    /// </exception>
    public static List<Contact> LoadCsv
        (string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        => CsvReader.GetInstance(platform, formatProvider, textEncoding).Read(fileName);


    /// <summary>
    /// Speichert den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine CSV-Datei.
    /// </summary>
    /// 
    /// <param name="fileName">Der Dateipfad der zu erzeugenden CSV-Datei. Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="contacts">
    /// <para>
    /// Die zu speichernde Sammlung von <see cref="Contact"/>-Objekten.
    /// </para>
    /// <para>
    /// Die Sammlung darf leer sein oder <c>null</c>-Werte
    /// enthalten. Wenn die Sammlung kein <see cref="Contact"/>-Objekt enthält, das Daten enthält, wird eine leere Datei erzeugt.
    /// </para>
    /// </param>
    /// <param name="platform">Die Plattform, für die die CSV-Datei bestimmt ist.</param>
    /// <param name="formatProvider">Ein Objekt, das kulturabhängige Formatierungsinformationen bereitstellt, oder <c>null</c>,
    /// um automatisch die am besten geeignete für <see cref="CultureInfo"/> aussuchen zu lassen.</param>
    /// <param name="textEncoding">Die zu verwendende Textkodierung oder <c>null</c> für UTF-8 mit BOM (<see cref="Encoding.UTF8"/>).</param>
    /// 
    /// <remarks>
    /// <para>
    /// Die Methode ruft auf allen als Argument übergebenen <see cref="Contact"/>-Objekten <see cref="Contact.Clean"/> auf. Alle
    /// <see cref="Contact"/>-Objekte deren Eigenschaft <see cref="Contact.IsEmpty"/> danach <c>true</c> zurückgibt, werden nicht in 
    /// die Datei geschrieben.
    /// </para>
    /// <para>
    /// Falls es unerwünscht ist, dass die Methode die <see cref="Contact"/>-Objekte durch den Aufruf von <see cref="Contact.Clean"/> ändert,
    /// können Sie vorher mit <see cref="Contact.Clone"/>
    /// Kopien der <see cref="Contact"/>-Objekte erstellen und der Methode dann die Kopien übergeben.
    /// </para>
    /// </remarks>
    /// 
    /// <exception cref = "ArgumentNullException"><paramref name="fileName"/> oder <paramref name="contacts"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="platform"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">E/A-Fehler.</exception>
    public static void SaveCsv
        (string fileName, IEnumerable<Contact?> contacts, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        => CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, contacts);

//    [Obsolete("Use LoadVcf instead.")]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    [Browsable(false)]
//#pragma warning disable CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element
//    public static List<Contact> LoadVCard(string fileName) => LoadVcf(fileName);
//#pragma warning restore CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element

    /// <summary>
    /// Lädt eine vCard-Datei (*.vcf) und gibt ihre Daten als <see cref="List{T}">Liste</see> von <see cref="Contact"/>-Objekten zurück. (Eine VCF-Datei kann
    /// mehrere aneinandergehängte vCards enthalten.)
    /// </summary>
    /// 
    /// <param name="fileName">Der Dateipfad der VCF-Datei.</param>
    /// 
    /// <returns>Die geladenen Daten als <see cref="List{T}">Liste</see> von <see cref="Contact"/>-Objekten. Enthält 
    /// die Datei keinen Text, wird eine leere Liste zurückgegeben.</returns>
    /// 
    /// <remarks>Die Methode führt auf jedem zurückgegebenen <see cref="Contact"/>-Objekt <see cref="Contact.Clean"/> aus,
    /// weshalb es in der Regel nicht nötig ist, <see cref="Contact.Clean"/> in eigenem Code aufzurufen.</remarks>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
    /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
    public static List<Contact> LoadVcf(string fileName) => VcfReader.Read(fileName);

//    [Obsolete("Use SaveVcf instead.")]
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    [Browsable(false)]
//#pragma warning disable CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element
//    public static void SaveVCard(string fileName, IEnumerable<Contact?> contacts, VCardVersion version = VCardVersion.V3_0)
//#pragma warning restore CS1591 // Fehlender XML-Kommentar für öffentlich sichtbaren Typ oder Element
//            => SaveVcf(fileName, contacts, version);

    /// <summary>
    /// Speichert den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine gemeinsame 
    /// vCard-Datei (*.vcf).
    /// </summary>
    /// 
    /// <param name="fileName">Der Dateipfad der zu erzeugenden VCF-Datei.
    /// Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="contacts">
    /// <para>
    /// Die zu speichernde Sammlung von <see cref="Contact"/>-Objekten.
    /// </para>
    /// <para>
    /// Die Sammlung darf leer sein oder <c>null</c>-Werte
    /// enthalten. Wenn die Sammlung kein <see cref="Contact"/>-Objekt enthält, das Daten enthält, wird keine Datei erzeugt.
    /// </para>
    /// </param>
    /// <param name="version">Dateiversion der zu speichernden vCard.</param>
    /// 
    /// <remarks>
    /// <para>
    /// Die Methode ruft auf allen als Argument übergebenen <see cref="Contact"/>-Objekten <see cref="Contact.Clean"/> auf. Alle
    /// <see cref="Contact"/>-Objekte deren Eigenschaft <see cref="Contact.IsEmpty"/> danach <c>true</c> zurückgibt, werden nicht in 
    /// die Datei geschrieben.
    /// </para>
    /// <para>
    /// Falls es unerwünscht ist, dass die Methode die <see cref="Contact"/>-Objekte durch den Aufruf von <see cref="Contact.Clean"/> ändert,
    /// können Sie vorher mit <see cref="Contact.Clone"/>
    /// Kopien der <see cref="Contact"/>-Objekte erstellen und der Methode dann die Kopien übergeben.
    /// </para>
    /// </remarks>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para><paramref name="fileName"/> ist kein gültiger Dateipfad.</para>
    /// <para>- oder -</para>
    /// <para><paramref name="version"/> hat einen nichtdefinierten Wert.</para>
    /// </exception>
    /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
    public static void SaveVcf(string fileName, IEnumerable<Contact?> contacts, VCardVersion version = VCardVersion.V3_0)
        => VcfWriter.Write(contacts, fileName, version);
}
