using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface IStructureRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<Structure?> GetByIdAsync(Guid id);
        Task<List<Structure>> GetAllAsync();
        Task AddAsync(Structure structure);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
