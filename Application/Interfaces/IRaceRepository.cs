using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Domain.Interfaces
{
    public interface IRaceRepository
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
        Task<bool> ExistsByTraitsAsync(RaceTraits traits, CancellationToken ct = default);

        Task AddAsync(Race race, CancellationToken ct = default);
    }
}
