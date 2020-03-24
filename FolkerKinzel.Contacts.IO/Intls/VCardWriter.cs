using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FolkerKinzel.VCards;
using FolkerKinzel.VCards.Models.Helpers;
using VC = FolkerKinzel.VCards.Models;

namespace FolkerKinzel.Contacts.IO.Intls
{
    internal static class VCardWriter
    {
        /// <summary>
        /// Schreibt den Inhalt eines <see cref="Contact"/>-Objekts in eine vCard-Datei.
        /// </summary>
        /// <param name="contact">Das zu serialisierende <see cref="Contact"/>-Objekt. Wenn <paramref name="contact"/>&#160;<c>null</c> ist
        /// oder keine Daten enthält, wird keine Datei geschrieben.</param>
        /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei. 
        /// Existiert die Datei schon, wird sie überschrieben.</param>
        /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> ist <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="fileName"/> ist kein gültiger Dateipfad.</exception>
        /// <exception cref="IOException">Die Datei konnte nicht geschrieben werden.</exception>
        internal static void Write(Contact? contact, string fileName, VCardVersion version)
        {
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
            if (contacts is null) throw new ArgumentNullException(nameof(contacts));

            VCard.Save(fileName, contacts.Select(x => ToVCard(x)).ToList(), (VC::Enums.VCdVersion)version);
        }



        private static VCard? ToVCard(Contact? contact)
        {
            if (contact is null) return null;
            contact.Clean();
            if (contact.IsEmpty) return null;

            var vcard = new VCard();
            bool writeAdrWork = true;
            var work = contact.Work;

            var adrHome = contact.AddressHome;

            if(adrHome != null)
            {
                var adrProp = new VC::AddressProperty(
                    null,
                    null,
                    adrHome.Street,
                    adrHome.City,
                    adrHome.State,
                    adrHome.PostalCode,
                    adrHome.Country);

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
                var adrWork = work.AddressWork;

                if(writeAdrWork && adrWork != null)
                {
                    var adrProp = new VC::AddressProperty(
                    null,
                    null,
                    adrWork.Street,
                    adrWork.City,
                    adrWork.State,
                    adrWork.PostalCode,
                    adrWork.Country);

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

                if(work.Position != null)
                {
                    var titles = new List<VC::TextProperty>();
                    vcard.Titles = titles;

                    var titleProp = new VC::TextProperty(work.Position);

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

            var emails = contact.EmailAddresses;
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

            var webPersonal = contact.HomePagePersonal;
            var webWork = contact.HomePageWork;
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
                var urls = (List<VC::TextProperty?>?)vcard.URLs ?? new List<VC::TextProperty?>();
                vcard.URLs = urls;

                var urlWorkProp = new VC::TextProperty(webWork);
                urls.Add(urlWorkProp);

                urlWorkProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;
            }

            var impps = contact.InstantMessengerHandles;
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

            var person = contact.Person;
            if (person != null)
            {
                var bday = person.BirthDay;
                if (bday.HasValue)
                {
                    var bdays = new List<VC::DateTimeProperty>();
                    vcard.BirthDayViews = bdays;
                    var bdayProp = new VC::DateTimeOffsetProperty(bday.Value);
                    bdays.Add(bdayProp);
                }

                var name = person.Name;
                if (name != null)
                {
                    var names = new List<VC::NameProperty>();
                    vcard.NameViews = names;
                    names.Add(new VC::NameProperty(name.LastName, name.FirstName, name.MiddleName, name.Prefix, name.Suffix));
                }

                var gender = person.Gender;
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

                    var spouseProp = new VC::RelationTextProperty(spouseName);
                    spouseNames.Add(spouseProp);

                    spouseProp.Parameters.RelationType = VC::Enums.RelationTypes.Spouse;
                }

                var anniversary = person.Anniversary;
                if(anniversary.HasValue)
                {
                    var anniversaries = new List<VC::DateTimeProperty>();
                    vcard.AnniversaryViews = anniversaries;

                    var anniversaryProp = new VC::DateTimeOffsetProperty(anniversary.Value);
                    anniversaries.Add(anniversaryProp);
                }
            }

            var phones = contact.PhoneNumbers;
            if(phones != null)
            {
                var phoneProps = new List<VC::TextProperty>();
                vcard.PhoneNumbers = phoneProps;
                VC::TextProperty phoneProp;

                foreach (var number in phones)
                {
                    phoneProp = new VC::TextProperty(number!.Value);
                    phoneProps.Add(phoneProp);

                    VC::Enums.TelTypes? telType = null;

                    if(number.IsCell)
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

                

                //string? fax = phones.Fax;
                //string? faxWork = phones.FaxWork;
                //string? phone = phones.Phone;
                //string? phoneWork = phones.PhoneWork;
                //bool writeWork = true;
                //bool writePhone = true;
                //bool writePhoneWork = true;

                //if(fax != null)
                //{
                //    phoneProp = new VC::TextProperty(fax);
                //    phoneProps.Add(phoneProp);

                //    if(fax == faxWork)
                //    {
                //        phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                //        writeWork = false;
                //    }
                //    else
                //    {
                //        phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
                //    }

                //    if (fax == phone)
                //    {
                //        phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Voice | VC::Enums.TelTypes.Fax;
                //        writePhone = false;
                //    }
                //    else
                //    {
                //        phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Fax;
                //    }
                //}

                //if(writeWork && faxWork != null)
                //{
                //    phoneProp = new VC::TextProperty(faxWork);
                //    phoneProps.Add(phoneProp);

                //    phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;

                //    if (faxWork == phoneWork)
                //    {
                //        phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Voice | VC::Enums.TelTypes.Fax;
                //        writePhoneWork = false;
                //    }
                //    else
                //    {
                //        phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Fax;
                //    }
                //}

                //if(writePhone && phone != null)
                //{
                //    phoneProp = new VC::TextProperty(phone);
                //    phoneProps.Add(phoneProp);

                //    if (phone == phoneWork)
                //    {
                //        phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                //        writePhoneWork = false;
                //    }
                //    else
                //    {
                //        phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
                //    }

                //    phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Voice;
                //}


                //if(writePhoneWork && phoneWork != null)
                //{
                //    phoneProp = new VC::TextProperty(phoneWork);
                //    phoneProps.Add(phoneProp);

                //    phoneProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;
                //    phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Voice;
                //}


                //phone = phones.MobilePhone;

                //if(phone != null)
                //{
                //    phoneProp = new VC::TextProperty(phone);
                //    phoneProps.Add(phoneProp);

                //    phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Voice | VC::Enums.TelTypes.Cell;
                //}

                //phone = phones.OtherPhone;

                //if (phone != null)
                //{
                //    phoneProp = new VC::TextProperty(phone);
                //    phoneProps.Add(phoneProp);
                //}


                //phone = phones.Pager;

                //if (phone != null)
                //{
                //    phoneProp = new VC::TextProperty(phone);
                //    phoneProps.Add(phoneProp);

                //    phoneProp.Parameters.TelephoneType = VC::Enums.TelTypes.Pager;
                //}
            }

          
            
            vcard.LastRevision = new VC::TimestampProperty(contact.TimeStamp);
            

            return vcard;
        }


        private static string BuildAddressLabel(Address adr)
        {
            var builder = new StringBuilder();

            builder.Append(adr.Street).Append(VCard.NewLine)
                   .Append(adr.PostalCode).Append(" ").Append(adr.City);

            return builder.ToString();
        }
    }
}