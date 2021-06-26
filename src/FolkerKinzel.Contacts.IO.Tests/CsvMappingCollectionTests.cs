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
                new Tuple<string, ContactProp>(null!, ContactProp.AddressHomeCity)
            };
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestItem1Whitespace()
        {
            var coll = new CsvMappingCollection
            {
                new Tuple<string, ContactProp>("   ", ContactProp.AddressHomeCity)
            };
        }

        [TestMethod()]
        public void AddTest()
        {
            var coll = new CsvMappingCollection
            {
                new Tuple<string, ContactProp>("Spalte", ContactProp.AddressHomeCity)
            };
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDuplicateContactProperty()
        {
            var coll = new CsvMappingCollection();

            coll.Add(new Tuple<string, ContactProp>("Spalte1", ContactProp.AddressHomeCity));
            coll.Add(new Tuple<string, ContactProp>("Spalte2", ContactProp.AddressHomeCity));
        }
    }
}