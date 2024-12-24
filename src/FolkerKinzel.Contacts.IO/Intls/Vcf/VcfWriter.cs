using System.Text;
using FolkerKinzel.VCards;
using FolkerKinzel.VCards.Extensions;
using VC = FolkerKinzel.VCards.Models;

namespace FolkerKinzel.Contacts.IO.Intls.Vcf;

internal static class VcfWriter
{
    /// <summary> Schreibt den Inhalt eines <see cref="Contact" />-Objekts in eine vCard-Datei.
    /// </summary>
    /// <param name="contact">Das zu serialisierende <see cref="Contact" />-Objekt.
    /// Wenn <paramref name="contact" />&#160;<c>null</c> ist oder keine Daten enthält,
    /// wird keine Datei geschrieben.</param>
    /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei.
    /// Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
    /// <exception cref="ArgumentNullException"> <paramref name="contact" /> or <paramref
    /// name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException"> <paramref name="fileName" /> is not a valid
    /// file path.</exception>
    /// <exception cref="IOException">The file could not be written.</exception>
    internal static void Write(Contact contact, string fileName, VCardVersion version)
    {
        if (contact is null)
        {
            throw new ArgumentNullException(nameof(contact));
        }

        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        ToVCard(contact)?.SaveVcf(fileName, (VC::Enums.VCdVersion)version);
    }


