using System.Globalization;
using System.Text;

namespace FolkerKinzel.Contacts.IO;

/// <summary>Extension methods for <see cref="IEnumerable{T}">IEnumerable&lt;Contact?&gt;</see>.</summary>
public static class ContactCollectionExtension
{
    /// <summary>Saves the content of a collection of <see cref="Contact" /> objects
    /// as a CSV file.</summary>
    /// <param name="contacts">The collection of <see cref="Contact" /> objects to save.</param>
    /// <param name="fileName">The file path of the CSV file to be created. If the file
    /// already exists, it will be overwritten.</param>
    /// <param name="platform">The platform that the CSV file targets.</param>
    /// <param name="formatProvider">An object, which provides culture-specific formatting
    /// information, or <c>null</c> to automatically choose the most suitable <see cref="CultureInfo"
    /// />.</param>
    /// <param name="textEncoding">The text encoding to use, or <c>null</c> for UTF-8
    /// with BOM (<see cref="Encoding.UTF8" />).</param>
    /// <remarks>The method calls <see cref="Contact.Clean" /> on every <see cref="Contact"
    /// /> object in <paramref name="contacts" />.</remarks>
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
    /// <paramref name="platform" /> has an undefined value.
    /// </para>
    /// </exception>
    /// <exception cref="IOException">I/O error.</exception>
    public static void SaveCsv(this IEnumerable<Contact?> contacts,
                               string fileName,
                               CsvCompatibility platform,
                               IFormatProvider? formatProvider = null,
                               Encoding? textEncoding = null)
        => ContactPersistence.SaveCsv(fileName, contacts, platform, formatProvider, textEncoding);


    /// <summary>Saves the contents of a collection of <see cref="Contact" /> objects
    /// as a single VCF file.</summary>
    /// <param name="contacts">The collection of <see cref="Contact" /> objects to be
    /// saved. The collection may be empty or contain <c>null</c> values. If the collection
    /// does not contain an <see cref="Contact" /> object that contains data, no file
    /// is created.</param>
    /// <param name="fileName">The file path of the vCard file (*.vcf) to be created.
    /// If the file already exists, it will be overwritten.</param>
    /// <param name="version">File version of the vCard file (*.vcf) to be saved.</param>
    /// <remarks>The method calls <see cref="Contact.Clean" /> on every <see cref="Contact"
    /// /> object in <paramref name="contacts" />.</remarks>
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
    public static void SaveVcf(this IEnumerable<Contact?> contacts,
                                 string fileName,
                                 VCardVersion version = VCardVersion.V3_0)
        => ContactPersistence.SaveVcf(fileName, contacts, version);
}
