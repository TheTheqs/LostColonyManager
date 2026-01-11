using LostColonyManager.Domain.Interfaces;
using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class RaceRepository : IRaceRepository
    {
        private readonly AppDbContext _context;

        public RaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return _context.Races
                .AsNoTracking()
                .AnyAsync(r => r.Name == normalized);
        }

        public Task<bool> ExistsByTraitsAsync(RaceTraits traits)
        {
            return _context.Races
                .AsNoTracking()
                .AnyAsync(r => r.Traits == traits);
        }

        public async Task AddAsync(Race race)
        {
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Races.FirstOrDefaultAsync(r => r.Id == id);
            if (entity is null)
                return false;

            _context.Races.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            var entity = await _context.Races.FirstOrDefaultAsync(r => r.Name == name);
            if (entity is null)
                return false;

            _context.Races.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
