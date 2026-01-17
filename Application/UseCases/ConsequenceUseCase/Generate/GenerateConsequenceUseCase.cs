using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GenerateConsequenceUseCase
    {

        public GenerateConsequenceUseCase()
        {
        }
        public GenerateConsequenceResponse ExecuteAsync(
            GenerateConsequenceRequest request
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Min and Max Range validation
            if(request.MinRange < 1 || request.MaxRange > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(request.MinRange), "MinRange must be at least 1 and MaxRange must be at most 20.");
            }

            // Validate Bonus Type
            if (!Enum.IsDefined(typeof(BonusType), request.Type))
            {
                throw new ArgumentException("Invalid BonusType");
            }

            // Validate Resource
            if (!Enum.IsDefined(typeof(Resource), request.Target))
            {
                throw new ArgumentException("Invalid Targeted Resource");
            }
            // Validate Value
            if (request.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(request.Value), "Value must be greater than zero.");
            }

            // Build Entity
            var consequence = new Consequence(
                id: Guid.NewGuid(),
                name: request.Name.Trim(),
                minRange: request.MinRange,
                maxRange: request.MaxRange,
                type: request.Type,
                target: request.Target,
                value: request.Value
                );

            // Response
            return new GenerateConsequenceResponse(
                consequence: consequence
            );
        }
    }
}
