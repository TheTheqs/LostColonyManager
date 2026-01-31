using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface IEventRepository
    {
        Task<bool> ExistsByIdAsync(Guid id);
        Task<Event?> GetByIdAsync(Guid id);
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event eventEntity);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
