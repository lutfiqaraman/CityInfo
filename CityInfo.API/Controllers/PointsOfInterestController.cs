using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

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
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, 
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
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

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, 
            int pointOfInterestId, 
            PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await cityInfoRepository.IsCityExist(cityId))
                return NotFound();

            PointOfInterest? pointOfInterestEntity = 
                await cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
                NotFound();

            mapper.Map(pointOfInterest, pointOfInterestEntity);
            
            await cityInfoRepository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if (!await cityInfoRepository.IsCityExist(cityId))
                return NotFound();

            PointOfInterest? pointOfInterestEntity = 
                await cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
                NotFound();

            PointOfInterestForUpdateDto? pointOfInterestToPatch = 
                mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(pointOfInterestToPatch))
                return BadRequest(ModelState);

            mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            await cityInfoRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await cityInfoRepository.IsCityExist(cityId))
                return NotFound();

            var pointOfInterestEntity = 
                await cityInfoRepository.GetPointOfInterestForCity(cityId, pointOfInterestId);

            if (pointOfInterestEntity == null)
                NotFound();
            else
                cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            await cityInfoRepository.SaveChanges();

            return NoContent();
        }
    }
}
