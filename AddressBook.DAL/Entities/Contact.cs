using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace AddressBook.DAL.Entities
{
    public class Contact
    {

        [Key] //primary key (long)
        public Int64 Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String LastName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public String Address { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
        [Required]
        public string TelephoneNumber { get; set; }
    }
}
