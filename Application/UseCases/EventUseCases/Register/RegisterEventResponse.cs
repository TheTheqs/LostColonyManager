using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases;

public sealed record RegisterEventResponse(
        Event Event
    );