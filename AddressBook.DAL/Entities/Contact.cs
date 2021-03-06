﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.DAL.Entities
{
    public class Contact
    {

        [Key] //primary key (long)
        public Int64 Id { get; set; }

        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String FirstName { get; set; }

        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public String LastName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public String Address { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$")]
        [Required]
        [Remote(action: "IsTelephoneNumberUse", controller: "Contact")]
        public string TelephoneNumber { get; set; }
    }
}
