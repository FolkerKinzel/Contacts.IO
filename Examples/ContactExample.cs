using FolkerKinzel.Contacts;
using System;

namespace Examples
{
    static class ContactExample
    {
        public static Contact[] InitializeContacts() => new Contact[]
            {
                new Contact
                {
                    DisplayName = "John Doe",
                    Person = new Person
                    {
                        Name = new Name
                        {
                            FirstName = "John",
                            MiddleName = "William",
                            LastName = "Doe",
                            Suffix = "jr."
                        },

                        BirthDay = new DateTime(1972, 1, 3),
                        Spouse = "Jane Doe",
                        Anniversary = new DateTime(2001, 6, 15)
                    },

                    Work = new Work
                    {
                        JobTitle = "Facility Manager",
                        Company = "Does Company"
                    },

                    PhoneNumbers = new PhoneNumber[]
                    {
                        new PhoneNumber
                        {
                            Value = "0123-45678",
                            IsWork = true
                        }
                    },

                    EmailAddresses = new string[]
                    {
                        "john.doe@internet.com"
                    }
                },//new Contact()

                ///////////

                new Contact
                {
                    DisplayName = "Jane Doe",
                    Person = new Person
                    {
                        Name = new Name
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Prefix = "Dr."
                        },
                        BirthDay = new DateTime(1981, 5, 4),
                        Spouse = "John Doe",
                        Anniversary = new DateTime(2001, 6, 15)
                    },

                    Work = new Work
                    {
                        JobTitle = "CEO",
                        Company = "Does Company"
                    },

                    PhoneNumbers = new PhoneNumber[]
                    {
                        new PhoneNumber
                        {
                            Value = "876-54321",
                            IsMobile = true
                        }
                    }
                }//new Contact()
            };//new Contact[]
    }
}
