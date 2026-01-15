namespace LostColonyManager.Application.UseCases
{
    public sealed record DeletePlanetResponse(
        string Name,
        bool Deleted
    );
}
