namespace LostColonyManager.Domain.Models
{
    public class Campaign
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;

        // Relationships
        public ICollection<Event> Events { get; set; } = new List<Event>();

        // Constructors
        public Campaign() { }
        public Campaign(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
