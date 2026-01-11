using LostColonyManager.Application.UseCases.RaceUseCases.Register;
using LostColonyManager.Domain.Interfaces;
using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Application.UseCases.Races.RegisterRace
{
    public sealed class RegisterRaceUseCase
    {
        private readonly IRaceRepository _repository;

        public RegisterRaceUseCase(IRaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterRaceResponse> ExecuteAsync(
            RegisterRaceRequest request,
            CancellationToken ct = default
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            if (request.Power < 0 || request.Technology < 0 || request.Diplomacy < 0 || request.Culture < 0)
                throw new ArgumentException("Traits cannot be negative.");

            // Trait sums must be 30
            var sum = request.Power + request.Technology + request.Diplomacy + request.Culture;
            if (sum != 30)
                throw new ArgumentException("The sum of all traits must be exactly 30.");

            // Unique name
            var nameAlreadyExists = await _repository.ExistsByNameAsync(request.Name, ct);
            if (nameAlreadyExists)
                throw new InvalidOperationException($"A race with name '{request.Name}' already exists.");

            // Build Traits
            var traits = new RaceTraits(
                request.Power,
                request.Technology,
                request.Diplomacy,
                request.Culture
            );

            // Unique Traits
            var traitsAlreadyExists = await _repository.ExistsByTraitsAsync(traits, ct);
            if (traitsAlreadyExists)
                throw new InvalidOperationException("A race with the same traits already exists.");

            // Build Entity
            var race = new Race(
                name: request.Name.Trim(),
                traits: traits
            );

            // Persist
            await _repository.AddAsync(race, ct);

            // Response
            return new RegisterRaceResponse(
                race.Id,
                race.Name,
                race.Traits.Power,
                race.Traits.Technology,
                race.Traits.Diplomacy,
                race.Traits.Culture
            );
        }
    }
}
