using FolkerKinzel.Contacts;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Examples
{
    public static class VCardExample
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
