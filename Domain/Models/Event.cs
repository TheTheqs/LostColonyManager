using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Domain.Models
{
    public class Event
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public WorldAspect Type { get; set; }

        // Relationships
        // Owner (Only One)
        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public Guid? RaceId { get; set; }
        public Race? Race { get; set; }

        public Guid? PlanetId { get; set; }
        public Planet? Planet { get; set; }
        public ICollection<Choice> Choices { get; private set; } = new List<Choice>();

        // Constructors
        public Event()
        {
        }
    }
}
