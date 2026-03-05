namespace LostColonyManager.Application.Interfaces
{
    public interface IPlanetStructureRepository
    {
        Task<bool> ExistsAsync(Guid planetId, Guid structureId);
        Task<List<PlanetStructure>> GetAllAsync();
        Task AddAsync(PlanetStructure entity);
        Task<bool> DeleteAsync(Guid planetId, Guid structureId);
    }
}
