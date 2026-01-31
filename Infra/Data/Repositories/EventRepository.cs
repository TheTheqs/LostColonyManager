using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data.Repositories
{
    public sealed class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Task.FromResult(false);

            return _context.Events
                .AsNoTracking()
                .AnyAsync(e => e.Id == id);
        }

        public Task<Event?> GetByIdAsync(Guid id)
        {
            return _context.Events
                .AsNoTracking()
                .AsSplitQuery()
                .Include(e => e.Campaign)
                .Include(e => e.Race)
                .Include(e => e.Planet)
                .Include(e => e.Choices)
                    .ThenInclude(c => c.Consequences)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<Event>> GetAllAsync()
        {
            return _context.Events
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (entity is null)
                return false;

            _context.Events.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
