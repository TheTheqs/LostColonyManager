using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Application.Interfaces
{
    public interface IRaceRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByTraitsAsync(RaceTraits traits);
        Task<Race?> GetByIdAsync(Guid id);
        Task<List<Race>> GetAllAsync();
        Task AddAsync(Race race);
        Task<bool> DeleteByIdAsync(Guid id);

        Task<bool> DeleteByNameAsync(string name);
    }
}
