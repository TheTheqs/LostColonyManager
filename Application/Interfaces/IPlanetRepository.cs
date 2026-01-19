using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface IPlanetRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<Planet?> GetByIdAsync(Guid id);
        Task<List<Planet>> GetAllAsync();
        Task AddAsync(Planet planet);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
