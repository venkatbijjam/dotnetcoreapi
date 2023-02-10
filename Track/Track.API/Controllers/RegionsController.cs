using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Track.API.Models.Domain;
using Track.API.Repositories;

namespace Track.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

            //return DTO regions instead of model regions

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };

            //    regionsDTO.Add(regionDTO);
            //}) ;
            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var regions = await regionRepository.GetAsync(id);
            if(regions == null)
            {
                return NotFound();
            }
            var regionsDTO = mapper.Map<Models.DTO.Region>(regions);
            return Ok(regionsDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest regionrequest)
        {
            //Reqest DTO to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = regionrequest.Code,
                Area = regionrequest.Area,
                Lat = regionrequest.Lat,
                Long = regionrequest.Long,
                Name = regionrequest.Name,
                Population = regionrequest.Population
            };

            //Pass Details to Repo
            region = await regionRepository.AddAsync(region);

            //Covert o DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionByIdAsync(Guid id)
        {
            //get region
            var regions = await regionRepository.DeleteAsync(id);
            if (regions == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(regions);
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]Models.DTO.UpdateRegionRequest updateregion)
        {
            //covert to domain
         
            //Reqest DTO to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = updateregion.Code,
                Area = updateregion.Area,
                Lat = updateregion.Lat,
                Long = updateregion.Long,
                Name = updateregion.Name,
                Population = updateregion.Population
            };

            region = await regionRepository.UpdateAsync(id, region);
            if (region == null)
            {
                return NotFound();
                
            }

            //Convert to dto


            //var regionDTO = new Models.DTO.Region
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Area = region.Area,
            //    Lat = region.Lat,
            //    Long = region.Long,
            //    Name = region.Name,
            //    Population = region.Population

            //};

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }
    }
}
