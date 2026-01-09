using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public EventType Type { get; set; }

        // Relationships
        public Guid? ReferenceId { get; set; }
        public ICollection<Guid> Choices { get; set; } = new List<Guid>();

        // Constructors
        public Event()
        {
        }

    }
}
