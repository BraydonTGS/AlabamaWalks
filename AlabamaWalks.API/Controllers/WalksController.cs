using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlabamaWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _repository;
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        // CTOR Injection //
        // Injecting WalkDiff and Region repo so that I can use for my Validations //
        public WalksController(IWalkRepository repository, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _repository = repository;
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        // Get All Walks //
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            // Fetch From DB - Convert to DTO - Return //
            var walks = await _repository.GetAllWalksAsync();
            if(walks == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<List<WalkDTO>>(walks); 
            return Ok(response);
        }

        // Get Walk By Id //
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
           var walk = await _repository.GetWalkByIdAsync(id);
           var responce = _mapper.Map<WalkDTO>(walk);
           return Ok(responce);
        }

        // Create a New Walk //
        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] AddWalkRequest request)
        {
            // Validate the Request //
            if (!(await ValidateAddWalk(request)))
            {
                return BadRequest(ModelState); 
            }
            // Convert DTO to Domain //
            var walkDomain = _mapper.Map<Walk>(request);

            // Pass Domain to Repo //
            var walk = await _repository.AddWalkAsync(walkDomain);
            if(walk == null)
            {
                return NotFound();
            }

            // Convert Domain to DTO //
            var response = _mapper.Map<WalkDTO>(walk);

            // Send Response to Client //
            // Pass CreatedAtAction to the Client - HTTP 201 - Client Knows Save was Successful //
            // Uses the GetRegioinsById Action - Passing the Id - Passing the Object as well //
            return CreatedAtAction(nameof(GetWalkById), new {id = response.Id}, response);

        }

        // Update a Walk //
        [HttpPut]
        [Route("{id:guid}")]
        // Id is coming FromRoute, UpdateWalkRequest is coming FromBody
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody]UpdateWalkRequest request)
        {
            // Validate the Request //
            if(!(await ValidateUpdateWalk(request)))
            {
                return BadRequest(ModelState);  
            }
            // Convert DTO to Domain //
            var walkDomain = _mapper.Map<Walk>(request);
            // Pass Domain to Repo //
            var walk = await _repository.UpdateWalkAsync(id, walkDomain); 
            if(walk == null)
            {
                return NotFound();
            }
            // Convet To DTO //
            var response = _mapper.Map<WalkDTO>(walk);
            // Send Response to Client //
            return CreatedAtAction(nameof(GetWalkById), new { id = response.Id }, response); 
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await _repository.DeleteWalkAsync(id);
            if (walk == null)
            {
                return NotFound(); 
            }
            var response = _mapper.Map<WalkDTO>(walk); 
            return Ok(response);
        }

        #region Private Methods

        // Validate Add Walk //
        private async Task <bool> ValidateAddWalk(AddWalkRequest request)
        {
            if(request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Walk Data is required.");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null empty or white space.");
            }
            if (request.Length <= 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} cannot be less than or equal to zero.");
            }

            // Brought in the Region Repository via CTOR So that we can Validate if Region Exists //
            var region = await _regionRepository.GetRegionByIdAsync(request.RegionId); 
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is invalid.");
            }

            // Brought in the WalkDiff Repository via CTOR So that we can Validate if the WalkDiff Exists //
            var walkDiff = await _walkDifficultyRepository.GetWalkDifficultyByIdAsync(request.WalkDifficultyId);
            if(walkDiff == null)
            {
                ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is invalid."); 
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true; 
        }

        // Validate Update Walk //
        private async Task<bool> ValidateUpdateWalk(UpdateWalkRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Walk Data is required.");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} cannot be null empty or white space.");
            }
            if (request.Length <= 0)
            {
                ModelState.AddModelError(nameof(request.Length), $"{nameof(request.Length)} cannot be less than or equal to zero.");
            }

            // Brought in the Region Repository via CTOR So that we can Validate if Region Exists //
            var region = await _regionRepository.GetRegionByIdAsync(request.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(request.RegionId), $"{nameof(request.RegionId)} is invalid.");
            }

            // Brought in the WalkDiff Repository via CTOR So that we can Validate if the WalkDiff Exists //
            var walkDiff = await _walkDifficultyRepository.GetWalkDifficultyByIdAsync(request.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(request.WalkDifficultyId), $"{nameof(request.WalkDifficultyId)} is invalid.");
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
