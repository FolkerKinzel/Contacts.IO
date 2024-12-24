namespace FolkerKinzel.Contacts.IO;

/// <summary>Named constants to specify the target platform of a CSV file.</summary>
public enum CsvCompatibility
{
    /// <summary>Unspecified</summary>
    Unspecified,

    /// <summary>Microsoft Outlook</summary>
    Outlook,

    /// <summary>Google Contacts</summary>
    Google,

    /// <summary>Mozilla Thunderbird</summary>
    Thunderbird
}
