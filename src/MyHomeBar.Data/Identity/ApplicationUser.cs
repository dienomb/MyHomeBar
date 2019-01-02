using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHomeBar.Data.Identity
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsBanned { get; set; }
        public string Voucher { get; set; }

    }
}
