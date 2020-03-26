using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Tests
{
    [TestClass()]
    public class ContactPersistenceTests
    {
#pragma warning disable CS8618 // Das Non-Nullable-Feld ist nicht initialisiert. Deklarieren Sie das Feld ggf. als "Nullable".
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618 // Das Non-Nullable-Feld ist nicht initialisiert. Deklarieren Sie das Feld ggf. als "Nullable".

        [TestMethod()]
        public void ReadVcardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteVcardTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteVcardTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteCsvTest()
        {
            string fileName = Path.Combine(TestContext.TestRunResultsDirectory, "Test.csv");

            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            var cont = new Contact()
            {
                DisplayName = "Folkers Contact"
            };

            ContactPersistence.WriteCsv(fileName, new Contact[] { cont });
        }
    }
}