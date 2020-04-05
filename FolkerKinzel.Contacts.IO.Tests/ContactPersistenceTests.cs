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
        public void LoadCsvTest_Thunderbird()
        {
            var conts = ContactPersistence.LoadCsv(TestFiles.ThunderbirdUtf8Csv, CsvTarget.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);

        }

        [TestMethod()]
        public void SaveCsvTest_Thunderbird()
        {
            
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Thunderbird.csv");


            Utility.InitTestContact().SaveCsv(fileName, CsvTarget.Thunderbird);
        }


        [TestMethod()]
        public void LoadCsvTest_Google()
        {
            var conts = ContactPersistence.LoadCsv(TestFiles.GoogleCsv, CsvTarget.Google);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);


            //string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Maxl.vcf");

            //conts[0].SaveVCard(fileName);


        }

        [TestMethod()]
        public void SaveCsvTest_Google()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Google.csv");
            Utility.InitTestContact().SaveCsv(fileName, CsvTarget.Google);
        }



        [TestMethod()]
        public void LoadCsvTest_Unspecified()
        {
            var conts = ContactPersistence.LoadCsv(TestFiles.GoogleCsv, CsvTarget.Unspecified);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);

        }


        [TestMethod()]
        public void LoadVCardTest()
        {
            var conts = ContactPersistence.LoadVCard(TestFiles.V3vcf);

            Assert.IsNotNull(conts);
            Assert.AreEqual(2, conts.Count);
        }

        [TestMethod()]
        public void SaveVCardTest_3_0()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "3_0.vcf");
            Utility.InitTestContact().SaveVCard(fileName);
        }

        
    }
}