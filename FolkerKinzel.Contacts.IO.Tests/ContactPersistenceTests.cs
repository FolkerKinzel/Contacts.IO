using FolkerKinzel.Contacts.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Globalization;
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
            System.Collections.Generic.List<Contact>? conts = ContactPersistence.LoadCsv(TestFiles.ThunderbirdUtf8Csv, CsvCompatibility.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);

        }

        [TestMethod()]
        public void SaveCsvTest_Thunderbird()
        {
            
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Thunderbird.csv");


            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Thunderbird);
        }


        [TestMethod()]
        public void LoadCsvTest_Outlook()
        {
            System.Collections.Generic.List<Contact>? conts = ContactPersistence.LoadCsv(TestFiles.Outlook365Csv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            Assert.IsNotNull(conts);
            Assert.AreEqual(5, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
            Assert.IsNotNull(conts[0].Person);
            Assert.AreEqual(new DateTime(1981, 1, 20), conts[0]?.Person?.BirthDay);
            Assert.AreEqual(new DateTime(2005, 6, 3), conts[0]?.Person?.Anniversary);
        }

        [TestMethod()]
        public void SaveCsvTest_Outlook()
        {

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Outlook.csv");

            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Outlook);
        }


        [TestMethod()]
        public void LoadCsvTest_Google()
        {
            System.Collections.Generic.List<Contact>? conts = ContactPersistence.LoadCsv(TestFiles.GoogleCsv, CsvCompatibility.Google);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);

            //string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Maxl.vcf");

            //conts[0].SaveVCard(fileName);


        }

        [TestMethod()]
        public void SaveCsvTest_Google()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Google.csv");
            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Google);
        }



        [TestMethod()]
        public void LoadCsvTest_Unspecified()
        {
            var sw = new Stopwatch();
            sw.Start();
            System.Collections.Generic.List<Contact>? conts = ContactPersistence.LoadCsv(TestFiles.WindowsLiveMail, CsvCompatibility.Unspecified);
            sw.Stop();

            TestContext.WriteLine($"Stopwatch: {sw.ElapsedMilliseconds}");

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);


        }


        [TestMethod()]
        public void LoadVCardTest()
        {
            System.Collections.Generic.List<Contact>? conts = ContactPersistence.LoadVCard(TestFiles.V3vcf);

            Assert.IsNotNull(conts);
            Assert.AreEqual(2, conts.Count);
        }

        //[TestMethod()]
        //public void LoadVCardTest2()
        //{
        //    var conts = ContactPersistence.LoadVCard(@"C:\Users\fkinz\Desktop\Test\Wittig\Wittig.vcf");

        //    Assert.IsNotNull(conts);
        //}

        //[TestMethod()]
        //public void LoadCsvTest_Google2()
        //{
        //    var conts = ContactPersistence.LoadCsv(@"C:\Users\fkinz\Desktop\Test\Wittig\Wittig-Google.csv", CsvCompatibility.Google);

        //    Assert.IsNotNull(conts);
        //}

        //[TestMethod()]
        //public void LoadCsvTest_Outlook2()
        //{
        //    var conts = ContactPersistence.LoadCsv(@"C:\Users\fkinz\Desktop\Test\Wittig\Wittig-Outlook.csv", CsvCompatibility.Outlook);

        //    Assert.IsNotNull(conts);

        //    var bday = conts[0].Person.BirthDay;

        //}



        [TestMethod()]
        public void SaveVCardTest_3_0()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "3_0.vcf");
            Utility.InitTestContact().SaveVCard(fileName);
        }

        
    }
}