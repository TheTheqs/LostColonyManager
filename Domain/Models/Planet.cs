namespace LostColonyManager.Domain.Models
{
    public class Planet
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Category { get; init; }

        // Relationships
        public List<Guid> EventsIds { get; private set; } = new ();

        // Constructors
        public Planet()
        {
        }
    }
}
