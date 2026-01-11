namespace LostColonyManager.Application.UseCases.RaceUseCases.Register
{
    public sealed record RegisterRaceResponse(
        Guid Id,
        string Name,
        int Power,
        int Technology,
        int Diplomacy,
        int Culture
    );
}
