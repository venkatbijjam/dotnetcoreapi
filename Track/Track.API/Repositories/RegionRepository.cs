using Microsoft.EntityFrameworkCore;
using Track.API.Data;
using Track.API.Models.Domain;

namespace Track.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly TrackDbContext trackDbContext;

        public RegionRepository(TrackDbContext trackDbContext)
        {
            this.trackDbContext = trackDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await trackDbContext.AddAsync(region);
            await trackDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            //find id in database
            var region= await GetAsync(id);
            if (region == null) { return null; }
            trackDbContext.Regions.Remove(region);
            await trackDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await trackDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
           return await trackDbContext.Regions.FirstOrDefaultAsync( x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id,Region region)
        {
            //find id in database
            var regionFound = await GetAsync(id);
            if (regionFound == null) { return null; }

            regionFound.Code= region.Code;
            regionFound.Name= region.Name;
            regionFound.Area= region.Area;  
            regionFound.Lat= region.Lat;
            regionFound.Long= region.Long;
            regionFound.Population= region.Population;

            await trackDbContext.SaveChangesAsync();
            return regionFound;
        }
    }
}
