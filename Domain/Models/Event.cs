using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public WorldAspect Type { get; set; }

        // Relationships
        public Guid ReferenceId { get; set; }
        public List<Guid> ChoicesIds { get; private set; } = new ();

        // Constructors
        public Event()
        {
        }
    }
}
