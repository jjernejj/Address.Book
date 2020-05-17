using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.UI.FillData
{
    public static class ContactContext
    {
        public static List<Contact> ContactList { get; set; }

        public static void InitContacList(int contactCount)
        {
            ContactList = new List<Contact>();

            for (int a = 1; a < contactCount; a++)
            {
                ContactList.Add(new Contact
                {
                    Id = 20 + 1,
                    FirstName = "firstname" + a,
                    LastName = "lastname" + a,
                    Address = "address" + a,
                    TelephoneNumber = "123-555-" + a.ToString("000"),
                }
                    );


            }

        }



    }
}
