using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases;

public sealed record GetRaceResponse(
        List<RaceDto> Races
    );
