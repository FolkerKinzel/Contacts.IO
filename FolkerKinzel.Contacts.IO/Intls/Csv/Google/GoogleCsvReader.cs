﻿using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{
    internal class GoogleCsvReader : CsvReader
    {
        internal GoogleCsvReader(Encoding? textEncoding) : base(textEncoding) { }

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            var mapping = HeaderRow.GetMapping();

            mapping.Add(

            // Dummy-Property, die am Ende von <see cref="CsvRecordWrapper"/> eingefügt wird, um beim Lesen von CSV am Ende der Initialisierung von <see cref="Contact"/> 
            // AddressHome und AddressWork ggf. zu vertauschen:
            new Tuple<string, ContactProp?, IList<string>>(nameof(AdditionalProp.Swap), (ContactProp)AdditionalProp.Swap, EmptyStringArray));

            return mapping;
        }


        protected override void InitCsvRecordWrapperUndefinedValues(Tuple<string, ContactProp?, IList<string>> tpl, CsvRecordWrapper wrapper)
        {
            Debug.Assert(tpl.Item2.HasValue);


            if (tpl.Item2 == (ContactProp)AdditionalProp.Swap)
            {
                wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item1,
                                tpl.Item3,
                                CsvConverterFactory.CreateConverter(CsvTypeCode.String, false)));
            }
            else
            {
                wrapper.AddProperty(
                            new CsvProperty(
                                tpl.Item1,
                                tpl.Item3,
                                this.StringConverter));
            }
        }



        protected override void InitContactNonStandardProp(Contact contact, ContactProp prop, CsvRecordWrapper wrapper, int index)
        {
            var addProp = (AdditionalProp)prop;

            switch (addProp)
            {
                case AdditionalProp.Phone1Type:
                    {
                        var phoneNumbers = new List<PhoneNumber>();
                        contact.PhoneNumbers = phoneNumbers;

                        var newNumber = new PhoneNumber();
                        phoneNumbers.Add(newNumber);
                        SetTelephoneType(newNumber, (string?)wrapper[index]);
                    }
                    break;
                case AdditionalProp.Phone2Type:
                case AdditionalProp.Phone3Type:
                case AdditionalProp.Phone4Type:
                case AdditionalProp.Phone5Type:
                case AdditionalProp.Phone6Type:
                case AdditionalProp.Phone7Type:
                case AdditionalProp.Phone8Type:
                case AdditionalProp.Phone9Type:
                    {
                        var phoneNumbers = (List<PhoneNumber>?)contact.PhoneNumbers ?? new List<PhoneNumber>();
                        contact.PhoneNumbers = phoneNumbers;

                        if (phoneNumbers.Count == 0)
                        {
                            phoneNumbers.Add(new PhoneNumber());
                        }

#if NET40
                        var lastNumber = phoneNumbers[phoneNumbers.Count - 1];
#else
                        var lastNumber = phoneNumbers[^1];
#endif


                        if (lastNumber.IsEmpty)
                        {
                            SetTelephoneType(lastNumber, (string?)wrapper[index]);
                        }
                        else
                        {
                            var newNumber = new PhoneNumber();
                            phoneNumbers.Add(newNumber);
                            SetTelephoneType(newNumber, (string?)wrapper[index]);
                        }

                    }
                    break;
                case AdditionalProp.Phone1Value:
                case AdditionalProp.Phone2Value:
                case AdditionalProp.Phone3Value:
                case AdditionalProp.Phone4Value:
                case AdditionalProp.Phone5Value:
                case AdditionalProp.Phone6Value:
                case AdditionalProp.Phone7Value:
                case AdditionalProp.Phone8Value:
                case AdditionalProp.Phone9Value:
                    {
                        var phoneNumbers = (List<PhoneNumber>?)contact.PhoneNumbers ?? new List<PhoneNumber>();
                        contact.PhoneNumbers = phoneNumbers;

                        if (phoneNumbers.Count == 0)
                        {
                            phoneNumbers.Add(new PhoneNumber());
                        }

#if NET40
                        phoneNumbers[phoneNumbers.Count - 1].Value = (string?)wrapper[index];
#else
                        phoneNumbers[^1].Value = (string?)wrapper[index];
#endif
                    }
                    break;
                //case AdditionalProp.AddressHomeType:
                //    break;
                //case AdditionalProp.AddressWorkType:
                //    break;
                //case AdditionalProp.InstantMessenger1Type:
                //    break;
                //case AdditionalProp.InstantMessenger1Service:
                //    break;
                //case AdditionalProp.InstantMessenger2Type:
                //    break;
                //case AdditionalProp.InstantMessenger2Service:
                //    break;
                //case AdditionalProp.RelationType:
                //    break;
                //case AdditionalProp.WebHomeType:
                //    break;
                //case AdditionalProp.WebWorkType:
                //    break;
                //case AdditionalProp.EventType:
                //    break;
                case AdditionalProp.Swap:
                    {
                        // Swap Addresses:
                        Address? addrHome = contact.AddressHome;
                        Address? addrWork = contact.Work?.AddressWork;

#if NET40
                        if(((string?)wrapper[nameof(ColumnName.AddressHomeType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? false)

#else
                        if (((string?)wrapper[nameof(ColumnName.AddressHomeType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? false)
#endif
                        {
                            var work = contact.Work ?? new Work();
                            contact.Work = work;


                            work.AddressWork = addrHome;
                            contact.AddressHome = null;

#if NET40
                            if(!((string?)wrapper[nameof(ColumnName.AddressWorkType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.AddressWorkType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                contact.AddressHome = addrWork;
                            }
                        }

                        var person = contact.Person;

                        if (person != null)
                        {
                            // check RelationType
#if NET40
                            if(!((string?)wrapper[nameof(ColumnName.RelationType)])?.ToUpperInvariant().Contains(RelationType.SpouseUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.RelationType)])?.Contains(RelationType.Spouse, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                person.Spouse = null;
                            }

                            // check Anniversary
#if NET40
                            if(!((string?)wrapper[nameof(ColumnName.EventType)])?.ToUpperInvariant().Contains(EventType.AnniversaryUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.EventType)])?.Contains(EventType.Anniversary, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                Debug.Assert(contact.Person != null);
                                contact.Person.Anniversary = null;
                            }
                        }


                        // swap Homepages

                        var homePagePersonal = contact.HomePagePersonal;
                        var homePageWork = contact.HomePageWork;

#if NET40
                        if (((string?)wrapper[nameof(ColumnName.WebHomeType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? false)
#else
                        if (((string?)wrapper[nameof(ColumnName.WebHomeType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? false)
#endif
                        {
                            contact.HomePageWork = homePagePersonal;
                            contact.HomePagePersonal = null;

#if NET40
                            if (!((string?)wrapper[nameof(ColumnName.WebWorkType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.WebWorkType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                contact.HomePagePersonal = homePageWork;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }



        private void SetTelephoneType(PhoneNumber phone, string? value)
        {
            if (value is null) return;

            phone.IsCell = phone.IsFax = phone.IsWork = false;

#if NET40
            value = value.ToUpperInvariant();

            if (value.Contains("MOBILE"))
            {
                phone.IsCell = true;
            }

            if (value.Contains("FAX"))
            {
                phone.IsFax = true;
            }

            if (value.Contains("WORK"))
            {
                phone.IsWork = true;
            }
#else
            if (value.Contains("MOBILE", StringComparison.OrdinalIgnoreCase))
            {
                phone.IsCell = true;
            }

            if (value.Contains("FAX", StringComparison.OrdinalIgnoreCase))
            {
                phone.IsFax = true;
            }

            if (value.Contains("WORK", StringComparison.OrdinalIgnoreCase))
            {
                phone.IsWork = true;
            }
#endif

        }
    }
}
