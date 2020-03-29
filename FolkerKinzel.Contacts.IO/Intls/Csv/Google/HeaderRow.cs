using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal static class En
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

        // bis hierher alle Spalten verwenden, danach optional

        internal const string Email1Type = "E-mail 1 - Type";
        internal const string Email1Value = "E-mail 1 - Value";

        internal const string Email2Type = "E-mail 2 - Type";
        internal const string Email2Value = "E-mail 2 - Value";

        internal const string IM1Type = "IM 1 - Type";
        internal const string IM1Service = "IM 1 - Service";
        internal const string IM1Value = "IM 1 - Value";

        internal const string IM2Type = "IM 2 - Type";
        internal const string IM2Service = "IM 2 - Service";
        internal const string IM2Value = "IM 2 - Value";

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

        internal const string Phone8Type = "Phone 8 - Type";
        internal const string Phone8Value = "Phone 8 - Value";

        internal const string Phone9Type = "Phone 9 - Type";
        internal const string Phone9Value = "Phone 9 - Value";

        internal const string AddressHomeType = "Address 1 - Type";
        internal const string AddressHomeFormatted = "Address 1 - Formatted";
        internal const string AddressHomeStreet = "Address 1 - Street";
        internal const string AddressHomeCity = "Address 1 - City";
        internal const string AddressHomePOBox = "Address 1 - PO Box";
        internal const string AddressHomeState = "Address 1 - Region";
        internal const string AddressHomePostalCode = "Address 1 - Postal Code";
        internal const string AddressHomeCountry = "Address 1 - Country";
        internal const string AddressHomeExtendedAddress = "Address 1 - Extended Address";

        internal const string AddressWorkType = "Address 2 - Type";
        internal const string AddressWorkFormatted = "Address 2 - Formatted";
        internal const string AddressWorkStreet = "Address 2 - Street";
        internal const string AddressWorkCity = "Address 2 - City";
        internal const string AddressWorkPOBox = "Address 2 - PO Box";
        internal const string AddressWorkRegion = "Address 2 - Region";
        internal const string AddressWorkPostalCode = "Address 2 - Postal Code";
        internal const string AddressWorkCountry = "Address 2 - Country";
        internal const string AddressWorkExtendedAddress = "Address 2 - Extended Address";

        //internal const string Address3Type = "Address 3 - Type";
        //internal const string Address3Formatted = "Address 3 - Formatted";
        //internal const string Address3Street = "Address 3 - Street";
        //internal const string Address3City = "Address 3 - City";
        //internal const string Address3POBox = "Address 3 - PO Box";
        //internal const string Address3Region = "Address 3 - Region";
        //internal const string Address3PostalCode = "Address 3 - Postal Code";
        //internal const string Address3Country = "Address 3 - Country";
        //internal const string Address3ExtendedAddress = "Address 3 - Extended Address";

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

        internal const string EventType = "Event 1 - Type";
        internal const string EventValue = "Event 1 - Value";

        //internal const string CustomField1Type = "Custom Field 1 - Type";
        //internal const string CustomField1Value = "Custom Field 1 - Value";
    }


    internal static class HeaderRow
    {

        

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

            En.IM1Type,
            En.IM1Service,
            En.IM1Value,

            En.IM2Type,
            En.IM2Service,
            En.IM2Value,

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

            En.Phone8Type,
            En.Phone8Value,

            En.Phone9Type,
            En.Phone9Value,

            En.AddressHomeType,
            En.AddressHomeFormatted,
            En.AddressHomeStreet,
            En.AddressHomeCity,
            En.AddressHomePOBox,
            En.AddressHomeState,
            En.AddressHomePostalCode,
            En.AddressHomeCountry,
            En.AddressHomeExtendedAddress,

            En.AddressWorkType,
            En.AddressWorkFormatted,
            En.AddressWorkStreet,
            En.AddressWorkCity,
            En.AddressWorkPOBox,
            En.AddressWorkRegion,
            En.AddressWorkPostalCode,
            En.AddressWorkCountry,
            En.AddressWorkExtendedAddress,

            //En.Address3Type,
            //En.Address3Formatted,
            //En.Address3Street,
            //En.Address3City,
            //En.Address3POBox,
            //En.Address3Region,
            //En.Address3PostalCode,
            //En.Address3Country,
            //En.Address3ExtendedAddress,

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


        internal static IList<Tuple<string, ContactProp?, IList<string>>> GetMappingEN() => new Tuple<string, ContactProp?, IList<string>>[]
        {
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Name),                       ContactProp.DisplayName,                     new string[]{En.Name}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.GivenName),                  ContactProp.FirstName,                       new string[]{En.GivenName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AdditionalName),             ContactProp.MiddleName,                      new string[]{En.AdditionalName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.FamilyName),                 ContactProp.LastName,                        new string[]{En.FamilyName}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.YomiName),                   null,                                        new string[]{En.YomiName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.GivenNameYomi),              null,                                        new string[]{En.GivenNameYomi}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AdditionalNameYomi),         null,                                        new string[]{En.AdditionalNameYomi}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.FamilyNameYomi),             null,                                        new string[]{En.FamilyNameYomi}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.NamePrefix),                 ContactProp.NamePrefix,                      new string[]{En.NamePrefix}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.NameSuffix),                 ContactProp.NameSuffix,                      new string[]{En.NameSuffix}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Initials),                   null,                                        new string[]{En.Initials}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Nickname),                   ContactProp.NickName,                        new string[]{En.Nickname}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.ShortName),                  null,                                        new string[]{En.ShortName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.MaidenName),                 null,                                        new string[]{En.MaidenName}),
                                                                                                                                               
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Birthday),                   ContactProp.BirthDay,                        new string[]{En.Birthday}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Gender),                     ContactProp.Gender,                          new string[]{En.Gender}),
        
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Location),                   null,                                        new string[]{En.Location}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.BillingInformation),         null,                                        new string[]{En.BillingInformation}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.DirectoryServer),            null,                                        new string[]{En.DirectoryServer}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Mileage),                    null,                                        new string[]{En.Mileage}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Occupation),                 null,                                        new string[]{En.Occupation}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Hobby),                      null,                                        new string[]{En.Hobby}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Sensitivity),                null,                                        new string[]{En.Sensitivity}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Priority),                   null,                                        new string[]{En.Priority}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Subject),                    null,                                        new string[]{En.Subject}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Notes),                      ContactProp.Comment,                         new string[]{En.Notes}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Language),                   null,                                        new string[]{En.Language}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Photo),                      null,                                        new string[]{En.Photo }),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.GroupMembership),            null,                                        new string[]{En.GroupMembership }),
                                  
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Email1Type),                 null,                                        new string[]{En.Email1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Email1Value),                ContactProp.Email1,                          new string[]{En.Email1Value}),
           
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Email2Type),                 null,                                        new string[]{En.Email2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Email2Value),                ContactProp.Email2,                          new string[]{En.Email2Value}),
           
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM1Type),                    (ContactProp)AdditionalProp.InstantMessenger1Type,    new string[]{En.IM1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM1Service),                 (ContactProp)AdditionalProp.InstantMessenger1Service, new string[]{En.IM1Service}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM1Value),                   ContactProp.InstantMessenger1,                        new string[]{En.IM1Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM2Type),                    (ContactProp)AdditionalProp.InstantMessenger2Type,    new string[]{En.IM2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM2Service),                 (ContactProp)AdditionalProp.InstantMessenger2Service, new string[]{En.IM2Service}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.IM2Value),                   ContactProp.InstantMessenger2,                        new string[]{En.IM2Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone1Type),                 (ContactProp)AdditionalProp.Phone1Type,      new string[]{En.Phone1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone1Value),                (ContactProp)AdditionalProp.Phone1Value,     new string[]{En.Phone1Value}),
           
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone2Type),                 (ContactProp)AdditionalProp.Phone2Type,      new string[]{En.Phone2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone2Value),                (ContactProp)AdditionalProp.Phone2Value,     new string[]{En.Phone2Value}),
          
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone3Type),                 (ContactProp)AdditionalProp.Phone3Type,      new string[]{En.Phone3Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone3Value),                (ContactProp)AdditionalProp.Phone3Value,     new string[]{En.Phone3Value}),
       
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone4Type),                 (ContactProp)AdditionalProp.Phone4Type,      new string[]{En.Phone4Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone4Value),                (ContactProp)AdditionalProp.Phone4Value,     new string[]{En.Phone4Value}),
         
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone5Type),                 (ContactProp)AdditionalProp.Phone5Type,      new string[]{En.Phone5Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone5Value),                (ContactProp)AdditionalProp.Phone5Value,     new string[]{En.Phone5Value}),
           
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone6Type),                 (ContactProp)AdditionalProp.Phone6Type,      new string[]{En.Phone6Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone6Value),                (ContactProp)AdditionalProp.Phone6Value,     new string[]{En.Phone6Value}),
      
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone7Type),                 (ContactProp)AdditionalProp.Phone7Type,      new string[]{En.Phone7Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone7Value),                (ContactProp)AdditionalProp.Phone7Value,     new string[]{En.Phone7Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone8Type),                 (ContactProp)AdditionalProp.Phone8Type,      new string[]{En.Phone8Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone8Value),                (ContactProp)AdditionalProp.Phone8Value,     new string[]{En.Phone8Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone9Type),                 (ContactProp)AdditionalProp.Phone9Type,      new string[]{En.Phone9Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Phone9Value),                (ContactProp)AdditionalProp.Phone9Value,     new string[]{En.Phone9Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeType),               (ContactProp)AdditionalProp.AddressHomeType,    new string[]{En.AddressHomeType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeFormatted),          null,                                        new string[]{En.AddressHomeFormatted}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeStreet),             ContactProp.AddressHomeStreet,               new string[]{En.AddressHomeStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeCity),               ContactProp.AddressHomeCity,                 new string[]{En.AddressHomeCity}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomePOBox),              null,                                        new string[]{En.AddressHomePOBox}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeState),             ContactProp.AddressHomeState,                new string[]{En.AddressHomeState}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomePostalCode),         ContactProp.AddressHomePostalCode,           new string[]{En.AddressHomePostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeCountry),            ContactProp.AddressHomeCountry,              new string[]{En.AddressHomeCountry}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressHomeExtendedAddress),    null,                                        new string[]{En.AddressHomeExtendedAddress}),
        
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkType),               (ContactProp)AdditionalProp.AddressWorkType,       new string[]{En.AddressWorkType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkFormatted),          null,                                           new string[]{En.AddressWorkFormatted}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkStreet),             (ContactProp)AdditionalProp.AddressWorkStreet,     new string[]{En.AddressWorkStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkCity),               (ContactProp)AdditionalProp.AddressWorkCity,       new string[]{En.AddressWorkCity }),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkPOBox),              null,                                           new string[]{En.AddressWorkPOBox}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkRegion),             (ContactProp)AdditionalProp.AddressWorkState,     new string[]{En.AddressWorkRegion}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkPostalCode),         (ContactProp)AdditionalProp.AddressWorkPostalCode, new string[]{En.AddressWorkPostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkCountry),            (ContactProp)AdditionalProp.AddressWorkCountry,    new string[]{En.AddressWorkCountry}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.AddressWorkExtendedAddress),    null,                                           new string[]{En.AddressWorkExtendedAddress}),
                                                                                                                                               
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Type),               null,                                        new string[]{En.Address3Type}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Formatted),          null,                                        new string[]{En.Address3Formatted}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Street),             null,                                        new string[]{En.Address3Street}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3City),               null,                                        new string[]{En.Address3City}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3POBox),              null,                                        new string[]{En.Address3POBox}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Region),             null,                                        new string[]{En.Address3Region}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3PostalCode),         null,                                        new string[]{En.Address3PostalCode}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Country),            null,                                        new string[]{En.Address3Country}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3ExtendedAddress),    null,                                        new string[]{En.Address3ExtendedAddress}),
                                                                                                                                               
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationType),           null,                                        new string[]{En.OrganizationType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationName),           ContactProp.WorkCompany,                     new string[]{En.OrganizationName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationYomiName),       null,                                        new string[]{En.OrganizationYomiName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationTitle),          ContactProp.WorkPosition,                    new string[]{En.OrganizationTitle}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationDepartment),     ContactProp.WorkDepartment,                  new string[]{En.OrganizationDepartment}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationSymbol),         null,                                        new string[]{En.OrganizationSymbol}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationLocation),       null,                                        new string[]{En.OrganizationLocation}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.OrganizationJobDescription), null,                                        new string[]{En.OrganizationJobDescription}),
                          
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.RelationType),               (ContactProp)AdditionalProp.RelationType,    new string[]{En.RelationType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.RelationValue),              ContactProp.Spouse,                          new string[]{En.RelationValue}),
                
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Website1Type),               (ContactProp)AdditionalProp.Website1Type,    new string[]{En.Website1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Website1Value),              (ContactProp)AdditionalProp.Website1Value,   new string[]{En.Website1Value }),
                                
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Website2Type),               (ContactProp)AdditionalProp.Website2Type,    new string[]{En.Website2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.Website2Value),              (ContactProp)AdditionalProp.Website2Value,   new string[]{En.Website2Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(En.EventType),                  (ContactProp)AdditionalProp.EventType,         new string[]{En.EventType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(En.EventValue),                 ContactProp.Anniversary,                       new string[]{En.EventValue})
        };

    }
}
