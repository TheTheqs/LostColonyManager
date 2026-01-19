using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases;

public sealed record GetCampaignResponse(
        List<CampaignDto> Campaigns
    );
