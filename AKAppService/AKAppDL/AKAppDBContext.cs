using Microsoft.EntityFrameworkCore;
using AKAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKAppDL
{
    public class AKAppDBContext : DbContext
    {
        public AKAppDBContext(DbContextOptions options) : base(options)
        {
        }
        public AKAppDBContext()
        {
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Upload> Upload { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Application>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Location>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Upload>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Application>()
                .HasMany(s => s.Uploads);
            modelBuilder.Entity<Application>()
                .HasOne(l => l.Location);
            modelBuilder.Entity<Location>()
                .HasOne(a => a.Address);

        }
    }
}
