namespace FolkerKinzel.Contacts.IO;

/// <summary>
/// Benannte Konstanten, um die VCF-Version anzugeben.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Bezeichner dürfen keine Unterstriche enthalten", Justification = "<Ausstehend>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enumerationen müssen einen Wert von null aufweisen.", Justification = "<Ausstehend>")]
public enum VCardVersion
{
    /// <summary>
    /// vCard 2.1
    /// </summary>
    V2_1 = 2,

    /// <summary>
    /// vCard 3.0
    /// </summary>
    V3_0 = 3,

    /// <summary>
    /// vCard 4.0
    /// </summary>
    V4_0 = 4
}
