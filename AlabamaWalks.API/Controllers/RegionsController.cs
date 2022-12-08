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

        // Get All Regions // Dont need to Worry about Validation - Not accepting anything from the Client //
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _repository.GetAllRegionsAsync();
            var response = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(response);
        }

        // Get Region by Id // The Route is protecting what is being passed in by speficing a guid // 
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
            // Validate The Request // 
            // Before Fluent Validation //
       /*     var isValid = ValidateAddRegion(addRegionRequest);
            if (!isValid)
            {
                // Bad Request Will Automatically Bind the BadRequest Errors to the ModelState //
                return BadRequest(ModelState); 
            }*/
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
            // Validate the incoming request // 
            if (!ValidateUpdateRegion(updateRegion))
            {
                return BadRequest(ModelState);
            }
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


        #region Private Methods

        private bool ValidateAddRegion(AddRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Region Data is required.");
                return false; 
            }
            if (string.IsNullOrEmpty(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null empty or white space.");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null empty or white space.");
            }  
            if(request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} cannot be less than or equal to zero.");
            }
            if (request.Long == 0)
            {
                ModelState.AddModelError(nameof(request.Long), $"{nameof(request.Long)} cannot be equal to zero.");
            }
            if (request.Lat == 0)
            {
                ModelState.AddModelError(nameof(request.Lat), $"{nameof(request.Lat)} cannot be equal to zero.");  
            }
            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount> 0)
            {
                return false; 
            }

            return true; 
        }

        private bool ValidateUpdateRegion(UpdateRegionRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Update Region Data is required.");
                return false;
            }
            if (string.IsNullOrEmpty(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null empty or white space.");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null empty or white space.");
            }
            if (request.Area <= 0)
            {
                ModelState.AddModelError(nameof(request.Area), $"{nameof(request.Area)} cannot be less than or equal to zero.");
            }
            if (request.Long <= 0)
            {
                ModelState.AddModelError(nameof(request.Long), $"{nameof(request.Long)} cannot be equal to zero.");
            }
            if (request.Lat <= 0)
            {
                ModelState.AddModelError(nameof(request.Lat), $"{nameof(request.Lat)} cannot be equal to zero.");
            }
            if (request.Population < 0)
            {
                ModelState.AddModelError(nameof(request.Population), $"{nameof(request.Population)} cannot be less than zero.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

    }
}
