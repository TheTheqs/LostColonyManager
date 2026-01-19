using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.Mapping;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GetCampaignUseCase
    {
        private readonly ICampaignRepository _repository;

        public GetCampaignUseCase(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCampaignResponse> ExecuteAsync(
            GetCampaignRequest request
        )
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result is null)
            {
                var campaigns = await _repository.GetAllAsync();
                var campaignDtos = campaigns.Select(c => c.ToDto()).ToList();
                return new GetCampaignResponse(Campaigns: campaignDtos);
            }
            return new GetCampaignResponse(new List<CampaignDto>() {result.ToDto()});
        }
    }
}
