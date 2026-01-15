namespace LostColonyManager.Application.UseCases;

public sealed record RegisterPlanetResponse(
        Guid Id,
        string Name,
        int Category
    );


