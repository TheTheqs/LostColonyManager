using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface ICampaignRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<Campaign?> GetByIdAsync(Guid id);
        Task<bool> ExistsByIdAsync (Guid id);
        Task<List<Campaign>> GetAllAsync();
        Task AddAsync(Campaign campaign);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
