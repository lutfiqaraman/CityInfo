using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
                
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            CityDto? city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}
