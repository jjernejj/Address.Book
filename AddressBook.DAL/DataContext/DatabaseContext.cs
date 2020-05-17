using System;
using System.Collections.Generic;
using System.Text;
using AddressBook.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace AddressBook.DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public class OptionBuilder
        {
            //CONSTRUCTOR
            public OptionBuilder()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                opsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<DatabaseContext> opsBuilder { get; set; }

            public DbContextOptions<DatabaseContext> dbOptions { get; set; }

            private AppConfiguration settings { get; set; }
        }
        public static OptionBuilder ops = new OptionBuilder();

        //CONSTCTOR
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        //DbSet GO HERE (each entitie class add here)
        // we tell to DbContext the table of database
        public DbSet<Contact> Contacts { get; set; }



    }
}
