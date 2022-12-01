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
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = _repository.GetAllRegionsAsync();
            var regionsDTO = _mapper.Map<List<Models.DTO.Region>>(regions); 
            
       
            return Ok(regionsDTO); 
        }
    }
}
