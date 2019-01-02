using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHomeBar.Data.Identity;
using System;

namespace MyHomeBar.Data
{
    public class MyHomeBarDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyHomeBarDbContext(DbContextOptions<MyHomeBarDbContext> options)
              : base(options)
        {
        }

        //public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
