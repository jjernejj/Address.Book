using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.UI.Models
{
    /// <summary>
    /// our basic contact view model
    /// </summary>
    public class ContactViewModel
    {
        public Int64 ContactId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
