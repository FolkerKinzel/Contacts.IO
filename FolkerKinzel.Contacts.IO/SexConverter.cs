using System;
using System.Collections.Generic;
using System.Text;
using FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO
{
    internal class SexConverter : ICsvTypeConverter
    {
        internal SexConverter(CsvTarget target)
        {
            this.Target = target;
        }

        public object? FallbackValue => Sex.NotSpecified;

        public Type Type => typeof(Sex);

        public bool ThrowsOnParseErrors => false;

        public CsvTarget Target { get; }

        public string? ConvertToString(object? value) => value switch
            {
            null => null,
            Sex.Female => Target == CsvTarget.Outlook ? "1" : "female",
            Sex.Male => Target == CsvTarget.Outlook ? "2" : "male",
            Sex.NotSpecified => null,
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
