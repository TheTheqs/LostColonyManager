using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Interface.Dtos;

// =========================
// Common / Value Objects
// =========================
public sealed record RaceTraitsDto(
    int Power,
    int Technology,
    int Diplomacy,
    int Culture
);

// =========================
// Campaign
// =========================
public sealed record CampaignSummaryDto(
    Guid Id,
    string Name
);

public sealed record CampaignDto(
    Guid Id,
    string Name,
    IReadOnlyList<EventSummaryDto> Events
);

// =========================
// Event
// =========================
public sealed record EventSummaryDto(
    Guid Id,
    string Name,
    WorldAspect Type,

    Guid? CampaignId,
    Guid? RaceId,
    Guid? PlanetId
);

public sealed record EventDto(
    Guid Id,
    string Name,
    WorldAspect Type,

    CampaignSummaryDto? Campaign,
    RaceSummaryDto? Race,
    PlanetSummaryDto? Planet,

    IReadOnlyList<ChoiceDto> Choices
);

// =========================
// Choice
// =========================
public sealed record ChoiceSummaryDto(
    Guid Id,
    string Name,
    ChoiceBonusType BonusType
);

public sealed record ChoiceDto(
    Guid Id,
    string Name,
    ChoiceBonusType BonusType,
    Guid EventId,
    IReadOnlyList<ConsequenceDto> Consequences
);

// =========================
// Consequence
// =========================
public sealed record ConsequenceDto(
    Guid Id,
    string Name,
    int MinRange,
    int MaxRange,
    BonusType Type,
    Resource Target,
    float Value,
    Guid ChoiceId
);

// =========================
// Planet
// =========================
public sealed record PlanetSummaryDto(
    Guid Id,
    string Name,
    int Category
);

public sealed record PlanetDto(
    Guid Id,
    string Name,
    int Category,

    IReadOnlyList<EventSummaryDto> Events,
    IReadOnlyList<StructureOnPlanetDto> Structures
);

// Join export (Planet -> Structures)
public sealed record StructureOnPlanetDto(
    Guid StructureId,
    string Name,
    BonusType BonusType,
    float Value,
    WorldAspect Type
);

// =========================
// Race
// =========================
public sealed record RaceSummaryDto(
    Guid Id,
    string Name
);

public sealed record RaceDto(
    Guid Id,
    string Name,
    RaceTraitsDto Traits,
    IReadOnlyList<EventSummaryDto> Events
);

// =========================
// Structure
// =========================
public sealed record StructureSummaryDto(
    Guid Id,
    string Name,
    BonusType BonusType,
    float Value,
    WorldAspect Type
);

public sealed record StructureDto(
    Guid Id,
    string Name,
    BonusType BonusType,
    float Value,
    IReadOnlyDictionary<Resource, int> Cost,
    IReadOnlyDictionary<Resource, int> Requeriments,
    WorldAspect Type,
    IReadOnlyList<PlanetSummaryDto> Planets
);
