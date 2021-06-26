using FolkerKinzel.CsvTools.Helpers;
using FolkerKinzel.CsvTools.Helpers.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FolkerKinzel.Contacts.IO.Intls.Csv.Google
{
    internal class GoogleCsvReader : CsvReader
    {
#if NET40
        private readonly string[] _googleSeparator = new string[] { " ::: " };
#else
        private const string GOOGLE_SEPARATOR = " ::: ";
#endif

        internal GoogleCsvReader(Encoding? textEncoding) : base(null, textEncoding) { }

        protected override IList<Tuple<string, ContactProp?, IList<string>>> CreateMapping()
        {
            IList<Tuple<string, ContactProp?, IList<string>>>? mapping = HeaderRow.GetMapping();

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
            Debug.Assert(wrapper[index] != null);

            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.Phone1Type:
                    {
                        var newNumber = new PhoneNumber();
                        AddPhoneNumber(contact, newNumber);
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
                        if (contact.PhoneNumbers is null)
                        {
                            contact.PhoneNumbers = new PhoneNumber();
                        }

                        PhoneNumber lastNumber = contact.PhoneNumbers.Last()!;


                        if (lastNumber.IsEmpty)
                        {
                            SetTelephoneType(lastNumber, (string?)wrapper[index]);
                        }
                        else
                        {
                            var newNumber = new PhoneNumber();
                            AddPhoneNumber(contact, newNumber);
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
                        if (contact.PhoneNumbers is null)
                        {
                            contact.PhoneNumbers = new PhoneNumber();
                        }

                        PhoneNumber lastNumber = contact.PhoneNumbers.Last()!;

                        lastNumber.Value = (string?)wrapper[index];
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
                case AdditionalProp.Swap: // diverse Aufräumarbeiten
                    {
                        // Swap Addresses:
                        Address? addrHome = contact.AddressHome;
                        Address? addrWork = contact.Work?.AddressWork;

#if NET40
                        if (((string?)wrapper[nameof(ColumnName.AddressHomeType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? false)

#else
                        if (((string?)wrapper[nameof(ColumnName.AddressHomeType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? false)
#endif
                        {
                            Work? work = contact.Work ?? new Work();
                            contact.Work = work;


                            work.AddressWork = addrHome;
                            contact.AddressHome = null;

#if NET40
                            if (!((string?)wrapper[nameof(ColumnName.AddressWorkType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.AddressWorkType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                contact.AddressHome = addrWork;
                            }
                        }

                        Person? person = contact.Person;

                        if (person != null)
                        {
                            // check RelationType
#if NET40
                            if (!((string?)wrapper[nameof(ColumnName.RelationType)])?.ToUpperInvariant().Contains(RelationType.SpouseUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.RelationType)])?.Contains(RelationType.Spouse, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                person.Spouse = null;
                            }

                            // check Anniversary
#if NET40
                            if (!((string?)wrapper[nameof(ColumnName.EventType)])?.ToUpperInvariant().Contains(EventType.AnniversaryUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.EventType)])?.Contains(EventType.Anniversary, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                Debug.Assert(contact.Person != null);
                                contact.Person.Anniversary = null;
                            }
                        }


                        // swap Homepages

                        var homePagePersonal = contact.WebPagePersonal;
                        var homePageWork = contact.WebPageWork;

#if NET40
                        if (((string?)wrapper[nameof(ColumnName.WebHomeType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? false)
#else
                        if (((string?)wrapper[nameof(ColumnName.WebHomeType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? false)
#endif
                        {
                            contact.WebPageWork = homePagePersonal;
                            contact.WebPagePersonal = null;

#if NET40
                            if (!((string?)wrapper[nameof(ColumnName.WebWorkType)])?.ToUpperInvariant().Contains(PropertyClassType.WorkUpperCase) ?? true)

#else
                            if (!((string?)wrapper[nameof(ColumnName.WebWorkType)])?.Contains(PropertyClassType.Work, StringComparison.OrdinalIgnoreCase) ?? true)
#endif
                            {
                                contact.WebPagePersonal = homePageWork;
                            }
                        }


                        // Split CombinedValues

                        var emails = (List<string>?)contact.EmailAddresses;

                        if (emails != null)
                        {
                            for (int i = emails.Count - 1; i >= 0; i--)
                            {
                                var currentEmail = emails[i];

                                if(!ContainsGoogleSeparator(currentEmail))
                                {
                                    continue;
                                }

                                var arr = SplitAtGoogleSeparator(currentEmail);

                                if (arr.Length > 1)
                                {
                                    emails.RemoveAt(i);

                                    int j = 0;
                                    while(j < arr.Length)
                                    {
                                        emails.Insert(i + j, arr[j]);
                                        j++;
                                    }
                                }
                            }
                        }

                        var ims = (List<string>?)contact.InstantMessengerHandles;

                        if (ims != null)
                        {
                            for (int i = ims.Count - 1; i >= 0; i--)
                            {
                                var currentIms = ims[i];

                                if (!ContainsGoogleSeparator(currentIms))
                                {
                                    continue;
                                }

                                var arr = SplitAtGoogleSeparator(currentIms);

                                if (arr.Length > 1)
                                {
                                    ims.RemoveAt(i);

                                    int j = 0;
                                    while (j < arr.Length)
                                    {
                                        ims.Insert(i + j, arr[j]);
                                        j++;
                                    }
                                }
                            }
                        }

                        IEnumerable<PhoneNumber?>? contactPhones = contact.PhoneNumbers;
                        if (contactPhones != null && contactPhones.Any(x => x!.Value != null && ContainsGoogleSeparator(x.Value)))
                        {
                            List<PhoneNumber> phones;

                            if (contactPhones is PhoneNumber pn)
                            {
                                phones = new List<PhoneNumber>() { pn };
                                contact.PhoneNumbers = phones;
                            }
                            else
                            {
                                Debug.Assert(contactPhones is List<PhoneNumber>);
                                phones = (List<PhoneNumber>)contactPhones;
                            }

                            for (int i = phones.Count - 1; i >= 0; i--)
                            {
                                PhoneNumber phone = phones[i];

                                string? phoneNumber = phone.Value;

                                if (phoneNumber != null && ContainsGoogleSeparator(phoneNumber))
                                {
                                    string[] arr = SplitAtGoogleSeparator(phoneNumber);

                                    if (arr.Length > 1)
                                    {
                                        phones.RemoveAt(i);

                                        int j = 0;

                                        while (j < arr.Length)
                                        {
                                            phone = j == 0 ? phone : (PhoneNumber)phone.Clone();
                                            phone.Value = arr[j];

                                            phones.Insert(i + j, phone);
                                            j++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }



#if NET40
        private bool ContainsGoogleSeparator(string s) => s.Contains(_googleSeparator[0]);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ContainsGoogleSeparator(string s) => s.Contains(GOOGLE_SEPARATOR, StringComparison.Ordinal);
#endif

#if NET40
        private string[] SplitAtGoogleSeparator(string s) => s.Split(_googleSeparator, StringSplitOptions.RemoveEmptyEntries);
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string[] SplitAtGoogleSeparator(string s) => s.Split(GOOGLE_SEPARATOR, StringSplitOptions.RemoveEmptyEntries);
#endif


        private static void SetTelephoneType(PhoneNumber phone, string? value)
        {
            if (value is null)
            {
                return;
            }

            phone.IsMobile = phone.IsFax = phone.IsWork = false;

#if NET40
            value = value.ToUpperInvariant();

            if (value.Contains("MOBILE"))
            {
                phone.IsMobile = true;
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
                phone.IsMobile = true;
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
