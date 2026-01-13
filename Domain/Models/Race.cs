using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Domain.Models
{
    public class Race
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public RaceTraits Traits { get; init; }

        // Relationships
        public ICollection<Event> Events { get; private set; } = new List<Event>();

        // Constructors
        public Race() { }

        public Race (Guid id, string name, RaceTraits traits)
        {
            Id = id;
            Name = name;
            Traits = traits;
        }
    }
}
