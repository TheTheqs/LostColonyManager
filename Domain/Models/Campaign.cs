namespace LostColonyManager.Domain.Models
{
    public class Campaign
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
