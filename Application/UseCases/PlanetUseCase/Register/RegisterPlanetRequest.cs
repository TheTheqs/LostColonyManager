namespace LostColonyManager.Application.UseCases;

public sealed class RegisterPlanetRequest
{
    public required string Name { get; init; }
    public required int Category { get; init; }
}
