using LostColonyManager.Application.UseCases;
using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GenerateChoiceUseCase
    {
        public GenerateConsequenceUseCase _consequenceUseCase;

        public GenerateChoiceUseCase(GenerateConsequenceUseCase consequenceUseCase)
        {
            _consequenceUseCase = consequenceUseCase;
        }
        public GenerateChoiceResponse ExecuteAsync(
            GenerateChoiceRequest request
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Validate Bonus Type
            if (!Enum.IsDefined(typeof(ChoiceBonusType), request.BonusType))
            {
                throw new ArgumentException("Invalid BonusType");
            }

            // Generate Consequences
            var consequences = new List<Consequence>();
            var lastMaxRange = int.MinValue;

            foreach (var consequenceRequest in request.Consequences)
            {
                var result = _consequenceUseCase.Execute(consequenceRequest);
                var consequence = result.Consequence;

                if (consequence.MinRange > consequence.MaxRange)
                    throw new ArgumentException("Consequence has MinRange greater than MaxRange.");

                if (consequence.MinRange < lastMaxRange)
                    throw new ArgumentException("Consequences ranges are out of order or overlapping.");

                consequences.Add(consequence);
                lastMaxRange = consequence.MaxRange;
            }

            // Build Entity
            var choice = new Choice(
                id: Guid.NewGuid(),
                name: request.Name.Trim(),
                bonusType: request.BonusType,
                consequences: consequences
                );

            // Response
            return new GenerateChoiceResponse(
                choice: choice
            );
        }
    }
}
