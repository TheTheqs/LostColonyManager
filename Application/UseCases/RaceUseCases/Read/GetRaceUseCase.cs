using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.Mapping;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GetRaceUseCase
    {
        private readonly IRaceRepository _repository;

        public GetRaceUseCase(IRaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetRaceResponse> ExecuteAsync(
            GetRaceRequest request
        )
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result is null)
            {
                var races = await _repository.GetAllAsync();
                var raceDtos = races.Select(p => p.ToDto()).ToList();
                return new GetRaceResponse(Races: raceDtos);
            }
            return new GetRaceResponse(new List<RaceDto>() { result.ToDto() });
        }
    }
}
