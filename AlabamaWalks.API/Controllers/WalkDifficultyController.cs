using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        public async Task<IActionResult> AddWalkDifficulty(AddWalkDifficultyRequest request)
        {
            var domain = _mapper.Map<WalkDifficulty>(request);
            var walkDifficulty = await _repository.AddWalkDifficultyAsync(domain);
            var response = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);
            return CreatedAtAction(nameof(GetWalkDifficultyById), new {id = response.Id}, response); 
        }

        // Update Walk Difficulty //
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] UpdateWalkDifficultyRequest request)
        {
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
    }
}
