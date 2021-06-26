using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkerKinzel.Contacts.IO.Tests
{
    [TestClass()]
    public class ContactExtensionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveCsvTest1()
        {
            Contact? cont = null;
            cont!.SaveCsv("file.csv", CsvCompatibility.Thunderbird);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveCsvTest2()
        {
            var cont = new Contact();
            cont!.SaveCsv(null!, CsvCompatibility.Thunderbird);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveVcfTest1()
        {
            Contact? cont = null;
            cont!.SaveVcf("file.vcf");
        }
        
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveVcfTest2()
        {
            var cont = new Contact();
            cont!.SaveVcf(null!);
        }

        
    }
}