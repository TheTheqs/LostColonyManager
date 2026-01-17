using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases;

public class GenerateConsequenceResponse(
        Consequence consequence
    )
{
    public Consequence Consequence { get; } = consequence;
}