using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.UseCases;
using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace LostColonyManager.Application.UseCases
{
    public sealed class RegisterEventUseCase
    {
        public GenerateChoiceUseCase _choiceUseCase;
        public IEventRepository _repository;

        public RegisterEventUseCase(GenerateChoiceUseCase choiceUseCase, IEventRepository repository)
        {
            _choiceUseCase = choiceUseCase;
            _repository = repository;
        }
        public async Task<RegisterEventResponse> ExecuteAsync(
            RegisterEventRequest request
        )
        {
            // All validatted and not null
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.", nameof(request.Name));

            // Validate World Apect
            if (!Enum.IsDefined(typeof(WorldAspect), request.Type))
            {
                throw new ArgumentException("Invalid World Aspect");
            }

            // Generate Choices
            var _choices = new List<Choice>();

            foreach (var choiceRequest in request.Choices)
            {
                var result = _choiceUseCase.Execute(choiceRequest);
                var choice = result.Choice;
                _choices.Add(choice);
            }

            // Validate Choices
            if (_choices.Count < 2)
            {
                throw new ArgumentException("There is must be at least 2 choices!");
            }

            // Build Entity
            var @event = new Event(
                id: Guid.NewGuid(),
                name: request.Name.Trim(),
                type: request.Type,
                choices: _choices
                );

            // Owner
            switch (request.Type)
            {
                case WorldAspect.Campaign:
                    @event.CampaignId = request.OwnerId;
                    break;

                case WorldAspect.Race:
                    @event.RaceId = request.OwnerId;
                    break;

                case WorldAspect.Planet:
                    @event.PlanetId = request.OwnerId;
                    break;

                default:
                    throw new ArgumentException("Invalid World Aspect");
            }

            // Persistence
            await _repository.AddAsync(@event);
            return new RegisterEventResponse(Event: @event);
        }
    }
}
