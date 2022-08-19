using CityInfo.API.Models;

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
                    Description = "The one with that big park."
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Dubai",
                    Description = "The one with that tallest tower in the world."
                },

                new CityDto()
                {
                    Id = 1,
                    Name = "London",
                    Description = "The one with that big dom."
                },


            };
        }
    }
}
