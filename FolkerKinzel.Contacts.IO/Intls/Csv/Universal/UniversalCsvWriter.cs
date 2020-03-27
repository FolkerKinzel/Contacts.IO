﻿using Csv = FolkerKinzel.CsvTools;
using FolkerKinzel.CsvTools.Helpers;
using Conv = FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using FolkerKinzel.Contacts.IO.Resources;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Universal
{
    internal static class HeaderRow
    {
        internal static string[] GetColumnNamesEn() => throw new NotImplementedException();

    }

    internal class UniversalCsvWriter : CsvWriter
    {
        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();
        

        protected override void InitMapping(List<Tuple<string, ContactProp?>> mapping, string[] columnNames)
        {
            throw new NotImplementedException();
        }
    }
}
