using AddressBook.DAL.DataContext;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DAL.Functions
{
    // inherit IContact class that can 

    public class ContactFunctions : IContact
    {

        /// <summary>
        /// check if exist yet this telephone number
        /// </summary>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        public Contact IsTelephoneNumberUse(string telephoneNumber)
        {
            Contact contact;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                contact = context.Contacts.FirstOrDefault(a => a.TelephoneNumber == telephoneNumber);
            }
            return contact;
        }


        /// <summary>
        ///  add new contact
        /// </summary>
        /// <param name="firstname">insert first name</param>
        /// <param name="lastname">insert last name</param>
        /// <param name="address">insert address of book</param>
        /// <param name="telephonenumber">insert telephone number</param>
        /// <param name="contactid"></param>
        /// <returns></returns>
        #region Task<Contact> AddContact(string firstname, string lastname, string address, string telephonenumber)
        public async Task<Contact> AddContact(string firstname, string lastname, string address, string telephonenumber)
        {
            Contact newContact = new Contact
            {
                FirstName = firstname,
                LastName = lastname,
                Address = address,
                TelephoneNumber = telephonenumber,
            };

            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                await context.Contacts.AddAsync(newContact);
                await context.SaveChangesAsync();
            }
            return newContact;
        }
        #endregion


        /// <summary>
        /// GET CONTACT BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Task<Contact> GetContact(Int64 id)
        public async Task<Contact> GetContact(Int64 id)
        {
            Contact contact = new Contact();
            // initialite DatabaseContext object
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                //then we access table of CONTACTS and allocated our list of contacts
                contact = await context.Contacts.Where(a => a.Id == id).SingleOrDefaultAsync();
            }
            return contact;
        }
        #endregion


        /// <summary>
        /// Seacrh Contact by inserted value
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastname"></param>
        /// <param name="address"></param>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        #region Task<List<Contact>> SearchContact(string firstName = "", string lastname = "", string address = "", string telephoneNumber = "")
        public async Task<List<Contact>> SearchContact(string firstName = "", string lastname = "", string address = "", string telephoneNumber = "")
        {
            List<Contact> contact = new List<Contact>();
            // initialite DatabaseContext object
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                    contact = await context.Contacts.Where(a => a.FirstName.Contains(firstName)).ToListAsync();
                if (!string.IsNullOrWhiteSpace(lastname))
                    contact = await context.Contacts.Where(a => a.LastName.Contains(lastname)).ToListAsync();
                if (!string.IsNullOrWhiteSpace(address))
                    contact = await context.Contacts.Where(a => a.Address.Contains(address)).ToListAsync();
                if (!string.IsNullOrWhiteSpace(telephoneNumber))
                    contact = await context.Contacts.Where(a => a.TelephoneNumber.Contains(telephoneNumber)).ToListAsync();

            }
            return contact;
        }
        #endregion


        /// <summary>
        /// Get all Contacts
        /// 
        /// </summary>
        /// <returns></returns>
        #region Task<List<Contact>> GetAllContacts(int pageNumber = 1)
        public async Task<List<Contact>> GetAllContacts(int pageNumber = 1)
        {
            List<Contact> contacts = new List<Contact>();

            // initialite DatabaseContext object
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                //then we access table of CONTACTS and allocated our list of contacts
                //contacts = await context.Contacts.ToListAsync();
                contacts = await PaginatedList<Contact>.CreateAsync(context.Contacts, pageNumber, 5);

            }
            // and at the end return list of contacts
            return contacts;
        }
        #endregion


        ///// <summary>
        ///// Get all Contacts
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public async Task<List<Contact>> GetAllContacts()
        //{
        //    List<Contact> contacts = new List<Contact>();

        //    // initialite DatabaseContext object
        //    using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
        //    {
        //        //then we access table of CONTACTS and allocated our list of contacts
        //        contacts = await context.Contacts.ToListAsync();
        //    }
        //    // and at the end return list of contacts
        //    return contacts;
        //}



        /// <summary>
        /// To Delete the record of a particular contact  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Task<int> DeleteContact(int id)
        public async Task<int> DeleteContact(int id)
        {
            try
            {
                //Contact contact;
                int data;
                using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
                {
                    //contact = await context.Contacts.FindAsync(id);
                    var contact = await context.Contacts.FirstOrDefaultAsync(a => a.Id == id);

                    context.Contacts.Remove(contact);
                    data = await context.SaveChangesAsync();
                }
                return data;
            }
            catch
            {
                throw;
            }
        }
        #endregion


        /// <summary>
        ///  Update specific contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        // PUT: api/ContactsEF/5 
        //[HttpPut("{id}")]
        #region Task<int> PutContact(long id, Contact contact)
        public async Task<int> PutContact(long id, Contact contact)
        {
            int update;
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOptions))
            {
                context.Entry(contact).State = EntityState.Modified;

                try
                {
                    update = await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return update;
        }
        #endregion



        /// <summary>
        /// Method veriy if exist contact with specific Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Task<bool> CheckContactExists(long id)
        public async Task<bool> CheckContactExists(long id)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var data = await context.Contacts.AnyAsync(e => e.Id == id);

            return data;
        }

        #endregion





    }
}
