using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DomowaBibliotekaCore.Models.Data_Models;

namespace DomowaBibliotekaCore.Models
{
 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Series> Series { get; set; }

        public DbSet<User_Books> User_Books { get; set; }
        public DbSet<Book_Authors> Book_Authors { get; set; }
        public DbSet<Book_Genres> Book_Genres { get; set; }
        public DbSet<Book_Series> Book_Series { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./DomowaBibliotekaCore.db");
        }

        public bool TrySaveChanges()
        {
            try
            {
                SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }   
}