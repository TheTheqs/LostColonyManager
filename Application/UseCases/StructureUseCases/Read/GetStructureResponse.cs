using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.UseCases;

public sealed record GetStructureResponse(
        List<StructureDto> Structures
    );
