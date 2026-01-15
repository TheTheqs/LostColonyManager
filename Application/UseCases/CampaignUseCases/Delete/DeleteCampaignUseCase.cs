using LostColonyManager.Application.Interfaces;

namespace LostColonyManager.Application.UseCases
{
    public sealed class DeleteCampaignUseCase
    {
        private readonly ICampaignRepository _repository;

        public DeleteCampaignUseCase(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteCampaignResponse> ExecuteAsync(
            DeleteCampaignRequest request
        )
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.Name == "")
                throw new ArgumentException("Name is required.", nameof(request.Name));

            var deleted = await _repository.DeleteByNameAsync(request.Name);

            if (!deleted)
                throw new KeyNotFoundException($"Campaign with the name '{request.Name}' was not found.");

            return new DeleteCampaignResponse(request.Name, deleted);
        }
    }
}
