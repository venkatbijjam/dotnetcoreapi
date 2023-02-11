using Microsoft.EntityFrameworkCore;
using Track.API.Data;
using Track.API.Models.Domain;

namespace Track.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly TrackDbContext trackDbContext;

        public WalkRepository(TrackDbContext trackDbContext)
        {
            this.trackDbContext = trackDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await trackDbContext.Walks
                .Include(a=>a.Region)
                .Include(a=>a.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
          return await trackDbContext.Walks
                .Include(a=>a.Region)
                .Include(a=>a.WalkDifficulty)
                .FirstOrDefaultAsync(a=>a.Id == id);
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await trackDbContext.AddAsync(walk);
            await trackDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walkFound=await GetAsync(id);
            if (walkFound != null)
            {
                walkFound.Name = walk.Name;
                walkFound.Length = walk.Length;
                walkFound.WalkDifficultyId = walk.WalkDifficultyId;
                walkFound.RegionId  = walk.RegionId;
                await trackDbContext.SaveChangesAsync();
                return walkFound;
            }
            return null;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walkFound = await GetAsync(id);
            if (walkFound != null)
            {
                trackDbContext.Walks.Remove(walkFound);
                await trackDbContext.SaveChangesAsync();
                return walkFound;

            }
            return null;
        }
    }
}
