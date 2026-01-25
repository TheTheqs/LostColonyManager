using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Application.UseCases;

public sealed record RegisterStructureResponse(
    Guid Id,
    string Name,
    BonusType BonusType,
    List<Resource> BonusTarget,
    bool IncreasePorTurn,
    float Value,
    WorldAspect Type
);
