using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Interface.Dtos;

// =========================
// Export Snapshot (Root)
// =========================
public sealed record DatabaseExportDto(
    IReadOnlyList<CampaignExportDto> Campaigns,
    IReadOnlyList<EventExportDto> Events,
    IReadOnlyList<ChoiceExportDto> Choices,
    IReadOnlyList<ConsequenceExportDto> Consequences,
    IReadOnlyList<PlanetExportDto> Planets,
    IReadOnlyList<RaceExportDto> Races,
    IReadOnlyList<StructureExportDto> Structures,
    IReadOnlyList<PlanetStructureExportDto> PlanetStructures
);

// =========================
// Export / Value Objects
// =========================
public sealed record RaceTraitsExportDto(
    int Power,
    int Technology,
    int Diplomacy,
    int Culture
);

// =========================
// Export Entities (Flat + Relationship IDs)
// =========================
public sealed record CampaignExportDto(
    Guid Id,
    string Name,
    IReadOnlyList<Guid> EventIds
);

public sealed record EventExportDto(
    Guid Id,
    string Name,
    WorldAspect Type,
    Guid? CampaignId,
    Guid? RaceId,
    Guid? PlanetId,
    IReadOnlyList<Guid> ChoiceIds
);

public sealed record ChoiceExportDto(
    Guid Id,
    string Name,
    ChoiceBonusType BonusType,
    Guid EventId,
    IReadOnlyList<Guid> ConsequenceIds
);

public sealed record ConsequenceExportDto(
    Guid Id,
    string Name,
    int MinRange,
    int MaxRange,
    BonusType Type,
    Resource Target,
    float Value,
    Guid ChoiceId
);

public sealed record PlanetExportDto(
    Guid Id,
    string Name,
    int Category,
    IReadOnlyList<Guid> EventIds,
    IReadOnlyList<Guid> StructureIds
);

public sealed record RaceExportDto(
    Guid Id,
    string Name,
    RaceTraitsExportDto Traits,
    IReadOnlyList<Guid> EventIds
);

public sealed record StructureExportDto(
    Guid Id,
    string Name,
    BonusType BonusType,
    IReadOnlyList<Resource> BonusTargets,
    bool IncreasePerTurn,
    float Value,
    IReadOnlyDictionary<Resource, int> Cost,
    IReadOnlyDictionary<Resource, int> Requeriments,
    WorldAspect Type,
    IReadOnlyList<Guid> PlanetIds
);

// Join table explicit (N:N)
public sealed record PlanetStructureExportDto(
    Guid PlanetId,
    Guid StructureId
);