using LostColonyManager.Domain.Interfaces;

namespace LostColonyManager.Application.UseCases
{
    public sealed class DeleteRaceUseCase
    {
        private readonly IRaceRepository _repository;

        public DeleteRaceUseCase(IRaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteRaceResponse> ExecuteAsync(
            DeleteRaceRequest request
        )
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.Name == "")
                throw new ArgumentException("Name is required.", nameof(request.Name));

            var deleted = await _repository.DeleteByNameAsync(request.Name);

            if (!deleted)
                throw new KeyNotFoundException($"Race with the name '{request.Name}' was not found.");

            return new DeleteRaceResponse(request.Name, deleted);
        }
    }
}
