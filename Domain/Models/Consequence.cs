using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Consequence
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required int minRange { get; init; }
        public required int maxRange { get; init; }
        public required ConsequenceType Type { get; init; }
        public required ConsequenceTarget Target { get; init; }
        public float Value { get; init; }

        // Relationships
        public Guid? ChoiceId { get; init; }
    }
}
