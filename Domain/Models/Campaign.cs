namespace LostColonyManager.Domain.Models
{
    public class Campaign
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }

        // Relationships
        public List<Guid> EventsIds { get; private set; } = new();

        // Constructors
        public Campaign() { }
    }
}
