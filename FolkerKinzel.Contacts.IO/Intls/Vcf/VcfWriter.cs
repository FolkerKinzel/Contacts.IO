using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FolkerKinzel.VCards;
using FolkerKinzel.VCards.Extensions;
using VC = FolkerKinzel.VCards.Models;

namespace FolkerKinzel.Contacts.IO.Intls.Vcf
{
    internal static class VcfWriter
    {
        /// <summary>
        /// Schreibt den Inhalt eines <see cref="Contact"/>-Objekts in eine vCard-Datei.
        /// </summary>
        /// <param name="contact">Das zu serialisierende <see cref="Contact"/>-Objekt. Wenn <paramref name="contact"/>&#160;<c>null</c> ist
        /// oder keine Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="contact"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        internal static void Write(Contact contact, string fileName, VCardVersion version)
        {
            if(contact is null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            ToVCard(contact)?.Save(fileName, (VC::Enums.VCdVersion)version);
        }


        /// <summary>
        /// Schreibt den Inhalt einer Auflistung  von <see cref="Contact"/>-Objekten in eine gemeinsame 
        /// vCard-Datei.
        /// </summary>
        /// <param name="contacts">Auflistung der in eine gemeinsame vCard-Datei zu schreibenden <see cref="Contact"/>-Objekte.
        /// Die Auflistung darf leer sein oder <c>null</c>-Werte
        /// enthalten. Wenn die Auflistung kein <see cref="Contact"/>-Objekt enthält, das Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="contacts"/> oder <paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        /// <remarks><paramref name="contacts"/> darf nicht null sein, aber null-Werte enthalten.</remarks>
        public static void Write(IEnumerable<Contact?> contacts, string fileName, VCardVersion version)
        {
            if (contacts is null)
            {
                throw new ArgumentNullException(nameof(contacts));
            }

            VCard.Save(fileName, contacts.Select(x => ToVCard(x)).ToList(), (VC::Enums.VCdVersion)version);
        }



        private static VCard? ToVCard(Contact? contact)
        {
            if (contact is null)
            {
                return null;
            }

            contact.Clean();

            if (contact.IsEmpty)
            {
                return null;
            }

            var vcard = new VCard();
            bool writeAdrWork = true;
            Work? work = contact.Work;

            Address? adrHome = contact.AddressHome;

            if(adrHome != null)
            {
                var adrProp = new VC::AddressProperty(street: adrHome.Street,
                                                      locality: adrHome.City,
                                                      postalCode: adrHome.PostalCode,
                                                      region: adrHome.State,
                                                      country: adrHome.Country);

                var addresses = new List<VC::AddressProperty?>();
                vcard.Addresses = addresses;

                addresses.Add(adrProp);

                adrProp.Parameters.AddressType = VC::Enums.AddressTypes.Dom | VC::Enums.AddressTypes.Intl | VC::Enums.AddressTypes.Parcel | VC::Enums.AddressTypes.Postal;

                if (adrHome == work?.AddressWork)
                {
                    adrProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                    writeAdrWork = false;
                }
                else
                {
                    adrProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
                }

                adrProp.Parameters.Label = BuildAddressLabel(adrHome);
            }


            
            if(work != null)
            {
                Address? adrWork = work.AddressWork;

                if(writeAdrWork && adrWork != null)
                {
                    var adrProp = new VC::AddressProperty(street: adrWork.Street,
                                                          locality: adrWork.City,
                                                          postalCode: adrWork.PostalCode,
                                                          region: adrWork.State,
                                                          country: adrWork.Country);

                    List<VC::AddressProperty?> addresses = (List<VC::AddressProperty?>?)vcard.Addresses ?? new List<VC::AddressProperty?>();
                    vcard.Addresses = addresses;

                    addresses.Add(adrProp);
                    adrProp.Parameters.AddressType = VC::Enums.AddressTypes.Dom | VC::Enums.AddressTypes.Intl | VC::Enums.AddressTypes.Parcel | VC::Enums.AddressTypes.Postal;
                    adrProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;

                    adrProp.Parameters.Label = BuildAddressLabel(adrWork);
                }

                if(work.Company != null || work.Department != null || work.Office != null)
                {
                    var orgProp = new VC::OrganizationProperty(work.Company, new string?[] { work.Department, work.Office });
                    var orgs = new List<VC::OrganizationProperty>();
                    vcard.Organizations = orgs;
                    orgs.Add(orgProp);
                }

                if(work.JobTitle != null)
                {
                    var titles = new List<VC::TextProperty>();
                    vcard.Titles = titles;

                    var titleProp = new VC::TextProperty(work.JobTitle);

                    titles.Add(titleProp);
                }
            }

            var comment = contact.Comment;
            if(comment != null)
            {
                var notes = new List<VC::TextProperty>();
                vcard.Notes = notes;
                notes.Add(new VC::TextProperty(comment));
            }

            var displayName = contact.DisplayName;
            if(displayName != null)
            {
                var dispNames = new List<VC::TextProperty>();
                vcard.DisplayNames = dispNames;

                dispNames.Add(new VC::TextProperty(displayName));
            }

            IEnumerable<string?>? emails = contact.EmailAddresses;
            if(emails != null)
            {
                var emailProps = new List<VC::TextProperty>();
                vcard.EmailAddresses = emailProps;

                int counter = 1;

                foreach (var mailAddress in emails)
                {
                    var mailProp = new VC::TextProperty(mailAddress);
                    emailProps.Add(mailProp);

                    mailProp.Parameters.EmailType = VC::Enums.EmailType.SMTP;
                    mailProp.Parameters.Preference = counter++;
                }
            }

            var webPersonal = contact.WebPagePersonal;
            var webWork = contact.WebPageWork;
            bool writeWebWork = true;

            if(webPersonal != null)
            {
                var urls = new List<VC::TextProperty?>();
                vcard.URLs = urls;

                var urlHomeProp = new VC::TextProperty(webPersonal);
                urls.Add(urlHomeProp);

                if(webPersonal == webWork)
                {
                    urlHomeProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                    writeWebWork = false;
                }
                else
                {
                    urlHomeProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
                }
            }

            if(writeWebWork && webWork != null)
            {
                List<VC.TextProperty?>? urls = (List<VC::TextProperty?>?)vcard.URLs ?? new List<VC::TextProperty?>();
                vcard.URLs = urls;

                var urlWorkProp = new VC::TextProperty(webWork);
                urls.Add(urlWorkProp);

                urlWorkProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;
            }

            IEnumerable<string?>? impps = contact.InstantMessengerHandles;
            if (impps != null)
            {
                var imppProps = new List<VC::TextProperty>();
                vcard.InstantMessengerHandles = imppProps;

                int counter = 1;

                foreach (var imppAddress in impps)
                {
                    var imppProp = new VC::TextProperty(imppAddress);
                    imppProps.Add(imppProp);

                    imppProp.Parameters.Preference = counter++;
                }
            }

            Person? person = contact.Person;
            if (person != null)
            {
                DateTime? bday = person.BirthDay;
                if (bday.HasValue)
                {
                    var bdays = new List<VC::DateTimeProperty>();
                    vcard.BirthDayViews = bdays;
                    var bdayProp = new VC::DateTimeOffsetProperty(bday.Value);
                    bdays.Add(bdayProp);
                }

                Name? name = person.Name;
                if (name != null)
                {
                    var names = new List<VC::NameProperty>();
                    vcard.NameViews = names;
                    names.Add(new VC::NameProperty(name.LastName, name.FirstName, name.MiddleName, name.Prefix, name.Suffix));
                }

                Sex gender = person.Gender;
                if(gender != Sex.Unspecified)
                {
                    var genders = new List<VC::GenderProperty>();
                    vcard.GenderViews = genders;
                    genders.Add(new VC::GenderProperty(gender == Sex.Female ? VC::Enums.VCdSex.Female : VC::Enums.VCdSex.Male));
                }

                var nickName = person.NickName;
                if(nickName != null)
                {
                    var nickNames = new List<VC::StringCollectionProperty>();
                    vcard.NickNames = nickNames;
                    nickNames.Add(new VC::StringCollectionProperty(nickName));
                }

                var spouseName = person.Spouse;
                if(spouseName != null)
                {
                    var spouseNames = new List<VC::RelationProperty>();
                    vcard.Relations = spouseNames;

                    var spouseProp = new VC::RelationTextProperty(spouseName, VC::Enums.RelationTypes.Spouse);
                    spouseNames.Add(spouseProp);
                }

                DateTime? anniversary = person.Anniversary;
                if(anniversary.HasValue)
                {
                    var anniversaries = new List<VC::DateTimeProperty>();
                    vcard.AnniversaryViews = anniversaries;

                    var anniversaryProp = new VC::DateTimeOffsetProperty(anniversary.Value);
                    anniversaries.Add(anniversaryProp);
                }
            }

            IEnumerable<PhoneNumber?>? phones = contact.PhoneNumbers;
            if(phones != null)
            {
                var phoneProps = new List<VC::TextProperty>();
                vcard.PhoneNumbers = phoneProps;
                VC::TextProperty phoneProp;

                foreach (PhoneNumber? number in phones)
                {
                    phoneProp = new VC::TextProperty(number!.Value);
                    phoneProps.Add(phoneProp);

                    VC::Enums.TelTypes? telType = null;

                    if(number.IsMobile)
                    {
                        telType = VC::Enums.TelTypes.Cell;
                    }

                    if(number.IsFax)
                    {
                        telType = telType.Set(VC.Enums.TelTypes.Fax);
                    }

                    phoneProp.Parameters.TelephoneType = telType;

                    if(number.IsWork)
                    {
                        phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;
                    }
                }
            }
            
            vcard.TimeStamp = contact.TimeStamp == default ? null : new VC::TimeStampProperty(contact.TimeStamp);

            return vcard;
        }


        private static string BuildAddressLabel(Address adr)
        {
            var builder = new StringBuilder();

            _ = builder.Append(adr.Street).Append(VCard.NewLine)
                   .Append(adr.PostalCode).Append(' ').Append(adr.City);

            return builder.ToString();
        }
    }
}