using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases;

public class GenerateChoiceResponse(
        Choice choice
    )
{
    public Choice Consequence { get; } = choice;
}