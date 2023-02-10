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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await trackDbContext.Regions.ToListAsync();
        }
    }
}
