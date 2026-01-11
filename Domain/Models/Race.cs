using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Domain.Models
{
    public class Race
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public RaceTraits Traits { get; init; }

        // Relationships
        public List<Guid> EventsIds { get; private set; } = new ();

        // Constructors
        public Race() { }

        public Race (string name, RaceTraits traits)
        {
            Name = name;
            Traits = traits;
        }
    }
}
