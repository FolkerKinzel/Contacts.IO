# FolkerKinzel.Contacts.IO
[![NuGet](https://img.shields.io/nuget/v/FolkerKinzel.Contacts.IO)](https://www.nuget.org/packages/FolkerKinzel.Contacts.IO/)
[![GitHub](https://img.shields.io/github/license/FolkerKinzel/Contacts.IO)](https://github.com/FolkerKinzel/Contacts.IO/blob/master/LICENSE)

Small and easy to use framework for .NET to manage contact data of organizations and natural persons, including a data model and classes to persist it as vCard (*.vcf) or CSV.

* [Download Reference (English)](https://github.com/FolkerKinzel/Contacts.IO/blob/master/ProjectReference/1.4.0/FolkerKinzel.Contacts.IO.en.chm)

* [Projektdokumentation (Deutsch) herunterladen](https://github.com/FolkerKinzel/Contacts.IO/blob/master/ProjectReference/1.4.0/FolkerKinzel.Contacts.IO.de.chm)

> IMPORTANT: On some systems the content of the CHM file is blocked. Before opening the file
> right click on the file icon, select Properties, and check the "Allow" checkbox - if it 
> is present - in the lower right corner of the General tab in the Properties dialog.


## Example Code
_(For the sake of better readability exception handling is ommitted in the following examples.)_

* [Example: Initializing Contact Objects](#initializing-contact-objects)
* [Example: Reading and Writing vCard Files (*.vcf)](#reading-and-writing-vcard-files-vcf)
* [Example: Reading and Writing CSV Files](#reading-and-writing-csv-files)

#### Initializing `Contact` Objects:
```csharp
using FolkerKinzel.Contacts;

namespace Examples;

public static class ContactExample
{
    public static Contact[] InitializeContacts() => new Contact[]
        {
                new Contact
                {
                    DisplayName = "John Doe",
                    Person = new Person
                    {
                        Name = new Name
                        {
                            FirstName = "John",
                            MiddleName = "William",
                            LastName = "Doe",
                            Suffix = "jr."
                        },

                        BirthDay = new DateTime(1972, 1, 3),
                        Spouse = "Jane Doe",
                        Anniversary = new DateTime(2001, 6, 15)
                    },

                    Work = new Work
                    {
                        JobTitle = "Facility Manager",
                        Company = "Does Company"
                    },

                    PhoneNumbers = new PhoneNumber[]
                    {
                        new PhoneNumber
                        {
                            Value = "876-54321",
                            IsMobile = true
                        },
                        new PhoneNumber
                        {
                            Value = "123-45678",
                            IsWork = true,
                        }
                    },

                    EmailAddresses = new string[]
                    {
                        "john.doe@internet.com"
                    }
                },//new Contact()

                ///////////

                new Contact
                {
                    DisplayName = "Jane Doe",
                    Person = new Person
                    {
                        Name = new Name
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Prefix = "Dr."
                        },
                        BirthDay = new DateTime(1981, 5, 4),
                        Spouse = "John Doe",
                        Anniversary = new DateTime(2001, 6, 15)
                    },

                    Work = new Work
                    {
                        JobTitle = "CEO",
                        Company = "Does Company"
                    },

                    // PhoneNumber implements IEnumerable<PhoneNumber>, so
                    // a single instance can be assigned directly:
                    PhoneNumbers = new PhoneNumber
                    {
                        Value = "123-45678",
                        IsWork = true,
                    }
                }//new Contact()
        };//new Contact[]
}
```

#### Reading and Writing vCard Files (*.vcf):
```csharp
using FolkerKinzel.Contacts;
using FolkerKinzel.Contacts.IO;

namespace Examples;

public static class VCardExample
{
    public static void ReadingAndWritingVCard()
    {
        // Initialize data (see code-example above):
        Contact[] contactArr = ContactExample.InitializeContacts();

        const string fileName = "FamilyDoe.vcf";

        // Save all contacts to a common VCF file:
        contactArr.SaveVcf(fileName);

        // Display the content of the VCF file:
        Console.WriteLine("Saved VCF:");
        Console.WriteLine();
        Console.WriteLine(File.ReadAllText(fileName));

        // Reload the VCF file:
        List<Contact> contactList = ContactPersistence.LoadVcf(fileName);

        // Display the content of the reloaded Contact objects:
        Console.WriteLine();
        Console.WriteLine("Reloaded Contact objects:");

        for (int i = 0; i < contactList.Count; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"Contact {i + 1}:");
            Console.WriteLine(contactArr[i]);
            Console.WriteLine();
        }
    }
}

/*
Console Output:

Saved VCF:

BEGIN:VCARD
VERSION:3.0
FN:John Doe
N:Doe;John;William;;jr.
TITLE:Facility Manager
ORG:Does Company
BDAY;VALUE=DATE:1972-01-03
X-ANNIVERSARY:2001-06-15
TEL;TYPE=CELL:876-54321
TEL;TYPE=WORK:123-45678
EMAIL;TYPE=INTERNET,PREF:john.doe@internet.com
X-SPOUSE:Jane Doe
END:VCARD
BEGIN:VCARD
VERSION:3.0
FN:Jane Doe
N:Doe;Jane;;Dr.;
TITLE:CEO
ORG:Does Company
BDAY;VALUE=DATE:1981-05-04
X-ANNIVERSARY:2001-06-15
TEL;TYPE=WORK:123-45678
X-SPOUSE:John Doe
END:VCARD


Reloaded Contact objects:

Contact 1:
Display Name:
        John Doe

Personal Data:
        Name:        John William Doe jr.
        Birthday:    01/03/1972
        Spouse:      Jane Doe
        Anniversary: 06/15/2001

E-Mails:
        john.doe@internet.com

Phone Numbers:
        876-54321
        123-45678 (w.)

Company Data:
        Company:  Does Company
        Position: Facility Manager


Contact 2:
Display Name:
        Jane Doe

Personal Data:
        Name:        Dr. Jane Doe
        Birthday:    05/04/1981
        Spouse:      John Doe
        Anniversary: 06/15/2001

Phone Numbers:
        123-45678 (w.)

Company Data:
        Company:  Does Company
        Position: CEO
*/
```

#### Reading and Writing CSV Files:
```csharp
using FolkerKinzel.Contacts;
using FolkerKinzel.Contacts.IO;

namespace Examples;

public static class CsvExample
{
    public static void ReadingAndWritingCsv()
    {
        // Initialize data (see code-example above):
        Contact[] contactArr = ContactExample.InitializeContacts();

        const string fileName = "FamilyDoe.csv";

        // Save the Contacts:
        contactArr.SaveCsv(fileName, CsvCompatibility.Thunderbird);

        // Display the content of the CSV file:
        Console.WriteLine("Saved CSV:");
        Console.WriteLine();
        Console.WriteLine(File.ReadAllText(fileName));

        // Reload the CSV file:
        List<Contact> contactList = ContactPersistence.LoadCsv(fileName, CsvCompatibility.Thunderbird);

        // Display the content of the reloaded Contact objects:
        Console.WriteLine();
        Console.WriteLine("Reloaded Contact objects:");

        for (int i = 0; i < contactList.Count; i++)
        {
            Console.WriteLine();
            Console.WriteLine($"Contact {i + 1}:");
            Console.WriteLine(contactArr[i]);
            Console.WriteLine();
        }
    }
}

/*
Saved CSV:

First Name,Last Name,Display Name,Nickname,Primary Email,Secondary Email,Screen Name,Work Phone,Home Phone,Fax Number,Pager Number,Mobile Number,Home Address,Home Address2,Home City,Home State,Home Zipcode,Home Country,Work Address,Work Address2,Work City,Work State,Work Zip,Work Country,Job Title,Department,Organization,Web Page 1,Web Page 2,Birth Year,Birth Month,Birth Day,Custom 1,Custom 2,Custom 3,Custom 4,Notes
John,Doe,John Doe,,john.doe@internet.com,,,123-45678,,,,876-54321,,,,,,,,,,,,,Facility Manager,,Does Company,,,1972,1,3,,,,,
Jane,Doe,Jane Doe,,,,,123-45678,,,,,,,,,,,,,,,,,CEO,,Does Company,,,1981,5,4,,,,,


Reloaded Contact objects:

Contact 1:
Display Name:
        John Doe

Personal Data:
        Name:        John William Doe jr.
        Birthday:    01/03/1972
        Spouse:      Jane Doe
        Anniversary: 06/15/2001

E-Mails:
        john.doe@internet.com

Phone Numbers:
        876-54321
        123-45678 (w.)

Company Data:
        Company:  Does Company
        Position: Facility Manager


Contact 2:
Display Name:
        Jane Doe

Personal Data:
        Name:        Dr. Jane Doe
        Birthday:    05/04/1981
        Spouse:      John Doe
        Anniversary: 06/15/2001

Phone Numbers:
        123-45678 (w.)

Company Data:
        Company:  Does Company
        Position: CEO
*/
```
.

- [Version History](https://github.com/FolkerKinzel/Contacts.IO/releases)
