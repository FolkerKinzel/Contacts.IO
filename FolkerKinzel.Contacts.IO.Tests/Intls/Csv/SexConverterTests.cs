using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Tests
{
    [TestClass]
    public class SexConverterTests
    {
        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow(Sex.Unspecified, null)]
        [DataRow(Sex.Female, "Female")]
        [DataRow(Sex.Male, "Male")]
        public void ConvertToStringTest1(Sex? input, string? expected)
        {
            var conv = new SexConverter();

            string? result = conv.ConvertToString(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void ConvertToStringTest2()
        {
            var conv = new SexConverter();
            _ = conv.ConvertToString("Hi!");
        }

        [DataTestMethod]
        [DataRow(null, Sex.Unspecified)]
        [DataRow("", Sex.Unspecified)]
        [DataRow("bla", Sex.Unspecified)]
        [DataRow("1", Sex.Female)]
        [DataRow("Frau", Sex.Female)]
        [DataRow("female", Sex.Female)]
        [DataRow("weiblich", Sex.Female)]
        [DataRow("2", Sex.Male)]
        [DataRow("male", Sex.Male)]
        [DataRow("männlich", Sex.Male)]
        public void ParseTest1(string? input, Sex expected)
        {
            var conv = new SexConverter();

            var result = (Sex?)conv.Parse(input);

            Assert.IsNotNull(result);

            Assert.AreEqual(expected, result);
        }
    }
}
