using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using AutoMapper;
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
        private readonly IMapper _mapper;

        // Injecting the RegionRepository through the Constructor //
        public RegionsController(IRegionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get All Regions //
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _repository.GetAllRegionsAsync();
            var response = _mapper.Map<List<Models.DTO.Region>>(regions); 
    
            return Ok(response); 
        }

        // Get Region by Id //
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _repository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<Models.DTO.Region>(region);

            return Ok(response);
        }
    }
}
