﻿using FolkerKinzel.Contacts.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Tests
{

    [TestClass()]
    public class ContactPersistenceTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }



        [TestMethod()]
        public void LoadCsvTest_Thunderbird1()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.ThunderbirdUtf8Csv, CsvCompatibility.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);

        }

        [TestMethod()]
        public void LoadCsvTest_Thunderbird2()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.ThunderbirdUtf8UnknownAndLessColumnNamesCsv, CsvCompatibility.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
        }

        [TestMethod()]
        public void LoadCsvTest_Thunderbird3()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.ThunderbirdUtf8EnglishColumnsInvalidBDayCsv, CsvCompatibility.Thunderbird);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
        }

        [TestMethod()]
        public void SaveCsvTest_Thunderbird()
        {

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Thunderbird.csv");


            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Thunderbird);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadCsvTest1()
        {
            _ = ContactPersistence.LoadCsv(TestFiles.Outlook365Csv, (CsvCompatibility)4711, CultureInfo.InvariantCulture);
        }


        [TestMethod]
        public void LoadCsvTest2()
        {
            List<Contact> res = ContactPersistence.LoadCsv(TestFiles.NoHeaderCsv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            Assert.AreEqual(0, res.Count);
        }


        [TestMethod()]
        public void LoadCsvTest_Outlook()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.Outlook365Csv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            Assert.IsNotNull(conts);
            Assert.AreEqual(6, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
            Assert.IsNotNull(conts[0].Person);
            Assert.AreEqual(new DateTime(1981, 1, 20), conts[0]?.Person?.BirthDay);
            Assert.AreEqual(new DateTime(2005, 6, 3), conts[0]?.Person?.Anniversary);
        }

        [TestMethod()]
        public void LoadCsvTest_Outlook_DifferentColumnNames()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.Outlook365DifferentColumnNamesCsv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            Assert.IsNotNull(conts);
            Assert.AreEqual(6, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
            Assert.IsNotNull(conts[0].Person);
            Assert.AreEqual(new DateTime(1981, 1, 20), conts[0]?.Person?.BirthDay);
            Assert.AreEqual(new DateTime(2005, 6, 3), conts[0]?.Person?.Anniversary);
        }


        [TestMethod()]
        public void LoadCsvTest_Outlook_DifferentColumnNamesLessColumns()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.Outlook365DifferentColumnNamesLessColumnsCsv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            Assert.IsNotNull(conts);
            Assert.AreEqual(6, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);

            Assert.IsNotNull(conts[5].Person);
            Assert.AreEqual("Mustermann", conts[5].Person?.Name?.LastName);
        }

        [TestMethod()]
        public void SaveCsvTest_Outlook1()
        {

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Outlook1.csv");

            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Outlook);
        }

        [TestMethod]
        public void SaveTest_Outlook2()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Outlook2.csv");

            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.Outlook365Csv, CsvCompatibility.Outlook, CultureInfo.InvariantCulture);

            conts.SaveCsv(fileName, CsvCompatibility.Outlook);
        }


        [TestMethod()]
        public void LoadCsvTest_Google()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.GoogleCsv, CsvCompatibility.Google);

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);

        }

        [TestMethod()]
        public void SaveCsvTest_Google1()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Google.csv");
            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Google);
        }

        [TestMethod()]
        public void SaveCsvTest_Google2()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Google.csv");
            Contact cont = Utility.InitTestContact();

            cont.AddressHome = null;
            cont.WebPagePersonal = null;

            cont.SaveCsv(fileName, CsvCompatibility.Google);
        }

        [TestMethod()]
        public void LoadCsvTest_Unspecified()
        {
            var sw = new Stopwatch();
            sw.Start();
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.WindowsLiveMail, CsvCompatibility.Unspecified);
            sw.Stop();

            TestContext.WriteLine($"Stopwatch: {sw.ElapsedMilliseconds}");

            Assert.IsNotNull(conts);
            Assert.AreEqual(1, conts.Count);
            CollectionAssert.DoesNotContain(conts, null);
        }

        [TestMethod]
        public void SaveCsvTest_Unspecified()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Unspecified.csv");
            Utility.InitTestContact().SaveCsv(fileName, CsvCompatibility.Unspecified);
        }


        [TestMethod()]
        public void LoadVCardTest1()
        {
            List<Contact> conts = ContactPersistence.LoadVcf(TestFiles.V3vcf);

            Assert.IsNotNull(conts);
            Assert.AreEqual(2, conts.Count);
        }

        [TestMethod()]
        public void LoadVCardTest2()
        {
            List<Contact> conts = ContactPersistence.LoadVcf(TestFiles.V4vcf);

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
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "3_0.vcf");
            Utility.InitTestContact().SaveVcf(fileName);
        }


    }
}