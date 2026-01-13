using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;

public class Structure
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required BonusType BonusType { get; init; }
    public float Value { get; init; }
    public Dictionary<Resource, int> Cost { get; init; } = new();
    public Dictionary<Resource, int> Requeriments { get; init; } = new();
    public WorldAspect Type { get; set; }

    public ICollection<PlanetStructure> Planets { get; set; } = new List<PlanetStructure>();

    public Structure() { }
}

public class PlanetStructure
{
    public Guid PlanetId { get; set; }
    public Planet Planet { get; set; } = null!;

    public Guid StructureId { get; set; }
    public Structure Structure { get; set; } = null!;
}
