using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.Interfaces
{
    public interface ICampaignRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task AddAsync(Campaign campaign);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> DeleteByNameAsync(string name);
    }
}
