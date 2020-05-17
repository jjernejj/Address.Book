using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.BLL.ContactLogic;
using AddressBook.DAL.Entities;
using AddressBook.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.UI.API
{
    [Route("api/contact")]
    [ApiController]
    public class ContactAPI_Controller : ControllerBase
    {
        // reference to Contact Logic
        private ContactLogic contactLogic = new ContactLogic();


        /// <summary>
        /// Add/Create new Contact
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="address"></param>
        /// <param name="telephonenumber"></param>
        /// <param name="contactid"></param>
        /// <returns>return true if is sucessfully created contact</returns>
        [Route("add")]
        [HttpGet]
        public async Task<Boolean> AddContact(string firstname, string lastname, string address, string telephonenumber)
        {

            bool result = await contactLogic.CreateNewContact(firstname, lastname, address, telephonenumber);

            return result;
        }



        /// <summary>
        /// Fill data into database that can work and testing 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="address"></param>
        /// <param name="telephonenumber"></param>
        /// <returns></returns>
        [Route("fill")]
        [HttpGet]
        public async Task<Boolean> FillContactsData(string firstname, string lastname, string address, string telephonenumber)
        {
            bool result = false;
            var dataList = FillData.ContactContext.ContactList;
            foreach (var data in dataList)
            {

                result = await contactLogic.CreateNewContact(data.FirstName, data.LastName, data.Address, data.TelephoneNumber);
            }
            return result;
        }



        /// <summary>
        /// Get all Contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [HttpGet]
        public async Task<List<ContactViewModel>> GetAllContact(int pagination)
        {
            List<ContactViewModel> contactList = new List<ContactViewModel>();
            var contacts = await contactLogic.GetAllContacts(pagination);
            if (contacts.Count > 0)
            {
                foreach (var contact in contacts)
                {
                    ContactViewModel curentContact = new ContactViewModel
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Address = contact.Address,
                        TelephoneNumber = contact.TelephoneNumber
                    };
                    contactList.Add(curentContact);
                }
            }
            return contactList;
        }



    }
}