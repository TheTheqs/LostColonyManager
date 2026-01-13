using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Consequence
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public required int MinRange { get; set; }
        public required int MaxRange { get; set; }
        public required BonusType Type { get; set; }
        public required Resource Target { get; set; }
        public float Value { get; set; }

        // Relationships
        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; } = null!;

        // Constructors
        public Consequence()
        {
        }
    }
}
