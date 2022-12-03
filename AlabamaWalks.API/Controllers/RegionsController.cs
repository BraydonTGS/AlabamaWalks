using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
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
            var response = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(response);
        }

        // Get Region by Id //
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionById")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _repository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<RegionDTO>(region);

            return Ok(response);
        }

        // Create a New Region // 
        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest)
        {
            // Request(DTO) Pass to Domain //
            var region = _mapper.Map<Region>(addRegionRequest);

            // For Reference Purposes - Before AutoMapper // 
            /*     var region = new Region()
             {
                 Code= addRegionRequest.Code,
                 Area= addRegionRequest.Area,
                 Lat= addRegionRequest.Lat,
                 Long= addRegionRequest.Long,
                 Name = addRegionRequest.Name,
                 Population= addRegionRequest.Population

             }; */

            // Domain Pass to Repo //
            var response = await _repository.AddRegionAsync(region);

            // Domain Convert to DTO Send to Client //
            var regionDTO = _mapper.Map<RegionDTO>(region);

            // Pass CreatedAtAction to the Client - HTTP 201 - Client Knows Save was Successful //
            // Uses the GetRegioinsById Action - Passing the Id - Passing the Object as well //
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }

        // Delete Region //
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            // Convert Response back to DTO //
            var region = await _repository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            // Map to DTO //
            var regionDTO = _mapper.Map<RegionDTO>(region);

            // Return Response to Client // 
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        // Id is coming FromRoute, UpdateRegionRequest is coming FromBody
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegion)
        {
            // Convert DTO to Domain // 
            var region = _mapper.Map<Region>(updateRegion);

            // Update Region using Repository //
            var response = await _repository.UpdateRegionAsync(id, region);
            if (response == null)
            {
                return NotFound();
            }

            // Convert Domain To DTO //
            var regionDTO = _mapper.Map<RegionDTO>(response); 

            return Ok(regionDTO);
            
        }
       
    }
}
