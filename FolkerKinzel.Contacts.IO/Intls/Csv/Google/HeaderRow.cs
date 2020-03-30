using System;
using System.Collections.Generic;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{
    internal static class HeaderRow
    {
        internal static string[] GetColumnNamesEn() => new string[]
        {
            ColumnName.Name,

            ColumnName.GivenName,
            ColumnName.AdditionalName,
            ColumnName.FamilyName,

            ColumnName.YomiName,
            ColumnName.GivenNameYomi,
            ColumnName.AdditionalNameYomi,
            ColumnName.FamilyNameYomi,

            ColumnName.NamePrefix,
            ColumnName.NameSuffix,

            ColumnName.Initials,
            ColumnName.Nickname,
            ColumnName.ShortName,
            ColumnName.MaidenName,

            ColumnName.Birthday,
            ColumnName.Gender,

            ColumnName.Location,
            ColumnName.BillingInformation,
            ColumnName.DirectoryServer,
            ColumnName.Mileage,
            ColumnName.Occupation,
            ColumnName.Hobby,
            ColumnName.Sensitivity,
            ColumnName.Priority,
            ColumnName.Subject,
            ColumnName.Notes,
            ColumnName.Language,
            ColumnName.Photo,
            ColumnName.GroupMembership,

            ColumnName.Email1Type,
            ColumnName.Email1Value,

            ColumnName.Email2Type,
            ColumnName.Email2Value,

            ColumnName.IM1Type,
            ColumnName.IM1Service,
            ColumnName.IM1Value,

            ColumnName.IM2Type,
            ColumnName.IM2Service,
            ColumnName.IM2Value,

            ColumnName.Phone1Type,
            ColumnName.Phone1Value,

            ColumnName.Phone2Type,
            ColumnName.Phone2Value,

            ColumnName.Phone3Type,
            ColumnName.Phone3Value,

            ColumnName.Phone4Type,
            ColumnName.Phone4Value,

            ColumnName.Phone5Type,
            ColumnName.Phone5Value,

            ColumnName.Phone6Type,
            ColumnName.Phone6Value,

            ColumnName.Phone7Type,
            ColumnName.Phone7Value,

            ColumnName.Phone8Type,
            ColumnName.Phone8Value,

            ColumnName.Phone9Type,
            ColumnName.Phone9Value,

            ColumnName.AddressHomeType,
            ColumnName.AddressHomeFormatted,
            ColumnName.AddressHomeStreet,
            ColumnName.AddressHomeCity,
            ColumnName.AddressHomePOBox,
            ColumnName.AddressHomeState,
            ColumnName.AddressHomePostalCode,
            ColumnName.AddressHomeCountry,
            ColumnName.AddressHomeExtendedAddress,

            ColumnName.AddressWorkType,
            ColumnName.AddressWorkFormatted,
            ColumnName.AddressWorkStreet,
            ColumnName.AddressWorkCity,
            ColumnName.AddressWorkPOBox,
            ColumnName.AddressWorkState,
            ColumnName.AddressWorkPostalCode,
            ColumnName.AddressWorkCountry,
            ColumnName.AddressWorkExtendedAddress,

            //En.Address3Type,
            //En.Address3Formatted,
            //En.Address3Street,
            //En.Address3City,
            //En.Address3POBox,
            //En.Address3Region,
            //En.Address3PostalCode,
            //En.Address3Country,
            //En.Address3ExtendedAddress,

            ColumnName.OrganizationType,
            ColumnName.OrganizationName,
            ColumnName.OrganizationYomiName,
            ColumnName.OrganizationTitle,
            ColumnName.OrganizationDepartment,
            ColumnName.OrganizationSymbol,
            ColumnName.OrganizationLocation,
            ColumnName.OrganizationJobDescription,

            ColumnName.RelationType,
            ColumnName.RelationValue,

            ColumnName.WebHomeType,
            ColumnName.WebHomeValue,

            ColumnName.WebWorkType,
            ColumnName.WebWorkValue,

            ColumnName.EventType,
            ColumnName.EventValue
        };


        internal static IList<Tuple<string, ContactProp?, IList<string>>> GetMapping() => new List<Tuple<string, ContactProp?, IList<string>>>
        {
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Name),                       ContactProp.DisplayName,                          new string[]{ColumnName.Name}),
                                                                                                                                                            
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.GivenName),                  ContactProp.FirstName,                            new string[]{ColumnName.GivenName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AdditionalName),             ContactProp.MiddleName,                           new string[]{ColumnName.AdditionalName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FamilyName),                 ContactProp.LastName,                             new string[]{ColumnName.FamilyName}),
                                                                                                                                                            
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.YomiName),                   null,                                             new string[]{ColumnName.YomiName}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.GivenNameYomi),              null,                                             new string[]{ColumnName.GivenNameYomi}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AdditionalNameYomi),         null,                                             new string[]{ColumnName.AdditionalNameYomi}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.FamilyNameYomi),             null,                                             new string[]{ColumnName.FamilyNameYomi}),
                                                                                                                                                            
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NamePrefix),                 ContactProp.NamePrefix,                           new string[]{ColumnName.NamePrefix}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.NameSuffix),                 ContactProp.NameSuffix,                           new string[]{ColumnName.NameSuffix}),

            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Initials),                   null,                                             new string[]{ColumnName.Initials}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Nickname),                   ContactProp.NickName,                             new string[]{ColumnName.Nickname}),
            // new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.ShortName),                  null,                                             new string[]{ColumnName.ShortName}),
            // new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.MaidenName),                 null,                                             new string[]{ColumnName.MaidenName}),
                                                                                                                                                            
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Birthday),                   ContactProp.BirthDay,                             new string[]{ColumnName.Birthday}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Gender),                     ContactProp.Gender,                               new string[]{ColumnName.Gender}),
                                                                                                                                                            
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Location),                   null,                                             new string[]{ColumnName.Location}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.BillingInformation),         null,                                             new string[]{ColumnName.BillingInformation}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.DirectoryServer),            null,                                             new string[]{ColumnName.DirectoryServer}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Mileage),                    null,                                             new string[]{ColumnName.Mileage}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Occupation),                 null,                                             new string[]{ColumnName.Occupation}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Hobby),                      null,                                             new string[]{ColumnName.Hobby}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Sensitivity),                null,                                             new string[]{ColumnName.Sensitivity}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Priority),                   null,                                             new string[]{ColumnName.Priority}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Subject),                    null,                                             new string[]{ColumnName.Subject}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Notes),                      ContactProp.Comment,                              new string[]{ColumnName.Notes}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Language),                   null,                                             new string[]{ColumnName.Language}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Photo),                      null,                                             new string[]{ColumnName.Photo }),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.GroupMembership),            null,                                             new string[]{ColumnName.GroupMembership }),
                                                                                                   
            // ////////////////////////////////////     bis hierher alle Spalten verwenden, danach optional  //////////////////////////////////////////////////////////////////////////////////////////

            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email1Type),                 null,                                             new string[]{ColumnName.Email1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email1Value),                ContactProp.Email1,                               new string[]{ColumnName.Email1Value}),
                                                                                                                                                            
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email2Type),                 null,                                             new string[]{ColumnName.Email2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Email2Value),                ContactProp.Email2,                               new string[]{ColumnName.Email2Value}),
           
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM1Type),                    (ContactProp)AdditionalProp.InstantMessenger1Type,    new string[]{ColumnName.IM1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM1Service),                 (ContactProp)AdditionalProp.InstantMessenger1Service, new string[]{ColumnName.IM1Service}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM1Value),                   ContactProp.InstantMessenger1,                        new string[]{ColumnName.IM1Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM2Type),                    (ContactProp)AdditionalProp.InstantMessenger2Type,    new string[]{ColumnName.IM2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM2Service),                 (ContactProp)AdditionalProp.InstantMessenger2Service, new string[]{ColumnName.IM2Service}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.IM2Value),                   ContactProp.InstantMessenger2,                        new string[]{ColumnName.IM2Value}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone1Type),                 (ContactProp)AdditionalProp.Phone1Type,            new string[]{ColumnName.Phone1Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone1Value),                (ContactProp)AdditionalProp.Phone1Value,           new string[]{ColumnName.Phone1Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone2Type),                 (ContactProp)AdditionalProp.Phone2Type,            new string[]{ColumnName.Phone2Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone2Value),                (ContactProp)AdditionalProp.Phone2Value,           new string[]{ColumnName.Phone2Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone3Type),                 (ContactProp)AdditionalProp.Phone3Type,            new string[]{ColumnName.Phone3Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone3Value),                (ContactProp)AdditionalProp.Phone3Value,           new string[]{ColumnName.Phone3Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone4Type),                 (ContactProp)AdditionalProp.Phone4Type,            new string[]{ColumnName.Phone4Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone4Value),                (ContactProp)AdditionalProp.Phone4Value,           new string[]{ColumnName.Phone4Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone5Type),                 (ContactProp)AdditionalProp.Phone5Type,            new string[]{ColumnName.Phone5Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone5Value),                (ContactProp)AdditionalProp.Phone5Value,           new string[]{ColumnName.Phone5Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone6Type),                 (ContactProp)AdditionalProp.Phone6Type,            new string[]{ColumnName.Phone6Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone6Value),                (ContactProp)AdditionalProp.Phone6Value,           new string[]{ColumnName.Phone6Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone7Type),                 (ContactProp)AdditionalProp.Phone7Type,            new string[]{ColumnName.Phone7Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone7Value),                (ContactProp)AdditionalProp.Phone7Value,           new string[]{ColumnName.Phone7Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone8Type),                 (ContactProp)AdditionalProp.Phone8Type,            new string[]{ColumnName.Phone8Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone8Value),                (ContactProp)AdditionalProp.Phone8Value,           new string[]{ColumnName.Phone8Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone9Type),                 (ContactProp)AdditionalProp.Phone9Type,            new string[]{ColumnName.Phone9Type}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.Phone9Value),                (ContactProp)AdditionalProp.Phone9Value,           new string[]{ColumnName.Phone9Value}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeType),            (ContactProp)AdditionalProp.AddressHomeType,       new string[]{ColumnName.AddressHomeType}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeFormatted),       null,                                              new string[]{ColumnName.AddressHomeFormatted}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeStreet),          ContactProp.AddressHomeStreet,                     new string[]{ColumnName.AddressHomeStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCity),            ContactProp.AddressHomeCity,                       new string[]{ColumnName.AddressHomeCity}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomePOBox),           null,                                              new string[]{ColumnName.AddressHomePOBox}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeState),           ContactProp.AddressHomeState,                      new string[]{ColumnName.AddressHomeState}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomePostalCode),      ContactProp.AddressHomePostalCode,                 new string[]{ColumnName.AddressHomePostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeCountry),         ContactProp.AddressHomeCountry,                    new string[]{ColumnName.AddressHomeCountry}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressHomeExtendedAddress), null,                                              new string[]{ColumnName.AddressHomeExtendedAddress}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkType),            (ContactProp)AdditionalProp.AddressWorkType,       new string[]{ColumnName.AddressWorkType}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkFormatted),       null,                                              new string[]{ColumnName.AddressWorkFormatted}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkStreet),          ContactProp.AddressWorkStreet,                     new string[]{ColumnName.AddressWorkStreet}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCity),            ContactProp.AddressWorkCity,                       new string[]{ColumnName.AddressWorkCity }),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkPOBox),           null,                                              new string[]{ColumnName.AddressWorkPOBox}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkState),           ContactProp.AddressWorkState,                      new string[]{ColumnName.AddressWorkState}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkPostalCode),      ContactProp.AddressWorkPostalCode,                 new string[]{ColumnName.AddressWorkPostalCode}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkCountry),         ContactProp.AddressWorkCountry,                    new string[]{ColumnName.AddressWorkCountry}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.AddressWorkExtendedAddress), null,                                              new string[]{ColumnName.AddressWorkExtendedAddress}),
                                                                                                                                               
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Type),                       null,                                              new string[]{En.Address3Type}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Formatted),                  null,                                              new string[]{En.Address3Formatted}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Street),                     null,                                              new string[]{En.Address3Street}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3City),                       null,                                              new string[]{En.Address3City}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3POBox),                      null,                                              new string[]{En.Address3POBox}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Region),                     null,                                              new string[]{En.Address3Region}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3PostalCode),                 null,                                              new string[]{En.Address3PostalCode}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3Country),                    null,                                              new string[]{En.Address3Country}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(En.Address3ExtendedAddress),            null,                                              new string[]{En.Address3ExtendedAddress}),
                                                                                                                                                             
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationType),           null,                                              new string[]{ColumnName.OrganizationType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationName),           ContactProp.WorkCompany,                           new string[]{ColumnName.OrganizationName}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationYomiName),       null,                                              new string[]{ColumnName.OrganizationYomiName}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationTitle),          ContactProp.WorkPosition,                          new string[]{ColumnName.OrganizationTitle}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationDepartment),     ContactProp.WorkDepartment,                        new string[]{ColumnName.OrganizationDepartment}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationSymbol),         null,                                              new string[]{ColumnName.OrganizationSymbol}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationLocation),       null,                                              new string[]{ColumnName.OrganizationLocation}),
            //new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.OrganizationJobDescription), null,                                              new string[]{ColumnName.OrganizationJobDescription}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.RelationType),               (ContactProp)AdditionalProp.RelationType,          new string[]{ColumnName.RelationType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.RelationValue),              ContactProp.Spouse,                                new string[]{ColumnName.RelationValue}),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebHomeType),                (ContactProp)AdditionalProp.WebHomeType,           new string[]{ColumnName.WebHomeType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebHomeValue),               ContactProp.HomePagePersonal,                      new string[]{ColumnName.WebHomeValue }),
                                                                                                                                                             
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebWorkType),                (ContactProp)AdditionalProp.WebWorkType,           new string[]{ColumnName.WebWorkType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.WebWorkValue),               ContactProp.HomePageWork,                          new string[]{ColumnName.WebWorkValue}),

            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.EventType),                  (ContactProp)AdditionalProp.EventType,             new string[]{ColumnName.EventType}),
            new Tuple<string, ContactProp?, IList<string>>(nameof(ColumnName.EventValue),                 ContactProp.Anniversary,                           new string[]{ColumnName.EventValue}),


           
            

        };

    }
}
