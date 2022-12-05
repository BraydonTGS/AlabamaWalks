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
        private readonly IMapper _mapper;

        // CTOR Injection //
        public WalksController(IWalkRepository repository, IMapper mapper)
        {
            _repository = repository;
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

    }
}
