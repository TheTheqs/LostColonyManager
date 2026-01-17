using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GenerateConsequenceRequest
    {
        public required string Name { get; init; }
        public required int MinRange { get; init; }
        public required int MaxRange { get; init; }
        public required BonusType Type { get; init; }
        public required Resource Target { get; init; }
        public required float Value { get; init; }
    }
}
