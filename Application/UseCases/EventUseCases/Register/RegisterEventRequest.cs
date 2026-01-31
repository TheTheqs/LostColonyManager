using LostColonyManager.Domain.Enums;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases;

public sealed class RegisterEventRequest
{
    public required string Name { get; init; }
    public required WorldAspect Type { get; init; }
    public required Guid OwnerId { get; init; }
    public List<GenerateChoiceRequest> Choices { get; init; } = [];
}
