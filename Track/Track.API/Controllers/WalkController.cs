using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using Track.API.Repositories;

namespace Track.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WalkController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // get walk from repo
            var walks = await walkRepository.GetAllAsync();

            //convert to DTO
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walksDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            //get walk
            var walk = await walkRepository.GetAsync(id);

            if (walk == null)
            {
                return NotFound();

            }

            //convert to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] Models.DTO.AddWalkRequest walkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = walkRequest.Name,
                Length = walkRequest.Length,
                RegionId = walkRequest.RegionId,
                WalkDifficultyId = walkRequest.WalkDifficultyId
            };

            walk = await walkRepository.AddAsync(walk);

            //DTO 
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkDTO.Id }, walkDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // var walkDomain = mapper.Map<Models.Domain.Walk>(updateWalkRequest);

            var walkDomain = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound("Walk is not found");
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound("not able to delete");
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }
    }
}
