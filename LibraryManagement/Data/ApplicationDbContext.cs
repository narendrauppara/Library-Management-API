using LibraryManagement.Model.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryManagement.Model;

namespace LibraryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
      //  public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(new User
            {
                UserEmailId = "John@gmail.com",
                Password = "Password@1234",
                isAdmin = false,
                //Role = "User"

            }, new User
            {
                UserEmailId = "Catherine@gmail.com",
                Password = "Password@1234",
                isAdmin = false,
                //Role = "User"

            },
             new User
             {
                 UserEmailId = "Betsy@gmail.com",
                 Password = "Password@1234",
                 isAdmin = false,
               //  Role = "User"

             },
             new User
             {
                 UserEmailId = "admin@gmail.com",
                 Password = "Password@12345",
                 isAdmin = true,
                // Role = "Admin"

             });
        }
    }
}

