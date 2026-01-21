using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.Mapping;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GetPlanetUseCase
    {
        private readonly IPlanetRepository _repository;

        public GetPlanetUseCase(IPlanetRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPlanetResponse> ExecuteAsync(
            GetPlanetRequest request
        )
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result is null)
            {
                var planets = await _repository.GetAllAsync();
                var planetDtos = planets.Select(p => p.ToDto()).ToList();
                return new GetPlanetResponse(Planets: planetDtos);
            }
            return new GetPlanetResponse(new List<PlanetDto>() { result.ToDto() });
        }
    }
}
