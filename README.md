# FolkerKinzel.Contacts.IO
Simple .NET-API to manage contact data of organizations and natural persons, including a data model and classes to persisist this 
as vCard (*.vcf) or CSV.

```
nuget Package Manager:
PM> 

.NET CLI:
> 

PackageReference (Visual Studio Project File):


Paket CLI:
> 
```

* [Download Reference (English)](https://github.com/FolkerKinzel/Contacts.IO/blob/master/FolkerKinzel.Contacts.IO.Reference.en/Help/FolkerKinzel.Contacts.IO.en.chm)

* [Projektdokumentation (Deutsch) herunterladen](https://github.com/FolkerKinzel/Contacts.IO/blob/master/FolkerKinzel.Contacts.IO.Doku.de/Help/FolkerKinzel.Contacts.IO.de.chm)

> IMPORTANT: On some systems, the content of the CHM file is blocked. Before extracting it,
>  right click on the file, select Properties, and check the "Allow" checkbox - if it 
> is present - in the lower right corner of the General tab in the Properties dialog.


## Example Code
_(For better readability exception handling is ommitted in the following examples.)_

* [Initialize Contact Objects](#initialize-contact-objects)
* [Read and Write VCF Files](#read-and-write-vcf-files)
* [Read and Write CSV Files](#read-and-write-csv-files)

#### Initialize `Contact` Objects:
```csharp
using FolkerKinzel.Contacts;
using System;

namespace Examples
{
    static class ContactExample
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
                            Value = "0123-45678",
                            IsWork = true
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

                    PhoneNumbers = new PhoneNumber[]
                    {
                        new PhoneNumber
                        {
                            Value = "876-54321",
                            IsMobile = true
                        }
                    }
                }//new Contact()
            };//new Contact[]
    }
}
```

#### Read and Write VCF Files:
```csharp
using FolkerKinzel.Contacts;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Examples
{
    static class VCardExample
    {
        public static void ReadingAndWritingVCard()
        {
            // Initialize data (see code-example above):
            Contact[] contactArr = ContactExample.InitializeContacts();

            const string fileName = "FamilyDoe.vcf";

            // Save both contacts to a single vcf-file:
            ContactPersistence.SaveVCard(fileName, contactArr);

            // Show the content of the vcf-File:
            Console.WriteLine("Saved VCF:");
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(fileName));

            // Load the contacts:
            List<Contact> contactList = ContactPersistence.LoadVCard(fileName);

            // Show the content of the loaded Contact-objects:
            Console.WriteLine();
            Console.WriteLine("Loaded Contact-objects:");

            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Contact {i + 1}:");
                Console.WriteLine(contactArr[i]);
                Console.WriteLine();
            }

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
TEL;TYPE=WORK:0123-45678
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
TEL;TYPE=CELL:876-54321
X-SPOUSE:John Doe
END:VCARD


Loaded Contact-objects:

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
        0123-45678 (w.)

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
        876-54321

Company Data:
        Company:  Does Company
        Position: CEO
*/
```

#### Read and Write CSV Files:
```csharp
using FolkerKinzel.Contacts;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Examples
{
    static class CsvExample
    {
        public static void ReadingAndWritingCsv()
        {
            // Initialize data (see code-example above):
            Contact[] contactArr = ContactExample.InitializeContacts();

            const string fileName = "FamilyDoe.csv";

            // Save the contacts:
            ContactPersistence.SaveCsv(fileName, contactArr, CsvCompatibility.Thunderbird);

            // Show the content of the csv-File:
            Console.WriteLine("Saved CSV:");
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(fileName));

            // Load the contacts:
            List<Contact> contactList = ContactPersistence.LoadCsv(fileName, CsvCompatibility.Thunderbird);

            // Show the content of the loaded Contact-objects:
            Console.WriteLine();
            Console.WriteLine("Loaded Contact-objects:");

            for (int i = 0; i < contactList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Contact {i + 1}:");
                Console.WriteLine(contactArr[i]);
                Console.WriteLine();
            }
        }
    }
}

/*
Saved CSV:

First Name,Last Name,Display Name,Nickname,Primary Email,Secondary Email,Screen Name,Work Phone,
Home Phone,Fax Number,Pager Number,Mobile Number,Home Address,Home Address2,Home City,Home State,
Home Zipcode,Home Country,Work Address,Work Address2,Work City,Work State,Work Zip,Work Country,
Job Title,Department,Organization,Web Page 1,Web Page 2,Birth Year,Birth Month,Birth Day,Custom 1,
Custom 2,Custom 3,Custom 4,Notes
John,Doe,John Doe,,john.doe@internet.com,,,0123-45678,,,,,,,,,,,,,,,,,Facility Manager,,Does Company,,,1972,1,3,,,,,
Jane,Doe,Jane Doe,,,,,,,,,876-54321,,,,,,,,,,,,,CEO,,Does Company,,,1981,5,4,,,,,


Loaded Contact-objects:

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
        0123-45678 (w.)

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
        876-54321

Company Data:
        Company:  Does Company
        Position: CEO
 */
```
