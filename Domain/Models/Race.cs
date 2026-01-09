using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Domain.Models
{
    public class Race
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public RaceTraits Traits { get; init; }

        // Relationships
        public List<Guid> EventsIds { get; private set; } = new ();

        // Constructors
        public Race() { }
    }
}
