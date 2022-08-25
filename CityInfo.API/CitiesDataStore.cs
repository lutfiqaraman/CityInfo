
using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "United Nations Headquarters",
                            Description = "The building of United Nations"
                        },

                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "The Museum of Modern Art",
                            Description = "The most important museum in New York related to the modern art"
                        }
                    }
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Dubai",
                    Description = "The one with that tallest tower in the world.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Burj Khalifa",
                            Description = "The world's tallest tower"
                        },

                        new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Burj Al Arab",
                            Description = "The most luxury hotel in the world"
                        }
                    }
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "London",
                    Description = "The one with that big dom.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Buckingham Palace",
                            Description = "The biggest palace in the world"
                        },

                        new PointOfInterestDto()
                        {
                            Id = 6,
                            Name = "British Museum",
                            Description = "the biggest museum in London city"
                        }
                    }
                },

            };
        }
    }
}
