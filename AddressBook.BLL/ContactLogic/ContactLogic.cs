using AddressBook.DAL.Entities;
using AddressBook.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.ContactLogic
{
    public class ContactLogic
    {
        /// <summary>
        /// interface of Contacts functions
        /// </summary>
        private IContact _contact = new AddressBook.DAL.Functions.ContactFunctions();

        /// <summary>
        /// ADD NEW CONTACT
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="address"></param>
        /// <param name="telephonenumber"></param>
        /// <returns></returns>
        public async Task<Boolean> CreateNewContact(string firstname, string lastname, string address, string telephonenumber)
        {
            try
            {
                var result = await _contact.AddContact(firstname, lastname, address, telephonenumber);
                if (result.Id > 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public async Task<Boolean> DeleteThisContact(int id)
        {
            try
            {
                var result = await _contact.DeleteContact(id);
                if (result > 0)
                    return true;
                else
                    return false;


            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<Contact> GetContact(Int64 id)
        {
            Contact contact = await _contact.GetContact(id);
            return contact;
        }




        /// <summary>
        /// GET ALL CONTACTS
        /// </summary>
        /// <returns></returns>
        public async Task<List<Contact>> GetAllContacts(int pagination)
        {
            List<Contact> contacts = await _contact.GetAllContacts(pagination);

            return contacts;
        }


        public async Task<int> UpdateContact(long id, Contact contact)
        {
            int data = await _contact.PutContact(id, contact);
            return data;
        }



        public async Task<bool> VerifyContact(long id)
        {
            var data = await _contact.CheckContactExists(id);
            return data;
        }




    }
}
