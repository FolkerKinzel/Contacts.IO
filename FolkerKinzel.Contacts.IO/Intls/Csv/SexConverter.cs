﻿using System;
using System.Collections.Generic;
using System.Text;
using FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv
{
    /// <summary>
    /// Can convert <see cref="Sex"/> to something better ;) !
    /// </summary>
    internal class SexConverter : ICsvTypeConverter
    {
        

        public object? FallbackValue => Sex.Unspecified;

        public Type Type => typeof(Sex);

        public bool ThrowsOnParseErrors => false;

       

        public virtual string? ConvertToString(object? value) => value switch
            {
            null => null,
            Sex.Female => "female",
            Sex.Male => "male",
            Sex.Unspecified => null,
            _ => throw new InvalidCastException()
            };
        

        public object? Parse(string? value)
        {
            if(value is null || value.Length == 0)
            {
                return FallbackValue;
            }

            char c = Char.ToUpperInvariant(value[0]);
            if (c == 'M' || c == '2')
            {
                return Sex.Male;
            }

            if (c == 'F' || c == 'W' || c == '1')
            {
                return Sex.Female;
            }

            return FallbackValue;
        }
    }
}