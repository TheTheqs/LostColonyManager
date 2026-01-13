using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Choice
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public required ChoiceBonusType BonusType { get; set; }

        // Relationships
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
        public ICollection<Consequence> Consequences { get; set; } = new List<Consequence>();

        // Constructors
        public Choice()
        {
        }
    }
}
