using LostColonyManager.Application.Interfaces;

namespace LostColonyManager.Application.UseCases
{
    public sealed class DeletePlanetUseCase
    {
        private readonly IPlanetRepository _repository;

        public DeletePlanetUseCase(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeletePlanetResponse> ExecuteAsync(
            DeletePlanetRequest request
        )
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.Name == "")
                throw new ArgumentException("Name is required.", nameof(request.Name));

            var deleted = await _repository.DeleteByNameAsync(request.Name);

            if (!deleted)
                throw new KeyNotFoundException($"Planet with the name '{request.Name}' was not found.");

            return new DeletePlanetResponse(request.Name, deleted);
        }
    }
}
