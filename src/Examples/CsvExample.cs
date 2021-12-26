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
. */
