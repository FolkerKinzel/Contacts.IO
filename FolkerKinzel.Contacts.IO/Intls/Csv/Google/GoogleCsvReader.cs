using FolkerKinzel.CsvTools.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            wrapper.AddProperty(
                        new CsvProperty(
                            tpl.Item1,
                            tpl.Item3,
                            this.StringConverter));
        }



        protected override void InitContactNonStandardProp(Contact contact, ContactProp prop, object? value)
        {
            switch ((AdditionalProp)prop)
            {
                case AdditionalProp.Phone1Type:
                    break;
                case AdditionalProp.Phone1Value:
                    break;
                case AdditionalProp.Phone2Type:
                    break;
                case AdditionalProp.Phone2Value:
                    break;
                case AdditionalProp.Phone3Type:
                    break;
                case AdditionalProp.Phone3Value:
                    break;
                case AdditionalProp.Phone4Type:
                    break;
                case AdditionalProp.Phone4Value:
                    break;
                case AdditionalProp.Phone5Type:
                    break;
                case AdditionalProp.Phone5Value:
                    break;
                case AdditionalProp.Phone6Type:
                    break;
                case AdditionalProp.Phone6Value:
                    break;
                case AdditionalProp.Phone7Type:
                    break;
                case AdditionalProp.Phone7Value:
                    break;
                case AdditionalProp.Phone8Type:
                    break;
                case AdditionalProp.Phone8Value:
                    break;
                case AdditionalProp.Phone9Type:
                    break;
                case AdditionalProp.Phone9Value:
                    break;
                case AdditionalProp.AddressHomeType:
                    break;
                case AdditionalProp.AddressWorkType:
                    break;
                case AdditionalProp.InstantMessenger1Type:
                    break;
                case AdditionalProp.InstantMessenger1Service:
                    break;
                case AdditionalProp.InstantMessenger2Type:
                    break;
                case AdditionalProp.InstantMessenger2Service:
                    break;
                case AdditionalProp.RelationType:
                    break;
                case AdditionalProp.WebHomeType:
                    break;
                case AdditionalProp.WebWorkType:
                    break;
                case AdditionalProp.EventType:
                    break;
                case AdditionalProp.Swap:
                    break;
                default:
                    break;
            }
        }


    }
}