    /// <summary> Schreibt den Inhalt einer Auflistung von <see cref="Contact" />-Objekten
    /// in eine gemeinsame vCard-Datei. </summary>
    /// <param name="contacts">Auflistung der in eine gemeinsame vCard-Datei zu schreibenden
    /// <see cref="Contact" />-Objekte. Die Auflistung darf leer sein oder <c>null</c>-Werte
    /// enthalten. Wenn die Auflistung kein <see cref="Contact" />-Objekt enthält, das
    /// Daten enthält, wird keine Datei geschrieben.</param>
    /// <param name="fileName">Der vollständige Pfad der zu erzeugenden vCard-Datei.
    /// Existiert die Datei schon, wird sie überschrieben.</param>
    /// <param name="version">Dateiversion der zu speichernden vCard. (optional)</param>
    /// <exception cref="ArgumentNullException"> <paramref name="contacts" /> or <paramref
    /// name="fileName" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException"> <paramref name="fileName" /> is not a valid
    /// file path.</exception>
    /// <exception cref="IOException">The file could not be written.</exception>
    /// <remarks> <paramref name="contacts" /> darf nicht null sein, aber null-Werte
    /// enthalten.</remarks>
    internal static void Write(IEnumerable<Contact?> contacts, string fileName, VCardVersion version)
    {
        if (contacts is null)
        {
            throw new ArgumentNullException(nameof(contacts));
        }

        VCard.SaveVcf(fileName, contacts.Select(x => ToVCard(x)), (VC::Enums.VCdVersion)version);
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

        if (adrHome != null)
        {
            var homeAdr = new VC::AddressProperty(street: adrHome.Street,
                                                  locality: adrHome.City,
                                                  postalCode: adrHome.PostalCode,
                                                  region: adrHome.State,
                                                  country: adrHome.Country);

            vcard.Addresses = homeAdr;

            homeAdr.Parameters.AddressType = VC::Enums.AddressTypes.Dom | VC::Enums.AddressTypes.Intl | VC::Enums.AddressTypes.Parcel | VC::Enums.AddressTypes.Postal;

            if (adrHome == work?.AddressWork)
            {
                homeAdr.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                writeAdrWork = false;
            }
            else
            {
                homeAdr.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
            }

            homeAdr.Parameters.Label = BuildAddressLabel(adrHome);
        }

        if (work != null)
        {
            Address? adrWork = work.AddressWork;

            if (writeAdrWork && adrWork != null)
            {
                var workAdr = new VC::AddressProperty(street: adrWork.Street,
                                                      locality: adrWork.City,
                                                      postalCode: adrWork.PostalCode,
                                                      region: adrWork.State,
                                                      country: adrWork.Country);

                IEnumerable<VC::AddressProperty?>? addresses = vcard.Addresses;

                if (addresses is null)
                {
                    vcard.Addresses = workAdr;
                }
                else if (addresses is VC::AddressProperty homeAdr)
                {
                    vcard.Addresses = new VC::AddressProperty[] { homeAdr, workAdr };
                }

                workAdr.Parameters.AddressType = VC::Enums.AddressTypes.Dom | VC::Enums.AddressTypes.Intl | VC::Enums.AddressTypes.Parcel | VC::Enums.AddressTypes.Postal;
                workAdr.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;

                workAdr.Parameters.Label = BuildAddressLabel(adrWork);
            }

            if (work.Company != null || work.Department != null || work.Office != null)
            {
                vcard.Organizations =
                    new VC::OrganizationProperty(work.Company,
                                                 new string?[] { work.Department, work.Office });
            }

            if (work.JobTitle != null)
            {
                vcard.Titles = new VC::TextProperty(work.JobTitle);
            }
        }

        var comment = contact.Comment;
        if (comment != null)
        {
            vcard.Notes = new VC::TextProperty(comment);
        }

        var displayName = contact.DisplayName;
        if (displayName != null)
        {
            vcard.DisplayNames = new VC::TextProperty(displayName);
        }

        IEnumerable<string?>? emails = contact.EmailAddresses;
        if (emails != null)
        {
            var emailProps = new List<VC::TextProperty>();
            vcard.EmailAddresses = emailProps;

            int counter = 1;

            foreach (var mailAddress in emails)
            {
                Debug.Assert(mailAddress != null);

                var mailProp = new VC::TextProperty(mailAddress);
                emailProps.Add(mailProp);

                mailProp.Parameters.EmailType = VC::Enums.EmailType.SMTP;
                mailProp.Parameters.Preference = counter++;
            }
        }

        var webPersonal = contact.WebPagePersonal;
        var webWork = contact.WebPageWork;
        bool writeWebWork = true;

        if (webPersonal != null)
        {
            var urlHomeProp = new VC::TextProperty(webPersonal);
            vcard.URLs = urlHomeProp;

            if (webPersonal == webWork)
            {
                urlHomeProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home | VC::Enums.PropertyClassTypes.Work;
                writeWebWork = false;
            }
            else
            {
                urlHomeProp.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Home;
            }
        }

        if (writeWebWork && webWork != null)
        {
            var urlWork = new VC::TextProperty(webWork);
            urlWork.Parameters.PropertyClass = VC::Enums.PropertyClassTypes.Work;

            IEnumerable<VC::TextProperty?>? urls = vcard.URLs;

            if (urls is null)
            {
                vcard.URLs = urlWork;
            }
            else if (urls is VC::TextProperty urlHome)
            {
                vcard.URLs = new VC::TextProperty[] { urlHome, urlWork };
            }
        }

        IEnumerable<string?>? impps = contact.InstantMessengerHandles;
        if (impps != null)
        {
            var imppProps = new List<VC::TextProperty>();
            vcard.InstantMessengerHandles = imppProps;

            int counter = 1;

            foreach (var imppAddress in impps)
            {
                Debug.Assert(imppAddress != null);

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
                vcard.BirthDayViews = new VC::DateTimeOffsetProperty(bday.Value);
            }

            Name? name = person.Name;
            if (name != null)
            {
                vcard.NameViews = new VC::NameProperty(name.LastName,
                                                       name.FirstName,
                                                       name.MiddleName,
                                                       name.Prefix,
                                                       name.Suffix);
            }

            Sex gender = person.Gender;
            if (gender != Sex.Unspecified)
            {
                vcard.GenderViews = new VC::GenderProperty(
                    gender == Sex.Female ? VC::Enums.VCdSex.Female : VC::Enums.VCdSex.Male);
            }

            var nickName = person.NickName;
            if (nickName != null)
            {
                vcard.NickNames = new VC::StringCollectionProperty(nickName);
            }

            var spouseName = person.Spouse;
            if (spouseName != null)
            {
                vcard.Relations = new VC::RelationTextProperty(
                    spouseName, VC::Enums.RelationTypes.Spouse);
            }

            DateTime? anniversary = person.Anniversary;
            if (anniversary.HasValue)
            {
                vcard.AnniversaryViews = new VC::DateTimeOffsetProperty(anniversary.Value);
            }
        }

        IEnumerable<PhoneNumber?>? phones = contact.PhoneNumbers;
        if (phones != null)
        {
            var phoneProps = new List<VC::TextProperty>();
            vcard.PhoneNumbers = phoneProps;

            foreach (PhoneNumber? number in phones)
            {
                Debug.Assert(number != null);

                var phoneProp = new VC::TextProperty(number!.Value);
                phoneProps.Add(phoneProp);

                VC::Enums.TelTypes? telType = null;

                if (number.IsMobile)
                {
                    telType = VC::Enums.TelTypes.Cell;
                }

                if (number.IsFax)
                {
                    telType = telType.Set(VC.Enums.TelTypes.Fax);
                }

                phoneProp.Parameters.TelephoneType = telType;

                if (number.IsWork)
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
