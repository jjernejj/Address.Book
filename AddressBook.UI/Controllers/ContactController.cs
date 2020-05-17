using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AddressBook.BLL.ContactLogic;
using AddressBook.DAL.DataContext;
using AddressBook.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AddressBook.UI.API
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private ContactLogic contactLogic = new ContactLogic();

        //Asynchronous action


        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {

            try
            {
                var data = await contactLogic.GetAllContacts(pageNumber);

                if (data == null)
                    //return NotFound();  // Content(HttpStatusCode.NotFound, "Contact not found");
                    return StatusCode(StatusCodes.Status404NotFound, "Contact not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(Int64 id)
        {
            try
            {
                var data = await contactLogic.GetContact(id);

                if (data == null)
                    //return NotFound();  // Content(HttpStatusCode.NotFound, "Contact not found");
                    return StatusCode(StatusCodes.Status404NotFound, "Contact not found");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }

        }
    }
}
