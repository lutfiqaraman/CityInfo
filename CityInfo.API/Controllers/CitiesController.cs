using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository citiesInfoRepository;
        private readonly IMapper mapper;

        public CitiesController(ICityInfoRepository CityInfoRepository, IMapper Mapper)
        {
            citiesInfoRepository = 
                CityInfoRepository ?? throw new ArgumentNullException(nameof(CityInfoRepository));

            mapper = 
                Mapper ?? throw new ArgumentNullException(nameof(Mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithNoPointsOfInterestDto>>> GetCities()
        {
            IEnumerable<City> cities =
                await citiesInfoRepository.GetCities();

            IEnumerable<CityWithNoPointsOfInterestDto> result =
                mapper.Map<IEnumerable<CityWithNoPointsOfInterestDto>>(cities);
                
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public ActionResult<CityDto> GetCity(int id)
        //{
        //    CityDto? city = 
        //        _citiesDataStore.Cities.FirstOrDefault(x => x.Id == id);

        //    if (city == null)
        //        return NotFound();

        //    return Ok(city);
        //}
    }
}
