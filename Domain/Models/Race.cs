namespace LostColonyManager.Domain.Models
{
    public class Race
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public ValuesObjects.RaceTraits Traits { get; init; }

        // Relationships
        public ICollection<Guid> EventsId { get; init; } = new List<Guid>();

        // Constructors
        public Race() { }
    }
}
