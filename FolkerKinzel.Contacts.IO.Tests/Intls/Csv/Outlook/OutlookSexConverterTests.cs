using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook.Tests
{
    [TestClass]
    public class OutlookSexConverterTests
    {
        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow(Sex.Unspecified, null)]
        [DataRow(Sex.Female, "1")]
        [DataRow(Sex.Male, "2")]
        public void ConvertToStringTest1(Sex? input, string? expected)
        {
            var conv = new OutlookSexConverter();

            string? result = conv.ConvertToString(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void ConvertToStringTest2()
        {
            var conv = new OutlookSexConverter();
            _ = conv.ConvertToString("Hi!");
        }
    }

    
}
