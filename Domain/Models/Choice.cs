using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Choice
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required ChoiceBonusType BonusType { get; init; }

        // Relationships
        public Guid? EventId { get; init; }
        public ICollection<Guid> ConsequencesId { get; init; } = new List<Guid>();
    }
}
