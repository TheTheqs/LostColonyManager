namespace LostColonyManager.Domain.Models
{
    public class Planet
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int Category { get; set; } = 0;

        // Relationships
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<PlanetStructure> Structures { get; set; } = new List<PlanetStructure>();


        // Constructors
        public Planet()
        {
        }
        public Planet(Guid id, string name, int category)
        {
            Id = id;
            Name = name;
            Category = category;
        }
    }
}
