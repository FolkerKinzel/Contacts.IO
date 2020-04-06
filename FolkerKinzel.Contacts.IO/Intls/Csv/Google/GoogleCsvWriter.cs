using FolkerKinzel.CsvTools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using FolkerKinzel.CsvTools.Helpers.Converters;
using FolkerKinzel.CsvTools.Helpers.Converters.Specialized;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal class GoogleCsvWriter : CsvWriter
    {
        internal GoogleCsvWriter(Encoding? textEncoding) : base(null, textEncoding) { }


        protected override ICsvTypeConverter InitNullableDateConverter() => new DateTimeConverter("yyyy-MM-dd", true);


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            var mapping = HeaderRow.GetMapping();

            mapping.Add(

            // Dummy-Property, die am Ende von <see cref="CsvRecordWrapper"/> eingefügt wird, um beim Lesen von CSV am Ende der Initialisierung von <see cref="Contact"/> 
            // AddressHome und AddressWork sowie HomePagePersonal und HomePageWork ggf. zu vertauschen:
            new Tuple<string, ContactProp?, IList<string>>(nameof(AdditionalProp.Swap), (ContactProp)AdditionalProp.Swap, EmptyStringArray));

            return mapping;
        }

        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            Debug.Assert(tpl.Item2.HasValue);

            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            this.StringConverter));
        }


        protected override void FillCsvRecordNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.Phone1Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 0);
                    break;
                case AdditionalProp.Phone1Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 0);
                    break;
                case AdditionalProp.Phone2Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 1);
                    break;
                case AdditionalProp.Phone2Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 1);
                    break;
                case AdditionalProp.Phone3Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 2);
                    break;
                case AdditionalProp.Phone3Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 2);
                    break;
                case AdditionalProp.Phone4Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 3);
                    break;
                case AdditionalProp.Phone4Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 3);
                    break;
                case AdditionalProp.Phone5Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 4);
                    break;
                case AdditionalProp.Phone5Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 4);
                    break;
                case AdditionalProp.Phone6Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 5);
                    break;
                case AdditionalProp.Phone6Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 5);
                    break;
                case AdditionalProp.Phone7Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 6);
                    break;
                case AdditionalProp.Phone7Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 6);
                    break;
                case AdditionalProp.Phone8Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 7);
                    break;
                case AdditionalProp.Phone8Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 7);
                    break;
                case AdditionalProp.Phone9Type:
                    wrapper[index] = GetTelephoneTypeFromPhone(contact.PhoneNumbers, 8);
                    break;
                case AdditionalProp.Phone9Value:
                    wrapper[index] = GetValueFromPhone(contact.PhoneNumbers, 8);
                    break;
                case AdditionalProp.AddressHomeType:
                    wrapper[index] = contact.AddressHome is null ? null : PropertyClassType.Home;
                    break;
                case AdditionalProp.AddressWorkType:
                    wrapper[index] = contact.Work?.AddressWork is null ? null : PropertyClassType.Work;
                    break;
                case AdditionalProp.RelationType:
                    if (contact.Person?.Spouse != null)
                    {
                        wrapper[index] = RelationType.Spouse;
                    }
                    break;
                case AdditionalProp.WebHomeType:
                    wrapper[index] = PropertyClassType.Home;
                    break;
                case AdditionalProp.WebWorkType:
                    wrapper[index] = PropertyClassType.Work;
                    break;
                case AdditionalProp.EventType:
                    {
                        var anniversary = contact.Person?.Anniversary;

                        if (anniversary.HasValue)
                        {
                            wrapper[index] = EventType.Anniversary;
                        }
                    }
                    break;
                case AdditionalProp.InstantMessenger1Type:
                    wrapper[index] = contact.InstantMessengerHandles?.FirstOrDefault() ?? PropertyClassType.Other;
                    break;
                case AdditionalProp.InstantMessenger1Service:
                    wrapper[index] = GetIMService(contact.InstantMessengerHandles?.FirstOrDefault());
                    break;
                case AdditionalProp.InstantMessenger2Type:
                    wrapper[index] = contact.InstantMessengerHandles?.ElementAtOrDefault(1) is null ? null : PropertyClassType.Other;
                    break;
                case AdditionalProp.InstantMessenger2Service:
                    wrapper[index] = GetIMService(contact.InstantMessengerHandles?.ElementAtOrDefault(1));
                    break;
                case AdditionalProp.Swap:
                    // Dummy-Property ohne eigene Daten. Dient dazu AM ENDE der Initialsierung von CSVRecordWrapper
                    // AddressWork AddressHome zuzuweisen, wenn AddressHome null war. (Google unterscheidet nur zwischen
                    // Address 1 und Address 2 und weist diesen den Typ [Home | Work] explizit zu.)
                    // Das gleiche gilt für HomePagePersonal und HomePageWork 

                    if (contact.AddressHome is null)
                    {
                        var adrWorkType = wrapper[nameof(ColumnName.AddressWorkType)];

                        if (adrWorkType != null)
                        {
                            wrapper[nameof(ColumnName.AddressHomeType)] = adrWorkType;
                            wrapper[nameof(ColumnName.AddressWorkType)] = null;

                            wrapper[nameof(ColumnName.AddressHomeStreet)] = wrapper[nameof(ColumnName.AddressWorkStreet)];
                            wrapper[nameof(ColumnName.AddressWorkStreet)] = null;

                            wrapper[nameof(ColumnName.AddressHomeCity)] = wrapper[nameof(ColumnName.AddressWorkCity)];
                            wrapper[nameof(ColumnName.AddressWorkCity)] = null;

                            wrapper[nameof(ColumnName.AddressHomePostalCode)] = wrapper[nameof(ColumnName.AddressWorkPostalCode)];
                            wrapper[nameof(ColumnName.AddressWorkPostalCode)] = null;

                            wrapper[nameof(ColumnName.AddressHomeState)] = wrapper[nameof(ColumnName.AddressWorkState)];
                            wrapper[nameof(ColumnName.AddressWorkState)] = null;

                            wrapper[nameof(ColumnName.AddressHomeCountry)] = wrapper[nameof(ColumnName.AddressWorkCountry)];
                            wrapper[nameof(ColumnName.AddressWorkCountry)] = null;
                        }
                    }

                    if (contact.WebPagePersonal is null)
                    {
                        var webWorkType = wrapper[nameof(ColumnName.WebWorkType)];

                        if(webWorkType != null)
                        {
                            wrapper[nameof(ColumnName.WebHomeType)] = webWorkType;
                            wrapper[nameof(ColumnName.WebWorkType)] = null;

                            wrapper[nameof(ColumnName.WebHomeValue)] = wrapper[nameof(ColumnName.WebWorkValue)];
                            wrapper[nameof(ColumnName.WebWorkValue)] = null;
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        private static string? GetTelephoneTypeFromPhone(IEnumerable<PhoneNumber?>? phones, int index)
        {
            var phone = phones?.ElementAtOrDefault(index);

            if (phone is null) return null;

            if (phone.IsFax)
            {
                return phone.IsWork ? TelephoneTypes.FaxWork : TelephoneTypes.FaxHome;
            }

            if (phone.IsMobile)
            {
                return TelephoneTypes.Cell;
            }

            if (phone.IsWork)
            {
                return TelephoneTypes.Work;
            }

            if (index == 0)
            {
                return TelephoneTypes.Home;
            }

            return TelephoneTypes.Other;
        }


        private static string? GetValueFromPhone(IEnumerable<PhoneNumber?>? phones, int index)
        {
            var phone = phones?.ElementAtOrDefault(index);

            return phone?.Value;
        }

        private static string? GetIMService(string? val)
        {
            if (val is null)
            {
                return null;
            }

            if (val.StartsWith("aim:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Aim;
            }
            //else if (val.StartsWith("gg:", StringComparison.OrdinalIgnoreCase))
            //{
            //    this.BuildProperty(VCard.PropKeys.NonStandard.InstantMessenger.X_GADUGADU, prop, i == 0 && prop.Parameters.Preference < 100);
            //}
            else if (val.StartsWith("gtalk:", StringComparison.OrdinalIgnoreCase) || val.StartsWith("com.google.hangouts:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.GoogleTalk;
            }
            else if (val.StartsWith("icq:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Icq;
            }
            else if (val.StartsWith("xmpp:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Jabber;
            }
            else if (val.StartsWith("msnim:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Msn;
            }
            //else if (val.StartsWith("sip:", StringComparison.OrdinalIgnoreCase))
            //{
            //    this.BuildProperty(VCard.PropKeys.NonStandard.InstantMessenger.X_MS_IMADDRESS, prop, i == 0 && prop.Parameters.Preference < 100);
            //}
            else if (val.StartsWith("skype:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Skype;
            }
            //else if (val.StartsWith("twitter:", StringComparison.OrdinalIgnoreCase))
            //{
            //    this.BuildProperty(VCard.PropKeys.NonStandard.InstantMessenger.X_TWITTER, prop, i == 0 && prop.Parameters.Preference < 100);
            //}
            else if (val.StartsWith("ymsgr:", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Yahoo;
            }
#if NET40
            else if(val.ToUpperInvariant().Contains("QQ"))
            {
                return IMType.Qq;
            }
#else
            else if (val.Contains("qq", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Qq;
            }
#endif
            return null;
        }
    }
}
