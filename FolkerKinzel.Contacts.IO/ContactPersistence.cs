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
    /// Statische Klasse zum Importieren und Exportieren von Kontaktdaten.
    /// </summary>
    /// <example>
    /// <note type="important">Die Beispiele enthalten keine Ausnahmebehandlung. Kopieren Sie sie deshalb nicht in Code, den Sie veröffentlichen!</note>
    /// <para>Initialisieren von <see cref="Contact"/>-Objekten:</para>
    /// <code language="cs">
    /// using FolkerKinzel.Contacts;
    /// using System;
    /// 
    /// namespace Examples
    /// {
    ///     static class ContactExample
    ///     {
    ///         public static Contact[] InitializeContacts() => new Contact[]
    ///             {
    ///                 new Contact
    ///                 {
    ///                     DisplayName = "John Doe",
    ///                     Person = new Person
    ///                     {
    ///                         Name = new Name
    ///                         {
    ///                             FirstName = "John",
    ///                             MiddleName = "William",
    ///                             LastName = "Doe",
    ///                             Suffix = "jr."
    ///                         },
    /// 
    ///                         BirthDay = new DateTime(1972, 1, 3),
    ///                         Spouse = "Jane Doe",
    ///                         Anniversary = new DateTime(2001, 6, 15)
    ///                     },
    /// 
    ///                     Work = new Work
    ///                     {
    ///                         JobTitle = "Facility Manager",
    ///                         Company = "Contoso"
    ///                     },
    /// 
    ///                     PhoneNumbers = new PhoneNumber[]
    ///                     {
    ///                         new PhoneNumber
    ///                         {
    ///                             Value = "0123-45678",
    ///                             IsWork = true
    ///                         }
    ///                     },
    /// 
    ///                     EmailAddresses = new string[]
    ///                     {
    ///                         "john.doe@contoso.com"
    ///                     }
    ///                 },//new Contact()
    /// 
    ///                 ///////////
    /// 
    ///                 new Contact
    ///                 {
    ///                     DisplayName = "Jane Doe",
    ///                     Person = new Person
    ///                     {
    ///                         Name = new Name
    ///                         {
    ///                             FirstName = "Jane",
    ///                             LastName = "Doe",
    ///                             Prefix = "Dr."
    ///                         },
    ///                         BirthDay = new DateTime(1981, 5, 4),
    ///                         Spouse = "John Doe",
    ///                         Anniversary = new DateTime(2001, 6, 15)
    ///                     },
    /// 
    ///                     Work = new Work
    ///                     {
    ///                         JobTitle = "CEO",
    ///                         Company = "Contoso"
    ///                     },
    /// 
    ///                     PhoneNumbers = new PhoneNumber[]
    ///                     {
    ///                         new PhoneNumber
    ///                         {
    ///                             Value = "876-54321",
    ///                             IsMobile = true
    ///                         }
    ///                     }
    ///                 }//new Contact()
    ///             };//new Contact[]
    ///     }
    /// }
    /// 
    /// </code>
    /// <para>
    /// Lesen und Schreiben von vCards (*.vcf):
    /// </para>
    /// <code language="cs">
    /// using FolkerKinzel.Contacts;
    /// using FolkerKinzel.Contacts.IO;
    /// using System;
    /// using System.Collections.Generic;
    /// using System.IO;
    /// 
    /// namespace Examples
    /// {
    ///     static class VCardExample
    ///     {
    ///         public static void ReadingAndWritingVCard()
    ///         {
    ///             // Initialize data (see code-example above):
    ///             Contact[] contactArr = ContactExample.InitializeContacts();
    /// 
    ///             const string fileName = "FamilyDoe.vcf";
    /// 
    ///             // Save both contacts to a single vcf-file:
    ///             ContactPersistence.SaveVCard(fileName, contactArr);
    /// 
    ///             // Show the content of the vcf-File:
    ///             Console.WriteLine("Saved VCF:");
    ///             Console.WriteLine();
    ///             Console.WriteLine(File.ReadAllText(fileName));
    /// 
    ///             // Load the contacts:
    ///             List&lt;Contact&gt; contactList = ContactPersistence.LoadVCard(fileName);
    /// 
    ///             // Show the content of the loaded Contact-objects:
    ///             Console.WriteLine();
    ///             Console.WriteLine("Loaded Contact-objects:");
    /// 
    ///             for (int i = 0; i &lt; contactList.Count; i++)
    ///             {
    ///                 Console.WriteLine();
    ///                 Console.WriteLine($"Contact {i + 1}:");
    ///                 Console.WriteLine(contactArr[i]);
    ///                 Console.WriteLine();
    ///             }
    /// 
    ///         }
    ///     }
    /// }
    /// 
    /// /*
    /// Console Output:
    /// 
    /// Saved VCF:
    /// 
    /// BEGIN:VCARD
    /// VERSION:3.0
    /// FN:John Doe
    /// N:Doe;John;William;;jr.
    /// TITLE:Facility Manager
    /// ORG:Contoso
    /// BDAY;VALUE=DATE:1972-01-03
    /// X-ANNIVERSARY:2001-06-15
    /// TEL;TYPE=WORK:0123-45678
    /// EMAIL;TYPE=INTERNET,PREF:john.doe@contoso.com
    /// X-SPOUSE:Jane Doe
    /// END:VCARD
    /// BEGIN:VCARD
    /// VERSION:3.0
    /// FN:Jane Doe
    /// N:Doe;Jane;;Dr.;
    /// TITLE:CEO
    /// ORG:Contoso
    /// BDAY;VALUE=DATE:1981-05-04
    /// X-ANNIVERSARY:2001-06-15
    /// TEL;TYPE=CELL:876-54321
    /// X-SPOUSE:John Doe
    /// END:VCARD
    /// 
    /// 
    /// Loaded Contact-objects:
    /// 
    /// Contact 1:
    /// Display Name:
    ///         John Doe
    /// 
    /// Personal Data:
    ///         Name:        John William Doe jr.
    ///         Birthday:    01/03/1972
    ///         Spouse:      Jane Doe
    ///         Anniversary: 06/15/2001
    /// 
    /// E-Mails:
    ///         john.doe@contoso.com
    /// 
    /// Phone Numbers:
    ///         0123-45678 (w.)
    /// 
    /// Company Data:
    ///         Company:  Contoso
    ///         Position: Facility Manager
    /// 
    /// 
    /// Contact 2:
    /// Display Name:
    ///         Jane Doe
    /// 
    /// Personal Data:
    ///         Name:        Dr. Jane Doe
    ///         Birthday:    05/04/1981
    ///         Spouse:      John Doe
    ///         Anniversary: 06/15/2001
    /// 
    /// Phone Numbers:
    ///         876-54321
    /// 
    /// Company Data:
    ///         Company:  Contoso
    ///         Position: CEO
    /// 
    /// */
    /// </code>
    /// <para>
    /// Lesen und Schreiben von CSV-Dateien:
    /// </para>
    /// <code language="cs">
    /// using FolkerKinzel.Contacts;
    /// using FolkerKinzel.Contacts.IO;
    /// using System;
    /// using System.Collections.Generic;
    /// using System.IO;
    /// 
    /// namespace Examples
    /// {
    ///     static class CsvExample
    ///     {
    ///         public static void ReadingAndWritingCsv()
    ///         {
    ///             // Initialize data (see code-example above):
    ///             Contact[] contactArr = ContactExample.InitializeContacts();
    /// 
    ///             const string fileName = "FamilyDoe.csv";
    /// 
    ///             // Save the contacts:
    ///             ContactPersistence.SaveCsv(fileName, contactArr, CsvCompatibility.Thunderbird);
    /// 
    ///             // Show the content of the csv-File:
    ///             Console.WriteLine("Saved CSV:");
    ///             Console.WriteLine();
    ///             Console.WriteLine(File.ReadAllText(fileName));
    /// 
    ///             // Load the contacts:
    ///             List&lt;Contact&gt; contactList = ContactPersistence.LoadCsv(fileName, CsvCompatibility.Thunderbird);
    /// 
    ///             // Show the content of the loaded Contact-objects:
    ///             Console.WriteLine();
    ///             Console.WriteLine("Loaded Contact-objects:");
    /// 
    ///             for (int i = 0; i &lt; contactList.Count; i++)
    ///             {
    ///                 Console.WriteLine();
    ///                 Console.WriteLine($"Contact {i + 1}:");
    ///                 Console.WriteLine(contactArr[i]);
    ///                 Console.WriteLine();
    ///             }
    ///         }
    ///     }
    /// }
    /// 
    /// /*
    /// Console Output:
    /// 
    /// Saved CSV:
    /// 
    /// First Name,Last Name,Display Name,Nickname,Primary Email,Secondary Email,Screen Name,Work Phone,Home Phone,Fax Number,
    /// Pager Number,Mobile Number,Home Address,Home Address2,Home City,Home State,Home Zipcode,Home Country,Work Address,
    /// Work Address2,Work City,Work State,Work Zip,Work Country,Job Title,Department,Organization,Web Page 1,Web Page 2,
    /// Birth Year,Birth Month,Birth Day,Custom 1,Custom 2,Custom 3,Custom 4,Notes
    /// John,Doe,John Doe,,john.doe@contoso.com,,,0123-45678,,,,,,,,,,,,,,,,,Facility Manager,,Contoso,,,1972,1,3,,,,,
    /// Jane,Doe,Jane Doe,,,,,,,,,876-54321,,,,,,,,,,,,,CEO,,Contoso,,,1981,5,4,,,,,
    /// 
    /// 
    /// Loaded Contact-objects:
    /// 
    /// Contact 1:
    /// Display Name:
    ///         John Doe
    /// 
    /// Personal Data:
    ///         Name:        John William Doe jr.
    ///         Birthday:    01/03/1972
    ///         Spouse:      Jane Doe
    ///         Anniversary: 06/15/2001
    /// 
    /// E-Mails:
    ///         john.doe@contoso.com
    /// 
    /// Phone Numbers:
    ///         0123-45678 (w.)
    /// 
    /// Company Data:
    ///         Company:  Contoso
    ///         Position: Facility Manager
    /// 
    /// 
    /// Contact 2:
    /// Display Name:
    ///         Jane Doe
    /// 
    /// Personal Data:
    ///         Name:        Dr. Jane Doe
    ///         Birthday:    05/04/1981
    ///         Spouse:      John Doe
    ///         Anniversary: 06/15/2001
    /// 
    /// Phone Numbers:
    ///         876-54321
    /// 
    /// Company Data:
    ///         Company:  Contoso
    ///         Position: CEO
    ///  */
    /// </code>
    /// </example>
    public static class ContactPersistence
    {
        /// <summary>
        /// Liest den Inhalt einer CSV-Datei als <see cref="List{T}"/> von <see cref="Contact"/>-Objekten.
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
        /// Schreibt den Inhalt einer Sammlung von <see cref="Contact"/>-Objekten in eine CSV-Datei.
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
        /// Liest eine vCard-Datei und gibt ihre Daten als <see cref="Contact"/>-Array zurück. (Eine vCard-Datei kann
        /// mehrere aneinandergehängte vCards enthalten.) Enthält die Datei keinen Text, wird ein leeres Array zurückgegeben.
        /// </summary>
        /// <param name="fileName">Der vollständige Pfad der vCard-Datei.</param>
        /// <returns>Die Daten der vCard als <see cref="Contact"/>-Array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
        public static List<Contact> LoadVCard(string fileName)
        {
            return VcfReader.Read(fileName);
        }





        /// <summary>
        /// Schreibt den Inhalt einer Auflistung  von <see cref="Contact"/>-Objekten in eine gemeinsame 
        /// vCard-Datei.
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
