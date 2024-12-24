namespace FolkerKinzel.Contacts.IO.Intls.Csv.Outlook;

internal class OutlookSexConverter : SexConverter
{
    public override string? ConvertToString(object? value) => value switch
    {
        null => null,
        Sex.Female => "1",
        Sex.Male => "2",
        Sex.Unspecified => null,
        _ => throw new InvalidCastException()
    };
}
