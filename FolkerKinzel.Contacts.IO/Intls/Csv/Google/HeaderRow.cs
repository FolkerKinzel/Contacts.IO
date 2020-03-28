using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{
    internal static class HeaderRow
    {
        private static class En
        {
            internal const string Name = "Name";
            internal const string GivenName = "Given Name";
            internal const string AdditionalName = "Additional Name";
            internal const string FamilyName = "Family Name";
            internal const string YomiName = "Yomi Name";
            internal const string GivenNameYomi = "Given Name Yomi";
            internal const string AdditionalNameYomi = "Additional Name Yomi";
            internal const string FamilyNameYomi = "Family Name Yomi";
            internal const string NamePrefix = "Name Prefix";
            internal const string NameSuffix = "Name Suffix";
            internal const string Initials = "Initials";
            internal const string Nickname = "Nickname";
            internal const string ShortName = "Short Name";
            internal const string MaidenName = "Maiden Name";

            internal const string Birthday = "Birthday";
            internal const string Gender = "Gender";

            internal const string Location = "Location";
            internal const string BillingInformation = "Billing Information";
            internal const string DirectoryServer = "Directory Server";
            internal const string Mileage = "Mileage";
            internal const string Occupation = "Occupation";
            internal const string Hobby = "Hobby";
            internal const string Sensitivity = "Sensitivity";
            internal const string Priority = "Priority";
            internal const string Subject = "Subject";
            internal const string Notes = "Notes";
            internal const string Language = "Language";
            internal const string Photo = "Photo";
            internal const string GroupMembership = "Group Membership";

            internal const string Email1Type = "E-mail 1 - Type";
            internal const string Email1Value = "E-mail 1 - Value";

            internal const string Email2Type = "E-mail 2 - Type";
            internal const string Email2Value = "E-mail 2 - Value";

            internal const string InstantMessengerType = "IM 1 - Type";
            internal const string InstantMessengerService = "IM 1 - Service";
            internal const string InstantMessengerValue = "IM 1 - Value";

            internal const string Phone1Type = "Phone 1 - Type";
            internal const string Phone1Value = "Phone 1 - Value";

            internal const string Phone2Type = "Phone 2 - Type";
            internal const string Phone2Value = "Phone 2 - Value";

            internal const string Phone3Type = "Phone 3 - Type";
            internal const string Phone3Value = "Phone 3 - Value";

            internal const string Phone4Type = "Phone 4 - Type";
            internal const string Phone4Value = "Phone 4 - Value";

            internal const string Phone5Type = "Phone 5 - Type";
            internal const string Phone5Value = "Phone 5 - Value";

            internal const string Phone6Type = "Phone 6 - Type";
            internal const string Phone6Value = "Phone 6 - Value";

            internal const string Phone7Type = "Phone 7 - Type";
            internal const string Phone7Value = "Phone 7 - Value";

            internal const string Address1Type = "Address 1 - Type";
            internal const string Address1Formatted = "Address 1 - Formatted";
            internal const string Address1Street = "Address 1 - Street";
            internal const string Address1City = "Address 1 - City";
            internal const string Address1POBox = "Address 1 - PO Box";
            internal const string Address1Region = "Address 1 - Region";
            internal const string Address1PostalCode = "Address 1 - Postal Code";
            internal const string Address1Country = "Address 1 - Country";
            internal const string Address1ExtendedAddress = "Address 1 - Extended Address";

            internal const string Address2Type = "Address 2 - Type";
            internal const string Address2Formatted = "Address 2 - Formatted";
            internal const string Address2Street = "Address 2 - Street";
            internal const string Address2City = "Address 2 - City";
            internal const string Address2POBox = "Address 2 - PO Box";
            internal const string Address2Region = "Address 2 - Region";
            internal const string Address2PostalCode = "Address 2 - Postal Code";
            internal const string Address2Country = "Address 2 - Country";
            internal const string Address2ExtendedAddress = "Address 2 - Extended Address";

            internal const string Address3Type = "Address 3 - Type";
            internal const string Address3Formatted = "Address 3 - Formatted";
            internal const string Address3Street = "Address 3 - Street";
            internal const string Address3City = "Address 3 - City";
            internal const string Address3POBox = "Address 3 - PO Box";
            internal const string Address3Region = "Address 3 - Region";
            internal const string Address3PostalCode = "Address 3 - Postal Code";
            internal const string Address3Country = "Address 3 - Country";
            internal const string Address3ExtendedAddress = "Address 3 - Extended Address";

            internal const string OrganizationType = "Organization 1 - Type";
            internal const string OrganizationName = "Organization 1 - Name";
            internal const string OrganizationYomiName = "Organization 1 - Yomi Name";
            internal const string OrganizationTitle = "Organization 1 - Title";
            internal const string OrganizationDepartment = "Organization 1 - Department";
            internal const string OrganizationSymbol = "Organization 1 - Symbol";
            internal const string OrganizationLocation = "Organization 1 - Location";
            internal const string OrganizationJobDescription = "Organization 1 - Job Description";

            internal const string RelationType = "Relation 1 - Type";
            internal const string RelationValue = "Relation 1 - Value";

            internal const string Website1Type = "Website 1 - Type";
            internal const string Website1Value = "Website 1 - Value";

            internal const string Website2Type = "Website 2 - Type";
            internal const string Website2Value = "Website 2 - Value";
        }

        

        internal static string[] GetColumnNamesEn() => new string[]
        {
            En.Name,
            En.GivenName,
            En.AdditionalName,
            En.FamilyName,
            En.YomiName,
            En.GivenNameYomi,
            En.AdditionalNameYomi,
            En.FamilyNameYomi,
            En.NamePrefix,
            En.NameSuffix,
            En.Initials,
            En.Nickname,
            En.ShortName,
            En.MaidenName,

            En.Birthday,
            En.Gender,

            En.Location,
            En.BillingInformation,
            En.DirectoryServer,
            En.Mileage,
            En.Occupation,
            En.Hobby,
            En.Sensitivity,
            En.Priority,
            En.Subject,
            En.Notes,
            En.Language,
            En.Photo,
            En.GroupMembership,

            En.Email1Type,
            En.Email1Value,

            En.Email2Type,
            En.Email2Value,

            En.InstantMessengerType,
            En.InstantMessengerService,
            En.InstantMessengerValue,

            En.Phone1Type,
            En.Phone1Value,

            En.Phone2Type,
            En.Phone2Value,

            En.Phone3Type,
            En.Phone3Value,

            En.Phone4Type,
            En.Phone4Value,

            En.Phone5Type,
            En.Phone5Value,

            En.Phone6Type,
            En.Phone6Value,

            En.Phone7Type,
            En.Phone7Value,

            En.Address1Type,
            En.Address1Formatted,
            En.Address1Street,
            En.Address1City,
            En.Address1POBox,
            En.Address1Region,
            En.Address1PostalCode,
            En.Address1Country,
            En.Address1ExtendedAddress,

            En.Address2Type,
            En.Address2Formatted,
            En.Address2Street,
            En.Address2City,
            En.Address2POBox,
            En.Address2Region,
            En.Address2PostalCode,
            En.Address2Country,
            En.Address2ExtendedAddress,

            En.Address3Type,
            En.Address3Formatted,
            En.Address3Street,
            En.Address3City,
            En.Address3POBox,
            En.Address3Region,
            En.Address3PostalCode,
            En.Address3Country,
            En.Address3ExtendedAddress,

            En.OrganizationType,
            En.OrganizationName,
            En.OrganizationYomiName,
            En.OrganizationTitle,
            En.OrganizationDepartment,
            En.OrganizationSymbol,
            En.OrganizationLocation,
            En.OrganizationJobDescription,

            En.RelationType,
            En.RelationValue,

            En.Website1Type,
            En.Website1Value,
            En.Website2Type,
            En.Website2Value,
        };

        internal static IList<Tuple<string, ContactProp?, IList<string>>> GetMappingEN()
        {
            throw new NotImplementedException();
        }

    }
}
