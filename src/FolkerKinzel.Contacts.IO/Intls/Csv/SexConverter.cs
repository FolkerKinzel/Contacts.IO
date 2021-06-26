using System;
using System.Diagnostics.CodeAnalysis;
using FolkerKinzel.CsvTools.Helpers.Converters;

namespace FolkerKinzel.Contacts.IO.Intls.Csv
{
    /// <summary>
    /// Can convert <see cref="Sex"/> to something better ;) !
    /// </summary>
    internal class SexConverter : ICsvTypeConverter
    {
        [ExcludeFromCodeCoverage]
        public object? FallbackValue => Sex.Unspecified;

        [ExcludeFromCodeCoverage]
        public Type Type => typeof(Sex);

        [ExcludeFromCodeCoverage]
        public bool ThrowsOnParseErrors => false;

       

        public virtual string? ConvertToString(object? value) => value switch
            {
            null => null,
            Sex.Female => "Female",
            Sex.Male => "Male",
            Sex.Unspecified => null,
            _ => throw new InvalidCastException()
            };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:In bedingten Ausdruck konvertieren", Justification = "<Ausstehend>")]
        public object? Parse(string? value)
        {
            if(value is null || value.Length == 0)
            {
                return FallbackValue;
            }

            char c = char.ToUpperInvariant(value[0]);
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
