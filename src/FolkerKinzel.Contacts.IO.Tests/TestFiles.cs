using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FolkerKinzel.Contacts.IO.Tests
{
    internal static class TestFiles
    {
        private const string TEST_FILE_DIRECTORY_NAME = "TestFiles";
        private static readonly string _testFileDirectory;

        static TestFiles()
        {
            ProjectDirectory = Properties.Resources.ProjDir.Trim();
            _testFileDirectory = Path.Combine(ProjectDirectory, TEST_FILE_DIRECTORY_NAME);
        }


        internal static string[] GetAll()
        {
            return Directory.GetFiles(_testFileDirectory);
        }


        internal static string ProjectDirectory { get; }

        internal static string GoogleCsv => Path.Combine(_testFileDirectory, "Google.csv");

        internal static string GoogleCreatedOutlookCsv => Path.Combine(_testFileDirectory, "GoogleCreatedOutlook.csv");

        internal static string Outlook365Csv => Path.Combine(_testFileDirectory, "Outlook365.csv");

        internal static string Outlook365DifferentColumnNamesCsv => Path.Combine(_testFileDirectory, "Outlook365_DifferentColumnNames.csv");

        internal static string Outlook365DifferentColumnNamesLessColumnsCsv => Path.Combine(_testFileDirectory, "Outlook365_DifferentColumnNamesLessColumns.csv");

        internal static string NoHeaderCsv => Path.Combine(_testFileDirectory, "NoHeader.csv");


        internal static string WindowsLiveMail => Path.Combine(_testFileDirectory, "WindowsLiveMail.csv");


        internal static string ThunderbirdAnsiCsv => Path.Combine(_testFileDirectory, "ThunderbirdAnsi.csv");

        internal static string ThunderbirdUtf8Csv => Path.Combine(_testFileDirectory, "ThunderbirdUtf8.csv");

        internal static string ThunderbirdUtf8UnknownAndLessColumnNamesCsv => Path.Combine(_testFileDirectory, "ThunderbirdUtf8UnknownAndLessColumnNames.csv");

        internal static string ThunderbirdUtf8EnglishColumnsInvalidBDayCsv => Path.Combine(_testFileDirectory, "ThunderbirdUtf8EnglishColumnsInvalidBDay.csv");


        internal static string V2vcf => Path.Combine(_testFileDirectory, "v2_1.vcf");

        internal static string V3vcf => Path.Combine(_testFileDirectory, "v3.vcf");

        internal static string V4vcf => Path.Combine(_testFileDirectory, "v4.vcf");
    }
}
