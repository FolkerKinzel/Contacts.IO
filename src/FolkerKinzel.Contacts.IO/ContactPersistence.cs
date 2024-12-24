using System.ComponentModel;
using System.Globalization;
using System.Text;
using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;

namespace FolkerKinzel.Contacts.IO;

/// <summary>Static class for loading and saving <see cref="Contact" /> objects
/// as vCard (*.vcf) or CSV.</summary>
/// <example>
/// <note type="important">
/// For the sake of better readability, the examples do not contain any exception
/// handling.
/// </note>
/// <para>
/// Initializing <see cref="Contact" /> objects:
/// </para>
/// <code language="cs" source="..\Examples\ContactExample.cs" />
/// <para>
/// Reading and writing vCards (* .vcf):
/// </para>
/// <code language="cs" source="..\Examples\VCardExample.cs" />
/// <para>
/// Reading and writing CSV files:
/// </para>
/// <code language="cs" source="..\Examples\CsvExample.cs" />
/// </example>
public static class ContactPersistence
{
    /// <summary>Loads the content of a CSV file as a generic <see cref="List{T}">List</see>
    /// of <see cref="Contact" /> objects.</summary>
    /// <param name="fileName">The file path of the CSV file.</param>
    /// <param name="platform">The platform from which the CSV file comes.</param>
    /// <param name="formatProvider">An object, which provides culture-specific formatting
    /// information, or <c>null</c> to automatically choose the most suitable <see cref="CultureInfo"
    /// />.</param>
    /// <param name="textEncoding">The text encoding to use or <c>null</c> for UTF-8
    /// with BOM (<see cref="Encoding.UTF8" />).</param>
    /// <returns>The contents of the CSV file as a generic <see cref="List{T}">List</see>
    /// of <see cref="Contact" /> objects.</returns>
    /// <remarks>The method executes <see cref="Contact.Clean" /> on each returned <see
    /// cref="Contact" /> object. Therefore it is usually not necessary to call <see
    /// cref="Contact.Clean" /> in your own code.</remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para>
    /// <paramref name="fileName" /> is not a valid file path.
    /// </para>
    /// <para>
    /// - or -
    /// </para>
    /// <para>
    /// <paramref name="platform" /> has an undefined value.
    /// </para>
    /// </exception>
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
    public static List<Contact> LoadCsv
        (string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        => CsvReader.GetInstance(platform, formatProvider, textEncoding).Read(fileName);


    /// <summary>Saves the content of a collection of <see cref="Contact" /> objects
    /// as CSV file.</summary>
    /// <param name="fileName">The file path of the CSV file to be created. If the file
    /// already exists, it will be overwritten.</param>
    /// <param name="contacts">
    /// <para>
    /// The collection of <see cref="Contact" /> objects to be saved.
    /// </para>
    /// <para>
    /// The collection can be empty or contain <c>null</c> values. If the collection
    /// does not contain an <see cref="Contact" /> object that contains data, an empty
    /// file is created.
    /// </para>
    /// </param>
    /// <param name="platform">The platform that the CSV file targets.</param>
    /// <param name="formatProvider">An object, that provides culture-specific formatting
    /// information, or <c>null</c> to automatically choose the most suitable <see cref="CultureInfo"
    /// />.</param>
    /// <param name="textEncoding">The text encoding to use, or <c>null</c> for UTF-8
    /// with BOM (<see cref="Encoding.UTF8" />).</param>
    /// <remarks>
    /// <para>
    /// The method calls <see cref="Contact.Clean" /> on all <see cref="Contact" />
    /// objects that are passed as an argument. All <see cref="Contact" /> objects whose
    /// property <see cref="Contact.IsEmpty" /> then returns <c>true</c> are not written
    /// to the file.
    /// </para>
    /// <para>
    /// If it is undesirable that the method can change the <see cref="Contact" /> objects
    /// by calling <see cref="Contact.Clean" />, make copies of the <see cref="Contact"
    /// /> objects beforehand with <see cref="Contact.Clone" /> and then transfer the
    /// copies to the method.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="fileName" /> or <paramref
    /// name="contacts" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para>
    /// <paramref name="fileName" /> is not a valid file path.
    /// </para>
    /// <para>
    /// - or -
    /// </para>
    /// <para>
    /// <paramref name="platform" /> has an undefined value.
    /// </para>
    /// </exception>
    /// <exception cref="IOException">I/O error.</exception>
    public static void SaveCsv
        (string fileName, IEnumerable<Contact?> contacts, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
        => CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, contacts);

    /// <summary>Loads the content of a VCF file (vCard) as a generic <see cref="List{T}">List</see>
    /// of <see cref="Contact" /> objects. (A VCF file can contain multiple vCards attached
    /// to each other.)</summary>
    /// <param name="fileName">The file path of the VCF file.</param>
    /// <returns>The loaded data as <see cref="List{T}" /> of <see cref="Contact" />
    /// objects. If the file contains no text, an empty list is returned.</returns>
    /// <remarks>The method executes <see cref="Contact.Clean" /> on each returned <see
    /// cref="Contact" /> object. Therefore it is usually not necessary to call <see
    /// cref="Contact.Clean" /> in your own code.</remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException"> <paramref name="fileName" /> is not a valid
    /// file path.</exception>
    /// <exception cref="IOException">The file could not be loaded.</exception>
    public static List<Contact> LoadVcf(string fileName) => VcfReader.Read(fileName);

    /// <summary>Saves the contents of a collection of <see cref="Contact" /> objects
    /// as a single VCF file.</summary>
    /// <param name="fileName">The file path of the vCard file (*.vcf) to be created.
    /// If the file already exists, it will be overwritten.</param>
    /// <param name="contacts">
    /// <para>
    /// The collection of <see cref="Contact" /> objects to be saved.
    /// </para>
    /// <para>
    /// The collection may be empty or may contain <c>null</c> values. If the collection
    /// does not contain any <see cref="Contact" /> object that contains data, no file
    /// is created.
    /// </para>
    /// </param>
    /// <param name="version">File version of the vCard file (*.vcf) to be saved.</param>
    /// <remarks>
    /// <para>
    /// The method calls <see cref="Contact.Clean" /> on all <see cref="Contact" />
    /// objects that are passed as an argument. All <see cref="Contact" /> objects whose
    /// property <see cref="Contact.IsEmpty" /> then returns <c>true</c> are not written
    /// to the file.
    /// </para>
    /// <para>
    /// If it is undesirable that the method can change the <see cref="Contact" /> objects
    /// by calling <see cref="Contact.Clean" />, make copies of the <see cref="Contact"
    /// /> objects beforehand with <see cref="Contact.Clone" /> and then transfer the
    /// copies to the method.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="contacts" /> or <paramref
    /// name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// <para>
    /// <paramref name="fileName" /> is not a valid file path.
    /// </para>
    /// <para>
    /// - or -
    /// </para>
    /// <para>
    /// <paramref name="version" /> has an undefined value.
    /// </para>
    /// </exception>
    /// <exception cref="IOException">The file could not be written.</exception>
    public static void SaveVcf(string fileName, IEnumerable<Contact?> contacts, VCardVersion version = VCardVersion.V3_0)
        => VcfWriter.Write(contacts, fileName, version);
}
