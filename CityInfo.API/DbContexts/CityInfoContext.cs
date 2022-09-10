using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(

                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park."
                },

                new City("Dubai")
                {
                    Id = 2,
                    Description = "The one with that tallest tower in the world."
                },

                new City("London")
                {
                    Id = 3,
                    Description = "The one with that big dom."
                });

            modelBuilder.Entity<PointOfInterest>()
                .HasData(

                new PointOfInterest("United Nations Headquarters", "The building of United Nations")
                {
                    Id= 1,
                    CityId = 1                    
                },

                new PointOfInterest("The Museum of Modern Art", "The most important museum in New York related to the modern art")
                {
                    Id = 2,
                    CityId = 1
                },

                new PointOfInterest("Burj Khalifa", "The tallest tower in the world")
                {
                    Id = 3,
                    CityId = 2
                },

                new PointOfInterest("Burj Al Arab", "The most luxury hotel in the world")
                {
                    Id = 4,
                    CityId = 2
                },

                new PointOfInterest("Buckingham Palace", "The biggest palace in the world")
                {
                    Id = 5,
                    CityId = 3
                },

                new PointOfInterest("British Museum", "The biggest museum in London city")
                {
                    Id = 6,
                    CityId = 3
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
