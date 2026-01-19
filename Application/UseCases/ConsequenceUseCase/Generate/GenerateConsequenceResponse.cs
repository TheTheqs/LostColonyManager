using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases;

public sealed record GenerateConsequenceResponse(
        Consequence Consequence
    );