using System;

namespace FolkerKinzel.Contacts.IO.Tests
{
    public static class Utility
    {
        public static Contact InitTestContact()
        {
            var addr = new Address
            {
                Street = "HomeStreet Str. 42",
                City = "Entenhausen",
                PostalCode = "0123",
                State = "Kansas",
                Country = "Märchenland"

            };


            var work = new Work
            {
                AddressWork = new Address
                {
                    Street = "WorkStreet 4711",
                    City = "Berlin",
                    PostalCode = "030",
                    State = "Berlin-Brandenburg",
                    Country = "BRD"
                },
                Company = "Maxls Firma",
                Department = "Chefetage",
                Office = "Büro",
                JobTitle = "Chef"
            };

            var pers = new Person
            {
                BirthDay = new DateTime(1985, 6, 28),
                Name = new Name { FirstName = "Max", LastName = "Mustermann" },
                Gender = Sex.Male,
                Anniversary = new DateTime(2001, 4, 22),
                NickName = "Maxl",
                Spouse = "Daisy"
            };


            return new Contact
            {
                Work = work,
                WebPagePersonal = "www.folker.de",
                WebPageWork = "work.de",
                DisplayName = "Folker",
                EmailAddresses = new string?[] { "folker@freenet.de", "info@folker.de", null },
                Person = pers,
                AddressHome = addr,
                InstantMessengerHandles = new string?[] { "Maxl@twitter.com", "Maxl@skype.com" },
                PhoneNumbers = new PhoneNumber[] {
                    new PhoneNumber("1"),
                    new PhoneNumber("2", isWork: true),
                    new PhoneNumber("3", isMobile: true),
                    new PhoneNumber("4", isWork: true, isMobile: true),
                    new PhoneNumber("5", isFax: true),
                    new PhoneNumber("6", isWork: true, isFax: true),
                    new PhoneNumber("7")
                }
            };

        }
    }
}