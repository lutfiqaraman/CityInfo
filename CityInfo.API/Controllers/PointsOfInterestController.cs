using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> logger;
        private readonly ICityInfoRepository cityInfoRepository;
        private readonly IMapper mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> Logger, ICityInfoRepository CityInfoRepository, IMapper Mapper)
        {
            logger = 
                Logger ?? throw new ArgumentNullException(nameof(Logger));

            cityInfoRepository = 
                CityInfoRepository ?? throw new ArgumentNullException(nameof(CityInfoRepository));

            mapper = 
                Mapper ?? throw new ArgumentNullException(nameof(Mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!await cityInfoRepository.IsCityExist(cityId))
                {
                    logger.LogInformation($"City with id {cityId} was not found");
                    return NotFound();
                }

                IEnumerable<PointOfInterest> pointsOfInterestForCity =
                    await cityInfoRepository.GetPointsOfInterestForCity(cityId);

                IEnumerable<PointOfInterestDto>? result = 
                    mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex!, "Exception while getting points of interest for city with id {cityId}", cityId);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await cityInfoRepository.IsCityExist(cityId))
                return NotFound();

            var pointOfInterest = 
                await cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterest == null)
                return NotFound();

            var result = 
                mapper.Map<PointOfInterestDto>(pointOfInterest);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await cityInfoRepository.IsCityExist(cityId))
                return NotFound();

            PointOfInterest? mappedPointOfInterest =
                mapper.Map<PointOfInterest>(pointOfInterest);

            await cityInfoRepository.AddPointOfInterestForCity(cityId, mappedPointOfInterest);

            await cityInfoRepository.SaveChanges();

            PointOfInterestDto? createdPointOfInterest =
                mapper.Map<PointOfInterestDto>(mappedPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId,
                    pointOfInterestId = createdPointOfInterest.Id
                }, createdPointOfInterest);
        }

        //[HttpPut("{pointofinterestid}")]
        //public ActionResult<PointOfInterestDto> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        //{
        //    CityDto? city =
        //        _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //        return NotFound();

        //    PointOfInterestDto? pointOfInterestFromStore = 
        //        city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if (pointOfInterestFromStore == null)
        //        return NotFound();

        //    pointOfInterestFromStore.Name = pointOfInterest.Name;
        //    pointOfInterestFromStore.Description = pointOfInterest.Description;

        //    return NoContent();
        //}

        //[HttpPut("{pointofinterestid}")]
        //public ActionResult<PointOfInterestDto> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, 
        //    JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{
        //    CityDto? city =
        //        _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //        return NotFound();

        //    PointOfInterestDto? pointOfInterestFromStore =
        //        city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if (pointOfInterestFromStore == null)
        //        return NotFound();

        //    PointOfInterestForUpdateDto? pointOfInterestToPatch =
        //        new PointOfInterestForUpdateDto()
        //        {
        //            Name = pointOfInterestFromStore.Name,
        //            Description = pointOfInterestFromStore.Description,
        //        };

        //    patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!TryValidateModel(pointOfInterestToPatch))
        //        return BadRequest(ModelState);

        //    pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        //    pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

        //    return NoContent();
        //}

        //[HttpDelete("{pointofinterestid}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        //{
        //    CityDto? city =
        //        _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

        //    if (city == null)
        //        return NotFound();

        //    PointOfInterestDto? pointOfInterestFromStore =
        //        city.PointsOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);

        //    if (pointOfInterestFromStore == null)
        //        return NotFound();

        //    city.PointsOfInterest.Remove(pointOfInterestFromStore);
        //    return NoContent();
        //}
    }
}
