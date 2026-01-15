using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;
using LostColonyManager.Domain.ValuesObjects;

namespace LostColonyManager.Application.UseCases
{
    public sealed class RegisterPlanetUseCase
    {
        private readonly IPlanetRepository _repository;

        public RegisterPlanetUseCase(IPlanetRepository repository)
        {
            _repository = repository;
        }
        public async Task<RegisterPlanetResponse> ExecuteAsync(
            RegisterPlanetRequest request
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Unique name
            var nameAlreadyExists = await _repository.ExistsByNameAsync(request.Name);
            if (nameAlreadyExists)
                throw new InvalidOperationException($"A planet with name '{request.Name}' already exists.");

            // Validate Category
            if (request.Category < 1 || request.Category > 5)
                throw new ArgumentOutOfRangeException(nameof(request.Category), "Category must be between 1 and 5.");

            // Build Entity
            var planet = new Planet(
                id: Guid.NewGuid(),
                name: request.Name.Trim(),
                category: request.Category
            );

            // Persist
            await _repository.AddAsync(planet);

            // Response
            return new RegisterPlanetResponse(
                planet.Id,
                planet.Name,
                planet.Category
            );
        }
    }
}
