using Microsoft.EntityFrameworkCore;
using Track.API.Models.Domain;

namespace Track.API.Data
{
    public class TrackDbContext :DbContext
    {

        public TrackDbContext(DbContextOptions<TrackDbContext> options):base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
