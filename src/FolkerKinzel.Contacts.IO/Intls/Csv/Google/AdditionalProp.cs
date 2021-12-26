using FolkerKinzel.CsvTools.Helpers;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google;

internal enum AdditionalProp
{
    Phone1Type = 1000,
    Phone1Value,

    Phone2Type,
    Phone2Value,

    Phone3Type,
    Phone3Value,

    Phone4Type,
    Phone4Value,

    Phone5Type,
    Phone5Value,

    Phone6Type,
    Phone6Value,

    Phone7Type,
    Phone7Value,

    Phone8Type,
    Phone8Value,

    Phone9Type,
    Phone9Value,

    AddressHomeType,

    AddressWorkType,
    //AddressWorkStreet,
    //AddressWorkCity,
    //AddressWorkState,
    //AddressWorkPostalCode,
    //AddressWorkCountry,

    InstantMessenger1Type,
    InstantMessenger1Service,

    InstantMessenger2Type,
    InstantMessenger2Service,

    RelationType,

    WebHomeType,
    //WebHomeValue,

    WebWorkType,
    //WebWorkValue,

    EventType,

    /// <summary>
    /// Dummy-Property, die am Ende von <see cref="CsvRecordWrapper"/> eingefügt wird, um beim Lesen von CSV am
    /// Ende der Initialisierung von <see cref="Contact"/> AddressHome und AddressWork ggf. zu vertauschen.
    /// </summary>
    Swap

}
