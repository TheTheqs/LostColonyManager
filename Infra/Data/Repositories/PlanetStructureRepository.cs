using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class PlanetStructureRepository : IPlanetStructureRepository
    {
        private readonly AppDbContext _context;

        public PlanetStructureRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsAsync(Guid planetId, Guid structureId)
        {
            if (planetId == Guid.Empty || structureId == Guid.Empty)
                return Task.FromResult(false);

            return _context.PlanetStructures
                .AsNoTracking()
                .AnyAsync(x => x.PlanetId == planetId && x.StructureId == structureId);
        }

        public async Task AddAsync(PlanetStructure entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.PlanetId == Guid.Empty)
                throw new ArgumentException("PlanetId is required.", nameof(entity.PlanetId));

            if (entity.StructureId == Guid.Empty)
                throw new ArgumentException("StructureId is required.", nameof(entity.StructureId));

            await _context.PlanetStructures.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid planetId, Guid structureId)
        {
            if (planetId == Guid.Empty || structureId == Guid.Empty)
                return false;

            var entity = await _context.PlanetStructures
                .FirstOrDefaultAsync(x => x.PlanetId == planetId && x.StructureId == structureId);

            if (entity is null)
                return false;

            _context.PlanetStructures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
