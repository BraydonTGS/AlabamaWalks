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

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            // Fetch From DB - Convert to DTO - Return //
            var walks = await _repository.GetAllWalksAsync();
            var response = _mapper.Map<List<WalkDTO>>(walks); 
            return Ok(response);
        }


    }
}
