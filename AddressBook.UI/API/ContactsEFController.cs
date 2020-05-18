using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBook.DAL.DataContext;
using AddressBook.DAL.Entities;
using AddressBook.BLL.ContactLogic;

namespace AddressBook.UI.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsEFController : ControllerBase
    {
        /// <summary>
        /// instance of business logic layer
        /// </summary>
        private ContactLogic _context = new ContactLogic();


        //[HttpGet("{teleNumber}")]
        //public Contact IsTelephoneNumberUse(string teleNumber)
        //{
        //    var data = _context.IsTelephoneNumberUse(teleNumber);
        //    return data;
        //}

        /// <summary>
        /// GET: api/ContactsEF
        /// Get all contact
        /// </summary>
        /// <returns></returns>
        [HttpGet("{pageNumber}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(int pageNumber)
        {
            var data = await _context.GetAllContacts(pageNumber);
            if (data.Count < 1)
            {
                return Ok("Currently doesn't exist any records.");
            }

            return data;
        }




        /// <summary>
        /// GET: api/ContactsEF/5
        /// Get contact by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(long id)
        {
            var contact = await _context.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }


        /// <summary>
        /// API seacrh contact
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastname"></param>
        /// <param name="address"></param>
        /// <param name="telephoneNumber"></param>
        /// <returns></returns>
        public async Task<ActionResult<List<Contact>>> SearchContact(string firstName = "", string lastname = "", string address = "", string telephoneNumber = "")
        {
            var contact = await _context.SearchContact(firstName, lastname, address, telephoneNumber);

            if (contact == null)
            {
                return NotFound();
            }
            return contact;
        }


        /// <summary>
        /// PUT: api/ContactsEF/5,
        /// Update contact in a table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        #region Task<IActionResult> PutContact(long id, Contact contact)
        public async Task<IActionResult> PutContact(long id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                int data = await _context.UpdateContact(id, contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.VerifyContact(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        } 
        #endregion


        /// <summary>
        ///  POST: api/ContactsEF,
        ///  Creare new contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost()]
        //[Route("CreateNewContact")]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            var data = await _context.CreateNewContact(contact.FirstName, contact.LastName, contact.Address, contact.TelephoneNumber);
            if (data == false)
            {
                return BadRequest();
            }
            return Ok();
            //return Ok("A new contact was Successfully created.");
        }



        /// <summary>
        /// DELETE: api/ContactsEF/5
        /// Delete contact by specific Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            //var contact = await _context.Contacts.FindAsync(id);

            var data = await _context.DeleteThisContact(id);
            if (data == false)
            {
                return NotFound();
            }
            return Ok();
        }



        //// DELETE: api/ContactsEF/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Contact>> DeleteContact(long id)
        //{
        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Contacts.Remove(contact);
        //    await _context.SaveChangesAsync();

        //    return contact;
        //}

        //private bool ContactExists(long id)
        //{
        //    return _context.Contacts.Any(e => e.Id == id);
        //}



    }
}
