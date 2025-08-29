using Microsoft.EntityFrameworkCore;
using Properties.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain
{
    public class PropertiesDbContext : DbContext
    {

        public PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : base(options)
        {

        }

        public DbSet<Owner> Owners  { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<PropertyTrace>().ToTable("PropertyTrace");
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImage");
        }
    }
}
