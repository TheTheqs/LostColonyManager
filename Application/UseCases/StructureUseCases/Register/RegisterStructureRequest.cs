using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Application.UseCases;

public sealed class RegisterStructureRequest
{
    public required string Name { get; init; }
    public required BonusType BonusType { get; init; }
    public float Value { get; init; }
    public bool IncreasePerTurn { get; init; }
    public List<Resource> BonusTargets { get; init; } = new();
    public required WorldAspect Type { get; init; }
    public Dictionary<Resource, int> Cost { get; init; } = new();
    public Dictionary<Resource, int> Requeriments { get; init; } = new();
}
