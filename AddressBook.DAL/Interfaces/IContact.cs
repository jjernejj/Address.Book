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

        Task<Contact> AddContact(string firstname, string lastname, string address, string telephonenumber);

        Task<Contact> GetContact(Int64 id);

        Task<List<Contact>> GetAllContacts(int pageNumber = 1);

        Task<int> DeleteContact(int id);

        Task<int> PutContact(long id, Contact contact);

        Task<bool> CheckContactExists(long id);


    }
}
