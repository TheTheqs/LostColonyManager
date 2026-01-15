using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface IPlanetRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(Planet planet);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
