﻿using AddressBook.DAL.DataContext;
using AddressBook.DAL.Entities;
using AddressBook.DAL.Interfaces;
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
        ///  add new contact
        /// </summary>
        /// <param name="firstname">insert first name</param>
        /// <param name="lastname">insert last name</param>
        /// <param name="address">insert address of book</param>
        /// <param name="telephonenumber">insert telephone number</param>
        /// <param name="contactid"></param>
        /// <returns></returns>
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


        /// <summary>
        /// GET CONTACT BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Get all Contacts
        /// 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        ///  Update specific contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        // PUT: api/ContactsEF/5 
        //[HttpPut("{id}")]
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



        /// <summary>
        /// Method veriy if exist contact with specific Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckContactExists(long id)
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOptions);
            var data = await context.Contacts.AnyAsync(e => e.Id == id);

            return data;
        }






    }
}
