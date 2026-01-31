using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.Mapping;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases
{
    public sealed class GetEventUseCase
    {
        private readonly IEventRepository _repository;

        public GetEventUseCase(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetEventResponse> ExecuteAsync(
            GetEventRequest request
        )
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result is null)
            {
                var events = await _repository.GetAllAsync();
                var eventDtos = events.Select(e => e.ToDto()).ToList();
                return new GetEventResponse(Events: eventDtos);
            }
            return new GetEventResponse(new List<EventDto>() { result.ToDto() });
        }
    }
}
