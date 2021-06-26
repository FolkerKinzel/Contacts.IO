namespace FolkerKinzel.Contacts.IO
{
    /// <summary>
    /// Benannte Konstanten, um die Zielplattform einer CSV-Datei anzugeben.
    /// </summary>
    public enum CsvCompatibility
    {
        /// <summary>
        /// Keine Angabe.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Microsoft Outlook
        /// </summary>
        Outlook,

        /// <summary>
        /// Google Contacts
        /// </summary>
        Google,

        /// <summary>
        /// Mozilla Thunderbird
        /// </summary>
        Thunderbird
    }
}
