using Library.DataAccess.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.DataBase.Contexts
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public LibraryDBContext( DbContextOptions<LibraryDBContext> options) : base( options ) {
            
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            base.OnModelCreating( modelBuilder );
        }

    }
}
