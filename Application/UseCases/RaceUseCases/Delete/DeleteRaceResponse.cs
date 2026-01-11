namespace LostColonyManager.Application.UseCases
{
    public sealed record DeleteRaceResponse(
        string Name,
        bool Deleted
    );
}
