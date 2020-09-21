using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class LibraryManagementDbContext :DbContext
    {
        public LibraryManagementDbContext(DbContextOptions
        <LibraryManagementDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Book>().Property(p => p.FavouriteTo)
                    .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
            modelBuilder.Entity<Book>().Property(p => p.ReadyBy)
                    .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
            modelBuilder.Entity<Book>().Property(p => p.ReviewedBy)
                    .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
            modelBuilder.Entity<Book>().Property(p => p.Reviews)
                    .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<List<string>>(v));
            modelBuilder.Entity<Book>().HasData(new Book
            {
                BookId = 1,
                BookName = "IgnitedMinds"

            }, new Book
            {
                BookId = 2,
                BookName = "The 5 Am"

            },
             new Book
             {
                 BookId = 3,
                 BookName = "WingsOfFire"
             });
        }
    }
}
