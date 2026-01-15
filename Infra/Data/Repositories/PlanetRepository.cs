using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class PlanetRepository : IPlanetRepository
    {
        private readonly AppDbContext _context;

        public PlanetRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return _context.Planets
                .AsNoTracking()
                .AnyAsync(r => r.Name == normalized);
        }

        public async Task AddAsync(Planet planet)
        {
            await _context.Planets.AddAsync(planet);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Planets.FirstOrDefaultAsync(p => p.Id == id);
            if (entity is null)
                return false;

            _context.Planets.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            var entity = await _context.Planets.FirstOrDefaultAsync(p => p.Name == name);
            if (entity is null)
                return false;

            _context.Planets.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
