using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolkerKinzel.Contacts.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Tests
{
    [TestClass()]
    public class CsvMappingCollectionTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullValue()
        {
            var coll = new CsvMappingCollection
            {
                null!
            };
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestItem1Null()
        {
            var coll = new CsvMappingCollection
            {
                new Tuple<string, ContactProperty>(null!, ContactProperty.AddressHomeCity)
            };
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestItem1Whitespace()
        {
            var coll = new CsvMappingCollection
            {
                new Tuple<string, ContactProperty>("   ", ContactProperty.AddressHomeCity)
            };
        }

        [TestMethod()]
        public void AddTest()
        {
            var coll = new CsvMappingCollection
            {
                new Tuple<string, ContactProperty>("Spalte", ContactProperty.AddressHomeCity)
            };
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDuplicateContactProperty()
        {
            var coll = new CsvMappingCollection();

            coll.Add(new Tuple<string, ContactProperty>("Spalte1", ContactProperty.AddressHomeCity));
            coll.Add(new Tuple<string, ContactProperty>("Spalte2", ContactProperty.AddressHomeCity));
        }
    }
}