using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regiondomainMoels = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);
            if(regiondomainMoels == null)
            {
                return null;
            }
            dbContext.Regions.Remove(regiondomainMoels);
            await dbContext.SaveChangesAsync();

            return regiondomainMoels;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingVilla = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingVilla == null)
            {
                return null;
            }
            existingVilla.Code = region.Code;
            existingVilla.Name = region.Name;
            existingVilla.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingVilla;
        }
    }
}
