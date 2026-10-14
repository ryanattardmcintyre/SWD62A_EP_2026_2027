using Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    //What is the role of this context class?
    //- This is an abstraction of the database
    //- in other words a database represented as a class
    public class LibraryDbContext: IdentityDbContext<IdentityUser>
    {

        //package Microsoft.AspNetCore.Identity.FrameworkCore has these models => User, Role, etc.
        //IdentityDbContext => is another BUILT-IN context class which has DbSets such as
        //                     Users, Roles, UserRoles,... using Models User, Role, etc

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        //we need the DbContextOptions so in the Program.cs, you might need to pass
        // some values from the very beginning e.g. connection string, 
        // stuff which regulate how strong the password of a registered user should be,
        // which database server you want to use?
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
        { }

        //other things which go inside the context class:
        //configuring the identity seed
        //generating column values e.g. Guid datatypes to be automatically stored in id columns
        //enabling lazy loading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}
