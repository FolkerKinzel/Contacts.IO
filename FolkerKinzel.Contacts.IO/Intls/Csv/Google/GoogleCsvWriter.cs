using FolkerKinzel.CsvTools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{

    internal class GoogleCsvWriter : CsvWriter
    {
        internal GoogleCsvWriter(Encoding? textEncoding) : base(textEncoding) { }


        protected override string[] CreateColumnNames() => HeaderRow.GetColumnNamesEn();


        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping() => HeaderRow.GetMappingEN();


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
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if(adrWork != null)
                        {
                            index = contact.AddressHome is null ?  wrapper.IndexOf(nameof(ColumnName.AddressHomeType)) : index;
                            wrapper[index] = PropertyClassType.Work;
                        }
                    }
                    break;
                case AdditionalProp.AddressWorkStreet:
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if (adrWork != null)
                        {
                            index = contact.AddressHome is null ? wrapper.IndexOf(nameof(ColumnName.AddressHomeStreet)) : index;
                            wrapper[index] = adrWork.Street;
                        }
                    }
                    break;
                case AdditionalProp.AddressWorkPostalCode:
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if (adrWork != null)
                        {
                            index = contact.AddressHome is null ? wrapper.IndexOf(nameof(ColumnName.AddressHomePostalCode)) : index;
                            wrapper[index] = adrWork.PostalCode;
                        }
                    }
                    break;
                case AdditionalProp.AddressWorkCity:
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if (adrWork != null)
                        {
                            index = contact.AddressHome is null ? wrapper.IndexOf(nameof(ColumnName.AddressHomeCity)) : index;
                            wrapper[index] = adrWork.City;
                        }
                    }
                    break;
                case AdditionalProp.AddressWorkState:
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if (adrWork != null)
                        {
                            index = contact.AddressHome is null ? wrapper.IndexOf(nameof(ColumnName.AddressHomeState)) : index;
                            wrapper[index] = adrWork.State;
                        }
                    }
                    break;
                case AdditionalProp.AddressWorkCountry:
                    {
                        var adrWork = contact.Work?.AddressWork;

                        if (adrWork != null)
                        {
                            index = contact.AddressHome is null ? wrapper.IndexOf(nameof(ColumnName.AddressHomeCountry)) : index;
                            wrapper[index] = adrWork.Country;
                        }
                    }
                    break;
                case AdditionalProp.RelationType:
                    var spouse = contact.Person?.Spouse;
                    if(spouse != null)
                    {
                        wrapper[index] = spouse;
                    }
                    break;
                case AdditionalProp.Website1Type:
                    {
                        var webHome = contact.HomePagePersonal;
                        if (webHome is null) return;

                        wrapper[index] = PropertyClassType.Home;
                    }
                    break;
                case AdditionalProp.Website1Value:
                    {
                        var webHome = contact.HomePagePersonal;
                        if (webHome is null) return;

                        wrapper[index] = webHome;
                    }
                    break;
                case AdditionalProp.Website2Type:
                    {
                        var webWork = contact.HomePageWork;
                        if (webWork is null) return;

                        int webHomeTypeIndex = wrapper.IndexOf(nameof(ColumnName.Website1Type));

                        Debug.Assert(webHomeTypeIndex >= 0);

                        if(wrapper[webHomeTypeIndex] is null)
                        {
                            index = webHomeTypeIndex;
                        }

                        wrapper[index] = PropertyClassType.Work;
                    }
                    break;
                case AdditionalProp.Website2Value:
                    {
                        var webWork = contact.HomePageWork;
                        if (webWork is null) return;

                        int webHomeValueIndex = wrapper.IndexOf(nameof(ColumnName.Website1Value));

                        Debug.Assert(webHomeValueIndex >= 0);

                        if (wrapper[webHomeValueIndex] is null)
                        {
                            index = webHomeValueIndex;
                        }

                        wrapper[index] = webWork;
                    }
                    break;
                case AdditionalProp.EventType:
                    {
                        var anniversary = contact.Person?.Anniversary;

                        if(anniversary.HasValue)
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

            if (phone.IsCell)
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
            else if (val.StartsWith("gtalk:", StringComparison.OrdinalIgnoreCase) ||  val.StartsWith("com.google.hangouts:", StringComparison.OrdinalIgnoreCase))
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
            else if(val.Contains("qq", StringComparison.OrdinalIgnoreCase))
            {
                return IMType.Qq;
            }
#endif
            return null;
        }
    }
}
