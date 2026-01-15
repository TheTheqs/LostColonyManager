using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class CampaignRepository : ICampaignRepository
    {
        private readonly AppDbContext _context;

        public CampaignRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return _context.Campaigns
                .AsNoTracking()
                .AnyAsync(r => r.Name == normalized);
        }

        public async Task AddAsync(Campaign campaign)
        {
            await _context.Campaigns.AddAsync(campaign);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null)
                return false;

            _context.Campaigns.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            var entity = await _context.Campaigns.FirstOrDefaultAsync(c => c.Name == name);
            if (entity is null)
                return false;

            _context.Campaigns.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
