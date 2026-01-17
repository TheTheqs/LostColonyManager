using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Application.UseCases
{
    public class GenerateChoiceRequest
    {
        public required string Name { get; init; }
        public required ChoiceBonusType BonusType { get; init; }
        public required List<GenerateConsequenceRequest> Consequences { get; init; }
    }
}
