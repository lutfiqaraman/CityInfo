using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            CityDto? city =
                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);    
        }

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            CityDto? city =
                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            PointOfInterestDto? pointOfInterest = 
                city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            CityDto? city =
                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
                return NotFound();

            int maxPointOfInterest = 
                CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var createdPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterest,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(createdPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    CityId = cityId,
                    PointOfInterestId = createdPointOfInterest.Id
                }, createdPointOfInterest);
        }
    }
}
