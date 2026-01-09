using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Structure
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required BonusType BonusType { get; init; }
        public float Value { get; init; }
        public Dictionary<Resource, int> Cost { get; init; } = new();
        public Dictionary<Resource, int> Requeriments { get; init; } = new();
        public WorldAspect Type { get; set; }

        //Relationships
        public Guid ReferenceId { get; init; }

        // Constructors
        public Structure(){}
    }
}
