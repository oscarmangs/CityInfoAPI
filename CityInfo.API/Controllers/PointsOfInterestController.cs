using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using CityInfo.API.Entities; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CityInfo.API.Controllers
{
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest/")]
    [ApiController]
    [Authorize(Policy = "MustBeFromStockholm")]
    [ApiVersion("2.0")]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, 
            IMailService localMailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = localMailService ?? throw new ArgumentException(nameof(localMailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing the points of interest!");
                return NotFound(); 
            }

            IEnumerable<Entities.PointOfInterest> pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId); 

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }
        

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound(); 
            }

            Entities.PointOfInterest pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound(); 
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));   
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound(); 
            }

             var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var createPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new {cityId = cityId, pointOfInterestId = createPointOfInterestToReturn.Id},
                createPointOfInterestToReturn); 
        }


        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointsOfInterestForUpdateDto pointOfInterest)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromRepository = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId); 
            if (pointOfInterestFromRepository == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestFromRepository); 

            await _cityInfoRepository.SaveChangesAsync();   

            return NoContent();
        }


        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            JsonPatchDocument<PointsOfInterestForUpdateDto> patchDocument)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromRepository = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestFromRepository == null)
            {
                return NotFound();
            }


            var pointOfInterestToPatch = _mapper.Map<PointsOfInterestForUpdateDto>(pointOfInterestFromRepository); 

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);


            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (!TryValidateModel(pointOfInterestToPatch)) { return BadRequest(ModelState); }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestFromRepository);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromRepository = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestFromRepository == null)
            {
                return NotFound();
            }

           _cityInfoRepository.DeletePointOfInterest(pointOfInterestFromRepository);
           await _cityInfoRepository.SaveChangesAsync(); 

           // selectedCity.PointsOfInterest.Remove(selectedPointOfInterest);
            _mailService.Send("Point of interest was deleted", $"Point of interest {pointOfInterestFromRepository.Name} with id {pointOfInterestFromRepository.Id} was deleted.");
            return NoContent();
        }

    }

}
