using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Choice
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public ChoiceBonusType BonusType { get; set; }

        // Relationships
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
        public ICollection<Consequence> Consequences { get; set; } = new List<Consequence>();

        // Constructors
        public Choice()
        {
        }
        public Choice(Guid id, string name, ChoiceBonusType bonusType, List<Consequence> consequences)
        {
            Id = id;
            Name = name;
            BonusType = bonusType;
            Consequences = consequences;
        }
    }
}
