namespace LostColonyManager.Application.UseCases.RaceUseCases.Register;
public sealed class RegisterRaceRequest
{
    public required string Name { get; init; }

    public int Power { get; init; }
    public int Technology { get; init; }
    public int Diplomacy { get; init; }
    public int Culture { get; init; }
}