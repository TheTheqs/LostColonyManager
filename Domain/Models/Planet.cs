namespace LostColonyManager.Domain.Models
{
    public class Planet
    {
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public required int Category { get; set; }

        // Relationships
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<PlanetStructure> Structures { get; set; } = new List<PlanetStructure>();


        // Constructors
        public Planet()
        {
        }
    }
}
