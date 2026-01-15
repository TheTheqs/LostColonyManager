using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Application.UseCases
{
    public sealed class RegisterCampaignUseCase
    {
        private readonly ICampaignRepository _repository;

        public RegisterCampaignUseCase(ICampaignRepository repository)
        {
            _repository = repository;
        }
        public async Task<RegisterCampaignResponse> ExecuteAsync(
            RegisterCampaignRequest request
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Unique name
            var nameAlreadyExists = await _repository.ExistsByNameAsync(request.Name);
            if (nameAlreadyExists)
                throw new InvalidOperationException($"A campaign with name '{request.Name}' already exists.");

            // Build Entity
            var campaign = new Campaign(
                id: Guid.NewGuid(),
                name: request.Name.Trim()
            );

            // Persist
            await _repository.AddAsync(campaign);

            // Response
            return new RegisterCampaignResponse(
                campaign.Id,
                campaign.Name
            );
        }
    }
}
