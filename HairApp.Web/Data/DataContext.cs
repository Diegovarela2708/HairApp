

using HairApp.Common.Entities;
using HairApp.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HairApp.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<BookingHistory> BookingHistories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<City> Cities { get; set; }        
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Neighborhood> Neighborhoods { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Shop> Shops { get; set; }

        public DbSet<ShopImage> ShopImages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Departament>(cou =>
            {
                cou.HasIndex("Name").IsUnique();
                cou.HasMany(c => c.Cities).WithOne(d => d.Departament).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<City>(dep =>
            {
                dep.HasIndex("Name", "DepartamentId").IsUnique();
                dep.HasOne(d => d.Departament).WithMany(c => c.Cities).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Neighborhood>(cit =>
            {
                cit.HasIndex("Name", "CityId").IsUnique();
                cit.HasOne(c => c.City).WithMany(d => d.Neighborhoods).OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<User>()
            .HasIndex(t => t.Document)
            .IsUnique();

            modelBuilder.Entity<Shop>()
                .HasIndex(t => t.Name)
                .IsUnique();               

        }
    }

}
