using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlabamaWalks.API.Controllers
{ 
    // Region endpoint maps to the Region Controller //
    [Route("[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repository;
        
        // Injecting the RegionRepository through the Constructor //
        public RegionsController(IRegionRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = _repository.GetAllRegions();
            var regionsDTO = new List<Models.DTO.Region>();
            // Return DTO Regions //
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population

                };
                regionsDTO.Add(regionDTO);
            });
            return Ok(regionsDTO); 
        }
    }
}
