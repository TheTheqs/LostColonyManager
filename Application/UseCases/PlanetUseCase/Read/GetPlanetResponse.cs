using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases;

public sealed record GetPlanetResponse(
        List<PlanetDto> Planets
    );
