using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace FolkerKinzel.Contacts.IO.Tests
{
    [TestClass()]
    public class ContactCollectionExtensionTests
    {
        [NotNull]
        public TestContext? TestContext { get; set; }


        [TestMethod()]
        public void SaveCsvTest()
        {
            List<Contact> conts = ContactPersistence.LoadCsv(TestFiles.GoogleCsv, CsvCompatibility.Google);

            conts.Insert(0, null!);

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "Google_CollectionExtension1.csv");

            conts.SaveCsv(fileName, CsvCompatibility.Google);
        }


        [TestMethod()]
        public void SaveVCardTest()
        {
            List<Contact> conts = ContactPersistence.LoadVcf(TestFiles.V3vcf);

            conts.Insert(0, null!);

            string fileName = Path.Combine(TestContext.TestRunResultsDirectory!, "CollectionExtension1.vcf");


            conts.SaveVcf(fileName);
        }
    }
}