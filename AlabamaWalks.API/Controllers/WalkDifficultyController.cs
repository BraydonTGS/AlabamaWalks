using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Data;

namespace AlabamaWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _repository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get All Walk Difficulties //
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDiff = await _repository.GetAllWalkDifficultiesAsync(); 

            if (walkDiff == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<WalkDifficultyDTO>>(walkDiff); 

            return Ok(response);
        }

        // Get Walk Difficulty by Id //
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
           var walkDiff = await _repository.GetWalkDifficultyByIdAsync(id);

           if(walkDiff == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<WalkDifficultyDTO>(walkDiff); 

            return Ok(response);
        }

        // Add a New Walk Difficulty //
        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficulty(AddWalkDifficultyRequest request)
        {

            #region Before Fluent Validation
            // Validate the Request //
       /*     if (!ValidateAddWalkDifficulty(request))
            {
                return BadRequest(ModelState);
            }*/
            #endregion
            var domain = _mapper.Map<WalkDifficulty>(request);
            var walkDifficulty = await _repository.AddWalkDifficultyAsync(domain);
            var response = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyById), new {id = response.Id}, response); 
        }

        // Update Walk Difficulty //
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest request)
        {
            #region Before Fluent Validations
            // Validate the Request //
     /*       if (!ValidateUpdateWalkDifficulty(request))
            {
                return BadRequest(ModelState);
            }*/
            #endregion

            var domain = _mapper.Map<WalkDifficulty>(request); 

            var walkDifficulty = await _repository.UpdateWalkDifficultyAsync(id, domain);
            if(walkDifficulty == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = response.Id }, response);
        }

        // Delete Walk Difficulty //
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var domain = await _repository.DeleteWalkDifficultyAsync(id); 
            if (domain == null)
            {
                return NotFound();
            }
            var resposne = _mapper.Map<WalkDifficultyDTO>(domain); 
            return Ok(resposne);    
        }

        #region Private Methods
        // Validate AddWalkDifficulty //
/*        private bool ValidateAddWalkDifficulty(AddWalkDifficultyRequest request)
        {
            if(request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Walk Difficulty Data is required.");
                return false;
            }
            if(string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null or empty"); 
            }
            if (ModelState.ErrorCount> 0)
            {
                return false; 
            }

            return true; 
        }*/
        // Validate UpdateWalkDifficulty //
   /*     private bool ValidateUpdateWalkDifficulty(UpdateWalkDifficultyRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), $"Add Walk Difficulty Data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                ModelState.AddModelError(nameof(request.Code), $"{nameof(request.Code)} cannot be null or empty");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }*/
        #endregion
    }
}
