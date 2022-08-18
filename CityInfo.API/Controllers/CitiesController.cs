using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetAllCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id = 1, Name = "New York City"},
                    new {id = 2, Name = "Los Angeles"}
                });
        }
    }
}
