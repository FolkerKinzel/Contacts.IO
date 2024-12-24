using System.ComponentModel;
using System.Globalization;
using System.Text;
using FolkerKinzel.Contacts.IO.Intls;
using FolkerKinzel.Contacts.IO.Intls.Csv;
using FolkerKinzel.Contacts.IO.Intls.Vcf;

namespace FolkerKinzel.Contacts.IO;

/// <summary>Extension methods for the <see cref="Contact" /> class.</summary>
public static class ContactExtension
{
    /// <summary>Saves the content of a <see cref="Contact" /> object as CSV file.</summary>
    /// <remarks>
    /// <para>
    /// The method creates a new CSV file with every call. (If you have to save several
    /// <see cref="Contact" /> objects in a common CSV file, <see cref="ContactPersistence.SaveCsv(string,
    /// IEnumerable{Contact?}, CsvCompatibility, IFormatProvider?, Encoding?)" /> or
    /// the extension method <see cref="ContactCollectionExtension.SaveCsv(IEnumerable{Contact?},
    /// string, CsvCompatibility, IFormatProvider?, Encoding?)" /> is suitable for your
    /// purposes.)
    /// </para>
    /// <para>
    /// The method calls <see cref="Contact.Clean" /> on <paramref name="contact" />.
    /// If the property <see cref="Contact.IsEmpty" /> of <paramref name="contact" />
    /// then returns <c>true</c>, an empty file is created. If it is undesirable for
    /// the method to change <paramref name="contact" /> by calling <see cref="Contact.Clean"
    /// />, you can create beforehand a copy of <paramref name="contact" /> with <see
    /// cref="Contact.Clone" /> and transfer then the copy as argument.
    /// </para>
    /// </remarks>
    /// <param name="contact">The <see cref="Contact" /> object to save.</param>
    /// <param name="fileName">The file path of the CSV file to be created. If the file
    /// already exists, it will be overwritten.</param>
    /// <param name="platform">The platform that the CSV file targets.</param>
    /// <param name="formatProvider">An object, which provides culture-specific formatting
    /// information, or <c>null</c> to automatically choose the most suitable <see cref="CultureInfo"
    /// />.</param>
    /// <param name="textEncoding">The text encoding to use, or <c>null</c> for UTF-8
    /// with BOM (<see cref="Encoding.UTF8" />).</param>
    /// <exception cref="ArgumentNullException"> <paramref name="contact" /> or <paramref
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
    public static void SaveCsv(this Contact contact, string fileName, CsvCompatibility platform, IFormatProvider? formatProvider = null, Encoding? textEncoding = null)
    {
        _ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        CsvWriter.GetInstance(platform, formatProvider, textEncoding).Write(fileName, [contact]);
    }

    /// <summary>Saves the content of a <see cref="Contact" /> object as a vCard file
    /// (*.vcf).</summary>
    /// <param name="contact">The <see cref="Contact" /> object to be saved. If <paramref
    /// name="contact" /> is <c>null</c> or contains no data, no file is created.</param>
    /// <param name="fileName">The file path of the vCard file (*.vcf) to be created.
    /// If the file already exists, it will be overwritten.</param>
    /// <param name="version">File version of the vCard file (*.vcf) to be saved.</param>
    /// <remarks>
    /// <para>
    /// The method calls <see cref="Contact.Clean" /> on <paramref name="contact" />.
    /// If the property <see cref="Contact.IsEmpty" /> of <paramref name="contact" />
    /// then returns <c>true</c>, no file is created. If it is undesirable for the method
    /// to change <paramref name="contact" /> by calling <see cref="Contact.Clean" />,
    /// you can create beforehand a copy of <paramref name="contact" /> with <see cref="Contact.Clone"
    /// /> and transfer the copy as argument.
    /// </para>
    /// <para>
    /// For storing several <see cref="Contact" /> objects in a single VCF file the
    /// method <see cref="ContactPersistence.SaveVcf(string, IEnumerable{Contact?},
    /// VCardVersion)" /> and the extension method are suitable <see cref="ContactCollectionExtension.SaveVcf(IEnumerable{Contact?},
    /// string, VCardVersion)" />.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException"> <paramref name="contact" /> or <paramref
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
    public static void SaveVcf(this Contact contact, string fileName, VCardVersion version = VCardVersion.V3_0)
        => VcfWriter.Write(contact, fileName, version);
}
