using AddressBook.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Interfaces
{
    // Iterface IContact can access to ContactFunction method and all
    public interface IContact
    {
        // EXPOSE THE FUNCTIONS REQUIRED

        Contact IsTelephoneNumberUse(string telephoneNumber);

        Task<Contact> AddContact(string firstname, string lastname, string address, string telephonenumber);

        Task<Contact> GetContact(Int64 id);

        Task<List<Contact>> SearchContact(string firstName = "", string lastname = "", string address = "", string telephoneNumber = "");

        Task<List<Contact>> GetAllContacts(int pageNumber = 1);

        Task<int> DeleteContact(int id);

        Task<int> PutContact(long id, Contact contact);

        Task<bool> CheckContactExists(long id);


    }
}
