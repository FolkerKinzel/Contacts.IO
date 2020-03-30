using FolkerKinzel.Contacts.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Tests
{

    [TestClass()]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Bezeichner dürfen keine Unterstriche enthalten", Justification = "<Ausstehend>")]
    public class ContactPersistenceTests
    {
#pragma warning disable CS8618 // Das Non-Nullable-Feld ist nicht initialisiert. Deklarieren Sie das Feld ggf. als "Nullable".
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618 // Das Non-Nullable-Feld ist nicht initialisiert. Deklarieren Sie das Feld ggf. als "Nullable".



        [TestMethod()]
        public void ReadCsvTest_Thunderbird()
        {
            var conts = ContactPersistence.ReadCsv(TestFiles.ThunderbirdUtf8Csv, CsvTarget.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);

        }

        [TestMethod()]
        public void WriteCsvTest_Thunderbird()
        {
            
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Thunderbird.csv");


            ContactPersistence.WriteCsv(fileName, new Contact[] { Utility.InitTestContact() }, CsvTarget.Thunderbird);
        }


        [TestMethod()]
        public void ReadCsvTest_Google()
        {
            var conts = ContactPersistence.ReadCsv(TestFiles.GoogleCsv, CsvTarget.Google);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);

        }

        [TestMethod()]
        public void WriteCsvTest_Google()
        {

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Google.csv");


            ContactPersistence.WriteCsv(fileName, new Contact[] { Utility.InitTestContact() }, CsvTarget.Google);
        }


        [TestMethod()]
        public void ReadVCardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteVCardTest()
        {
            Assert.Fail();
        }

        
    }
}