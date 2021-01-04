using FolkerKinzel.VCards;
using FolkerKinzel.VCards.Models.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VC = FolkerKinzel.VCards.Models;

namespace FolkerKinzel.Contacts.IO.Intls.Vcf
{
    internal static class VcfReader
    {
        /// <summary>
        /// Liest eine vCard-Datei und gibt ihre Daten als <see cref="Contact"/>-Array zurück. (Eine vCard-Datei kann
        /// mehrere aneinandergehängte vCards enthalten.) Enthält die Datei keinen Text, wird ein leeres Array zurückgegeben.
        /// </summary>
        /// <param name="fileName">Der vollständige Pfad der vCard-Datei.</param>
        /// <returns>Die Daten der vCard als <see cref="Contact"/>-Array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
        internal static List<Contact> Read(string fileName)
        {
            List<VCard> vcards = VCard.Load(fileName);
           

            var wabContacts = new List<Contact>(vcards.Count);

            for (int i = 0; i < vcards.Count; i++)
            {
                wabContacts.Add(ConvertToContact(vcards[i]));
            }

            return wabContacts;
        }



        private static Contact ConvertToContact(VCard vcard)
        {
            var contact = new Contact();

#if NET40
            IEnumerable<string> emptyArray = new string[0];
#else
            IEnumerable<string> emptyArray = Array.Empty<string>();
#endif
            

            foreach (KeyValuePair<VC.Enums.VCdProp, object> property in vcard)
            {
                switch (property.Key)
                {
                    //case VCards.Model.Enums.VCdProp.Version:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Mailer:
                    //    break;
                    case VC::Enums.VCdProp.LastRevision:
                        {
                            contact.TimeStamp = ((VC::TimestampProperty)property.Value).Value.DateTime;
                            
                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.UniqueIdentifier:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Categories:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.TimeZones:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.GeoCoordinates:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Access:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Kind:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Source:
                    //    break;
                    case VC::Enums.VCdProp.DisplayNames:
                        contact.DisplayName = ((IEnumerable<VC::TextProperty>)property.Value)
                            .Where(x => !x.IsEmpty)
                            .OrderBy(x => x.Parameters.Preference)
                            .FirstOrDefault()?.Value;
                        break;
                    case VC::Enums.VCdProp.NameViews:
                        {
                            VC.PropertyParts.Name? vcardName = ((IEnumerable<VC::NameProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .FirstOrDefault()?.Value;

                            if(vcardName != null)
                            {
                                Person? person = contact.Person ?? new Person();
                                contact.Person = person;

                                var name = new Name();
                                person.Name = name;

                                string separator = " ";
                                name.LastName = string.Join(separator, vcardName.LastName ?? emptyArray);
                                name.FirstName = string.Join(separator, vcardName.FirstName ?? emptyArray);
                                name.MiddleName = string.Join(separator, vcardName.MiddleName ?? emptyArray);
                                name.Prefix = string.Join(separator, vcardName.Prefix ?? emptyArray);
                                name.Suffix = string.Join(separator, vcardName.Suffix ?? emptyArray);
                            }
                            break;
                        }
                    case VC::Enums.VCdProp.NickNames:
                        {
                            Person? person = contact.Person ?? new Person();
                            contact.Person = person;

                            person.NickName = ((IEnumerable<VC::StringCollectionProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value.FirstOrDefault();

                            break;
                        }
                    case VC::Enums.VCdProp.Organizations:
                        {
                            VC.PropertyParts.Organization? org = ((IEnumerable<VC::OrganizationProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value;

                            if (org != null)
                            {
                                Work? work = contact.Work ?? new Work();
                                contact.Work = work;

                                work.Company = org.OrganizationName;
                                work.Department = org.OrganizationalUnits?.FirstOrDefault();
                                work.Office = org.OrganizationalUnits?.ElementAtOrDefault(1);
                            }

                            break;
                        }
                    case VC::Enums.VCdProp.Titles:
                        {
                            var title = ((IEnumerable<VC::TextProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value;

                            if (title != null)
                            {
                                Work? work = contact.Work ?? new Work();
                                contact.Work = work;

                                work.JobTitle = title;
                            }

                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.Roles:
                    //    break;
                    case VC::Enums.VCdProp.GenderViews:
                        {
                            Person? person = contact.Person ?? new Person();
                            contact.Person = person;

                            VC.PropertyParts.Gender? vcardGender = ((IEnumerable<VC::GenderProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .FirstOrDefault()?.Value;

                            if (vcardGender != null)
                            {
                                switch (vcardGender.Sex)
                                {
                                    case VC::Enums.VCdSex.Female:
                                        person.Gender = Sex.Female;
                                        break;
                                    case VC::Enums.VCdSex.Male:
                                        person.Gender = Sex.Male;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.Languages:
                    //    break;
                    case VC::Enums.VCdProp.BirthDayViews:
                        {
                            VC.DateTimeOffsetProperty? birthdayProp = ((IEnumerable<VC::DateTimeProperty>)property.Value)
                                               .Select(x => x as VC::DateTimeOffsetProperty)
                                               .Where(x => x != null && !x.IsEmpty)
                                               .FirstOrDefault();

                            if (birthdayProp != null)
                            {
                                Person? person = contact.Person ?? new Person();
                                contact.Person = person;

                                person.BirthDay = birthdayProp.Value?.Date;
                            }
                            break;
                        }
                    case VC::Enums.VCdProp.AnniversaryViews:
                        {
                            VC.DateTimeOffsetProperty? anniversaryProp = ((IEnumerable<VC::DateTimeProperty>)property.Value)
                                               .Select(x => x as VC::DateTimeOffsetProperty)
                                               .Where(x => x != null && !x.IsEmpty)
                                               .FirstOrDefault();

                            if (anniversaryProp != null)
                            {
                                Person? person = contact.Person ?? new Person();
                                contact.Person = person;

                                person.Anniversary = anniversaryProp.Value?.Date;

                            }
                            break;
                        }
                    case VC::Enums.VCdProp.Relations:
                        {
                            VC.RelationProperty? spouseProp = ((IEnumerable<VC::RelationProperty>)property.Value)
                                               .Where(x => x.Parameters.RelationType.IsSet(VC::Enums.RelationTypes.Spouse)
                                                    && x.IsEmpty
                                                    && (x is VC::RelationTextProperty || x is VC::RelationVCardProperty))
                                               .FirstOrDefault();
                            if(spouseProp is null)
                            {
                                break;
                            }
                            var spouseName = spouseProp switch
                            {
                                VC::RelationTextProperty textProp => textProp.Value,
                                VC::RelationVCardProperty vcProp => ((IEnumerable<VC::TextProperty>?)vcProp.Value?.DisplayNames)?
                                            .Where(x => !x.IsEmpty)
                                            .OrderBy(x => x.Parameters.Preference)
                                            .FirstOrDefault()?.Value,

                                _ => null,
                            };

                            if(spouseName != null)
                            {
                                Person? person = contact.Person ?? new Person();
                                contact.Person = person;
                                person.Spouse = spouseName;
                            }
                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.Logos:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Photos:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Sounds:
                    //    break;
                    case VC::Enums.VCdProp.Addresses:
                        {
                            var vcardAddresses = (IEnumerable<VC::AddressProperty>)property.Value;

                            string separator = " ";

                            foreach (VC.AddressProperty? vcardAddress in vcardAddresses.Where(x => x.Value != null).OrderByDescending(x => x.Parameters.Preference))
                            {
                                if(vcardAddress.Parameters.PropertyClass.IsSet(VC::Enums.PropertyClassTypes.Work))
                                {
                                    Work? work = contact.Work ?? new Work();
                                    contact.Work = work;

                                    work.AddressWork = new Address()
                                    {
                                        Street = string.Join(separator, vcardAddress.Value.Street ?? emptyArray),
                                        City = string.Join(separator, vcardAddress.Value.Locality ?? emptyArray),
                                        PostalCode = string.Join(separator, vcardAddress.Value.PostalCode ?? emptyArray),
                                        State = string.Join(separator, vcardAddress.Value.Region ?? emptyArray),
                                        Country = string.Join(separator, vcardAddress.Value.Country ?? emptyArray),
                                    };
                                }
                                else
                                {
                                    contact.AddressHome = new Address()
                                    {
                                        Street = string.Join(separator, vcardAddress.Value.Street ?? emptyArray),
                                        City = string.Join(separator, vcardAddress.Value.Locality ?? emptyArray),
                                        PostalCode = string.Join(separator, vcardAddress.Value.PostalCode ?? emptyArray),
                                        State = string.Join(separator, vcardAddress.Value.Region ?? emptyArray),
                                        Country = string.Join(separator, vcardAddress.Value.Country ?? emptyArray),
                                    };
                                }
                            }

                            break;
                        }
                    case VC::Enums.VCdProp.PhoneNumbers:
                        {
                            var vcardNumbers = (IEnumerable<VC::TextProperty>)property.Value;

                            if(vcardNumbers != null)
                            {
                                contact.PhoneNumbers = vcardNumbers
                                    .Where(x => x.Value != null)
                                    .OrderBy(x => x.Parameters.Preference)
                                    .Select(x => new PhoneNumber(x.Value,
                                                                 x.Parameters.PropertyClass.IsSet(VC.Enums.PropertyClassTypes.Work),
                                                                 x.Parameters.TelephoneType.IsSet(VC.Enums.TelTypes.Cell),
                                                                 x.Parameters.TelephoneType.IsSet(VC.Enums.TelTypes.Fax)));
                            }

                            break;
                        }
                    case VC::Enums.VCdProp.EmailAddresses:
                        {
                            var vcardEmails = (IEnumerable<VC::TextProperty>)property.Value;

                            if (vcardEmails != null)
                            {
                                var emails = vcardEmails.Where(x => x.Value != null).OrderBy(x => x.Parameters.Preference).Select(x => x.Value).ToArray();
                                contact.EmailAddresses = emails;
                            }
                            break;
                        }
                    case VC::Enums.VCdProp.URLs:
                        {
                            var vcardUrls = (IEnumerable<VC::TextProperty>)property.Value;

                            if (vcardUrls != null)
                            {
                                foreach (VC.TextProperty? url in vcardUrls.Where(x => x.Value != null).OrderByDescending(x => x.Parameters.Preference))
                                {
                                    if (url.Parameters.PropertyClass.IsSet(VC::Enums.PropertyClassTypes.Work))
                                    {
                                        contact.WebPageWork ??= url.Value;
                                    }
                                    else
                                    {
                                        contact.WebPagePersonal ??= url.Value;
                                    }
                                }
                            }
                            break;
                        }
                    case VC::Enums.VCdProp.InstantMessengerHandles:
                        {
                            var vcardIMPPs = (IEnumerable<VC::TextProperty>)property.Value;

                            if (vcardIMPPs != null)
                            {
                                var impps = vcardIMPPs.Where(x => x.Value != null).OrderBy(x => x.Parameters.Preference).Select(x => x.Value).ToArray();
                                contact.InstantMessengerHandles = impps;
                            }
                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.PublicKeys:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.CalendarUserAddresses:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.CalendarAddresses:
                    //    break;
                    case VC::Enums.VCdProp.Notes:
                        {
                            contact.Comment = ((IEnumerable<VC::TextProperty>)property.Value).FirstOrDefault(x => x.Value != null)?.Value;
                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.NonStandardProperties:
                    //    break;
                    default:
                        break;
                }//switch
            }//foreach

            contact.Clean();

            return contact;
        }
    }
}