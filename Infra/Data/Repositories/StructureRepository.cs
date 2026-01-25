using LostColonyManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class StructureRepository : IStructureRepository
    {
        private readonly AppDbContext _context;

        public StructureRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = name.Trim();

            return _context.Structures
                .AsNoTracking()
                .AnyAsync(s => s.Name == normalized);
        }
        public Task<Structure?> GetByIdAsync(Guid id)
        {
            return _context.Structures
                .AsNoTracking()
                .AsSplitQuery()
                .Include(s => s.Planets)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public Task<List<Structure>> GetAllAsync()
        {
            return _context.Structures
                .AsNoTracking()
                .Include(s => s.Planets)
                .ToListAsync();
        }
        public async Task AddAsync(Structure structure)
        {
            await _context.Structures.AddAsync(structure);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Structures.FirstOrDefaultAsync(s => s.Id == id);
            if (entity is null)
                return false;

            _context.Structures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var normalized = name.Trim();

            var entity = await _context.Structures
                .FirstOrDefaultAsync(p => p.Name == normalized);

            if (entity is null)
                return false;

            _context.Structures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
