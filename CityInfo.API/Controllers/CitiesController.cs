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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            City? city =
                await citiesInfoRepository.GetCityById(id, includePointsOfInterest);

            if (city == null)
                return NotFound();

            var result =
                mapper.Map<CityDto>(city);

            if (includePointsOfInterest)
                return Ok(result);

            var resultWithoutPointOfInterest = 
                mapper.Map<CityWithNoPointsOfInterestDto>(city);

            return Ok(resultWithoutPointOfInterest);
        }
    }
}
