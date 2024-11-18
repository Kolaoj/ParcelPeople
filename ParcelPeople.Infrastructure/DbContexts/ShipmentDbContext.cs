using Microsoft.EntityFrameworkCore;
using ParcelPeople.Domain.Entities;
using ParcelPeople.Domain.Models;

namespace ParcelPeople.Infrastructure.DbContexts
{
    public class ShipmentDbContext(DbContextOptions<ShipmentDbContext> options) : DbContext(options)
    {
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentCity> ShipmentCities { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<ParcelSurcharge> ParcelSurcharges { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Enum converstions for db readability
            modelBuilder.Entity<Shipment>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ShipmentCity>()
                .Property(s => s.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Parcel>()
                .Property(p => p.Type)
                .HasConversion<string>();

            //Seed Data
            modelBuilder.Entity<ParcelSurcharge>().HasData(
                new ParcelSurcharge { DimensionThreshold = 0, Surcharge = 0 },
                new ParcelSurcharge { DimensionThreshold = 50, Surcharge = 0.2M },
                new ParcelSurcharge { DimensionThreshold = 100, Surcharge = 0.4M }
            );

            modelBuilder.Entity<City>().HasData(
              new City { Id = 1, Name = "London", CultureCode = "en-GB", Country = "United Kingdom", OriginCost = 10M },
              new City { Id = 2, Name = "Dublin", CultureCode = "en-IE", Country = "Ireland", OriginCost = 20M },
              new City { Id = 3, Name = "Edinburgh", CultureCode = "en-GB", Country = "Scotland", OriginCost = 10M }
            );

            modelBuilder.Entity<Customer>().HasData(
             new Customer { Id = new Guid("c2e9e363-2e20-433a-88d6-3c1090d85d52"), FirstName = "Kola", LastName = "Oj", Email = "kolaoj@test.com", ContactNumber = "+4477788899911" }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string databasePath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}MySqliteDatabase.db";
            options.UseSqlite($"Data Source=C:\\Users\\kola.ojubanire\\source\\personal-repos\\Ascot\\ParcelPeople\\ParcelPeople.Infrastructure\\Databases\\MySqliteDatabase.db");
        }

    }
}
