using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases
{
    public sealed class RegisterStructureUseCase
    {
        private readonly IStructureRepository _repository;

        public RegisterStructureUseCase(IStructureRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterStructureResponse> ExecuteAsync(
            RegisterStructureRequest request
        )
        {
            // All validated and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Unique name
            var normalizedName = request.Name.Trim();
            var nameAlreadyExists = await _repository.ExistsByNameAsync(normalizedName);
            if (nameAlreadyExists)
                throw new InvalidOperationException($"A structure with name '{normalizedName}' already exists.");

            // Validate Enums (must be defined values)
            if (!Enum.IsDefined(typeof(BonusType), request.BonusType))
                throw new ArgumentOutOfRangeException(nameof(request.BonusType), "Invalid BonusType value.");

            if (!Enum.IsDefined(typeof(WorldAspect), request.Type))
                throw new ArgumentOutOfRangeException(nameof(request.Type), "Invalid WorldAspect value.");

            // Validate dictionaries (optional but safe)
            if (request.Cost is null)
                throw new ArgumentNullException(nameof(request.Cost));

            if (request.Requeriments is null)
                throw new ArgumentNullException(nameof(request.Requeriments));

            // Validate Resource enum keys (guarantee keys are valid enum values)
            foreach (var key in request.Cost.Keys)
            {
                if (!Enum.IsDefined(typeof(Resource), key))
                    throw new ArgumentOutOfRangeException(nameof(request.Cost), $"Invalid Resource in Cost: '{key}'.");
            }

            foreach (var key in request.Requeriments.Keys)
            {
                if (!Enum.IsDefined(typeof(Resource), key))
                    throw new ArgumentOutOfRangeException(nameof(request.Requeriments), $"Invalid Resource in Requeriments: '{key}'.");
            }

            // Validate values (optional but consistent)
            if (request.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(request.Value), "Value must be >= 0.");

            foreach (var kv in request.Cost)
            {
                if (kv.Value < 0)
                    throw new ArgumentOutOfRangeException(nameof(request.Cost), $"Cost for '{kv.Key}' must be >= 0.");
            }

            foreach (var kv in request.Requeriments)
            {
                if (kv.Value < 0)
                    throw new ArgumentOutOfRangeException(nameof(request.Requeriments), $"Requeriments for '{kv.Key}' must be >= 0.");
            }

            // Build Entity
            var structure = new Structure
            {
                Id = Guid.NewGuid(),
                Name = normalizedName,
                IncreasePerTurn = request.IncreasePerTurn,
                BonusTargets = request.BonusTargets,
                BonusType = request.BonusType,
                Value = request.Value,
                Cost = request.Cost,
                Requeriments = request.Requeriments,
                Type = request.Type
            };

            // Persist
            await _repository.AddAsync(structure);

            // Response
            return new RegisterStructureResponse(
                structure.Id,
                structure.Name,
                structure.BonusType,
                structure.BonusTargets,
                structure.IncreasePerTurn,
                structure.Value,
                structure.Type
            );
        }
    }
}
