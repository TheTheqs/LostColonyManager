namespace LostColonyManager.Application.UseCases;

public sealed record DeleteCampaignResponse(
        string Name,
        bool Deleted
    );

