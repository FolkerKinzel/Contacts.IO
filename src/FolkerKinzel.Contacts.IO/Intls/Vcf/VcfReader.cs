using FolkerKinzel.VCards;
using FolkerKinzel.VCards.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geladen werden.</exception>
        internal static List<Contact> Read(string fileName)
        {
            IList<VCard> vcards = VCard.LoadVcf(fileName);


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

            foreach (KeyValuePair<VC.Enums.VCdProp, object> property in vcard)
            {
                switch (property.Key)
                {
                    //case VCards.Model.Enums.VCdProp.Version:
                    //    break;
                    //case VCards.Model.Enums.VCdProp.Mailer:
                    //    break;
                    case VC::Enums.VCdProp.TimeStamp:
                        {
                            Debug.Assert(property.Value is VC::TimeStampProperty);

                            contact.TimeStamp = ((VC::TimeStampProperty)property.Value).Value.DateTime;

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
                        Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                        Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                        contact.DisplayName = ((IEnumerable<VC::TextProperty>)property.Value)
                            .Where(x => x.Value != null)
                            .OrderBy(x => x.Parameters.Preference)
                            .FirstOrDefault()?.Value;
                        break;
                    case VC::Enums.VCdProp.NameViews:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::NameProperty>);
                            Debug.Assert(((IEnumerable<VC::NameProperty>)property.Value).All(x => x != null));

                            VC::PropertyParts.Name? vcardName = ((IEnumerable<VC::NameProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .FirstOrDefault()?.Value;

                            if (vcardName != null)
                            {
                                Person? person = contact.Person ?? new Person();
                                contact.Person = person;

                                var name = new Name();
                                person.Name = name;

                                const string separator = " ";
                                name.LastName = string.Join(separator, vcardName.LastName);
                                name.FirstName = string.Join(separator, vcardName.FirstName);
                                name.MiddleName = string.Join(separator, vcardName.MiddleName);
                                name.Prefix = string.Join(separator, vcardName.Prefix);
                                name.Suffix = string.Join(separator, vcardName.Suffix);
                            }
                            break;
                        }
                    case VC::Enums.VCdProp.NickNames:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::StringCollectionProperty>);
                            Debug.Assert(((IEnumerable<VC::StringCollectionProperty>)property.Value).All(x => x != null));

                            Person? person = contact.Person ?? new Person();
                            contact.Person = person;

                            person.NickName = ((IEnumerable<VC::StringCollectionProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value!.FirstOrDefault();

                            break;
                        }
                    case VC::Enums.VCdProp.Organizations:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::OrganizationProperty>);
                            Debug.Assert(((IEnumerable<VC::OrganizationProperty>)property.Value).All(x => x != null));

                            VC::PropertyParts.Organization? org = ((IEnumerable<VC::OrganizationProperty>)property.Value)
                                .Where(x => !x.IsEmpty)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value;

                            if (org != null)
                            {
                                Work work = contact.Work ?? new Work();
                                contact.Work = work;

                                work.Company = org.OrganizationName;
                                work.Department = org.OrganizationalUnits?.FirstOrDefault();
                                work.Office = org.OrganizationalUnits?.ElementAtOrDefault(1);
                            }

                            break;
                        }
                    case VC::Enums.VCdProp.Titles:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            var title = ((IEnumerable<VC::TextProperty>)property.Value)
                                .Where(x => x.Value != null)
                                .OrderBy(x => x.Parameters.Preference)
                                .FirstOrDefault()?.Value;

                            if (title != null)
                            {
                                Work work = contact.Work ?? new Work();
                                contact.Work = work;

                                work.JobTitle = title;
                            }

                            break;
                        }
                    //case VCards.Model.Enums.VCdProp.Roles:
                    //    break;
                    case VC::Enums.VCdProp.GenderViews:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::GenderProperty>);
                            Debug.Assert(((IEnumerable<VC::GenderProperty>)property.Value).All(x => x != null));

                            Person person = contact.Person ?? new Person();
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
                            Debug.Assert(property.Value is IEnumerable<VC::DateTimeProperty>);
                            Debug.Assert(((IEnumerable<VC::DateTimeProperty>)property.Value).All(x => x != null));

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
                            Debug.Assert(property.Value is IEnumerable<VC::DateTimeProperty>);
                            Debug.Assert(((IEnumerable<VC::DateTimeProperty>)property.Value).All(x => x != null));

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
                            Debug.Assert(property.Value is IEnumerable<VC::RelationProperty>);
                            Debug.Assert(((IEnumerable<VC::RelationProperty>)property.Value).All(x => x != null));

                            VC.RelationProperty? spouseProp = ((IEnumerable<VC::RelationProperty>)property.Value)
                                               .Where(x => (!x.IsEmpty)
                                                    && x.Parameters.RelationType.IsSet(VC::Enums.RelationTypes.Spouse)
                                                    && (x is VC::RelationTextProperty || x is VC::RelationVCardProperty))
                                               .FirstOrDefault();

                            var spouseName = spouseProp switch
                            {
                                VC::RelationTextProperty textProp => textProp.Value,
                                VC::RelationVCardProperty vcProp => ((IEnumerable<VC::TextProperty>?)vcProp.Value?.DisplayNames)?
                                                                                                     .Where(x => !x.IsEmpty)
                                                                                                     .OrderBy(x => x.Parameters.Preference)
                                                                                                     .FirstOrDefault()?.Value,
                                _ => null,
                            };

                            if (spouseName != null)
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
                            Debug.Assert(property.Value is IEnumerable<VC::AddressProperty>);
                            Debug.Assert(((IEnumerable<VC::AddressProperty>)property.Value).All(x => x != null));

                            var vcardAddresses = (IEnumerable<VC::AddressProperty>)property.Value;

                            string separator = " ";

                            foreach (VC::AddressProperty vcardAddress in vcardAddresses
                                                                          .Where(x => x.Value != null)
                                                                          .OrderByDescending(x => x.Parameters.Preference))
                            {
                                if (vcardAddress.Parameters.PropertyClass.IsSet(VC::Enums.PropertyClassTypes.Work))
                                {
                                    Work work = contact.Work ?? new Work();
                                    contact.Work = work;

                                    work.AddressWork = new Address()
                                    {
                                        Street = string.Join(separator, vcardAddress.Value.Street),
                                        City = string.Join(separator, vcardAddress.Value.Locality),
                                        PostalCode = string.Join(separator, vcardAddress.Value.PostalCode),
                                        State = string.Join(separator, vcardAddress.Value.Region),
                                        Country = string.Join(separator, vcardAddress.Value.Country),
                                    };
                                }
                                else
                                {
                                    contact.AddressHome = new Address()
                                    {
                                        Street = string.Join(separator, vcardAddress.Value.Street),
                                        City = string.Join(separator, vcardAddress.Value.Locality),
                                        PostalCode = string.Join(separator, vcardAddress.Value.PostalCode),
                                        State = string.Join(separator, vcardAddress.Value.Region),
                                        Country = string.Join(separator, vcardAddress.Value.Country),
                                    };
                                }
                            }

                            break;
                        }
                    case VC::Enums.VCdProp.PhoneNumbers:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            var vcardNumbers = (IEnumerable<VC::TextProperty>)property.Value;

                            contact.PhoneNumbers = vcardNumbers
                                .Where(x => x.Value != null)
                                .OrderBy(x => x.Parameters.Preference)
                                .Select(x => new PhoneNumber(x.Value,
                                                             x.Parameters.PropertyClass.IsSet(VC.Enums.PropertyClassTypes.Work),
                                                             x.Parameters.TelephoneType.IsSet(VC.Enums.TelTypes.Cell),
                                                             x.Parameters.TelephoneType.IsSet(VC.Enums.TelTypes.Fax)))
                                .ToArray();
                            break;
                        }
                    case VC::Enums.VCdProp.EmailAddresses:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            var vcardEmails = (IEnumerable<VC::TextProperty>)property.Value;

                            contact.EmailAddresses = vcardEmails
                                                     .Where(x => x.Value != null)
                                                     .OrderBy(x => x.Parameters.Preference)
                                                     .Select(x => x.Value)
                                                     .ToArray();
                            break;
                        }
                    case VC::Enums.VCdProp.URLs:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            var vcardUrls = (IEnumerable<VC::TextProperty>)property.Value;

                            foreach (VC::TextProperty url in vcardUrls
                                                              .Where(x => x.Value != null)
                                                              .OrderByDescending(x => x.Parameters.Preference))
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
                            break;
                        }
                    case VC::Enums.VCdProp.InstantMessengerHandles:
                        {
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            var vcardIMPPs = (IEnumerable<VC::TextProperty>)property.Value;

                            var impps = vcardIMPPs
                                        .Where(x => x.Value != null)
                                        .OrderBy(x => x.Parameters.Preference)
                                        .Select(x => x.Value)
                                        .ToArray();
                            contact.InstantMessengerHandles = impps;
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
                            Debug.Assert(property.Value is IEnumerable<VC::TextProperty>);
                            Debug.Assert(((IEnumerable<VC::TextProperty>)property.Value).All(x => x != null));

                            contact.Comment = ((IEnumerable<VC::TextProperty>)property.Value)
                                              .FirstOrDefault(x => x.Value != null)?.Value;
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