using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Consequence
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        public BonusType Type { get; set; }
        public Resource Target { get; set; }
        public float Value { get; set; }

        // Relationships
        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; } = null!;

        // Constructors
        public Consequence()
        {
        }
        public Consequence(
            Guid id,
            string name,
            int minRange,
            int maxRange,
            BonusType type,
            Resource target,
            float value
        )
        {
            Id = id;
            Name = name;
            MinRange = minRange;
            MaxRange = maxRange;
            Type = type;
            Target = target;
            Value = value;
        }
    }
}
