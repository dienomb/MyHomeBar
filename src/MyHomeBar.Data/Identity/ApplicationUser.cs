using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHomeBar.Data.Identity
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        // Extended Properties
        public DateTime BirthDate { get; set; }
        public bool IsBanned { get; set; }
        public string Voucher { get; set; }

    }
}
