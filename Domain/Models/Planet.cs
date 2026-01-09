namespace LostColonyManager.Domain.Models
{
    public class Planet
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Cateory { get; init; }

        // Relationships
        public ICollection<Guid> EventsId { get; init; } = new List<Guid>();

        // Constructors
        public Planet()
        {
        }
    }
}
