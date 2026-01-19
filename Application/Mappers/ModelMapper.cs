using LostColonyManager.Domain.Models;
using LostColonyManager.Interface.Dtos;

namespace LostColonyManager.Application.Mapping;

public static class ModelMapper
{
    // -------------------------
    // Campaign
    // -------------------------
    public static CampaignSummaryDto ToSummaryDto(this Campaign c)
        => new(c.Id, c.Name);

    public static CampaignDto ToDto(this Campaign c)
        => new(
            Id: c.Id,
            Name: c.Name,
            Events: c.Events.Select(e => e.ToSummaryDto()).ToList()
        );

    // -------------------------
    // Event
    // -------------------------
    public static EventSummaryDto ToSummaryDto(this Event e)
        => new(
            Id: e.Id,
            Name: e.Name,
            Type: e.Type,
            CampaignId: e.CampaignId,
            RaceId: e.RaceId,
            PlanetId: e.PlanetId
        );

    public static EventDto ToDto(this Event e)
        => new(
            Id: e.Id,
            Name: e.Name,
            Type: e.Type,
            Campaign: e.Campaign is null ? null : e.Campaign.ToSummaryDto(),
            Race: e.Race is null ? null : e.Race.ToSummaryDto(),
            Planet: e.Planet is null ? null : e.Planet.ToSummaryDto(),
            Choices: e.Choices.Select(c => c.ToDto()).ToList()
        );

    // -------------------------
    // Choice
    // -------------------------
    public static ChoiceSummaryDto ToSummaryDto(this Choice c)
        => new(c.Id, c.Name, c.BonusType);

    public static ChoiceDto ToDto(this Choice c)
        => new(
            Id: c.Id,
            Name: c.Name,
            BonusType: c.BonusType,
            EventId: c.EventId,
            Consequences: c.Consequences.Select(x => x.ToDto()).ToList()
        );

    // -------------------------
    // Consequence
    // -------------------------
    public static ConsequenceDto ToDto(this Consequence x)
        => new(
            Id: x.Id,
            Name: x.Name,
            MinRange: x.MinRange,
            MaxRange: x.MaxRange,
            Type: x.Type,
            Target: x.Target,
            Value: x.Value,
            ChoiceId: x.ChoiceId
        );

    // -------------------------
    // Planet
    // -------------------------
    public static PlanetSummaryDto ToSummaryDto(this Planet p)
        => new(p.Id, p.Name, p.Category);

    public static PlanetDto ToDto(this Planet p)
        => new(
            Id: p.Id,
            Name: p.Name,
            Category: p.Category,
            Events: p.Events.Select(e => e.ToSummaryDto()).ToList(),
            Structures: p.Structures.Select(ps => ps.Structure.ToStructureOnPlanetDto()).ToList()
        );

    // -------------------------
    // Race
    // -------------------------
    public static RaceSummaryDto ToSummaryDto(this Race r)
        => new(r.Id, r.Name);

    public static RaceDto ToDto(this Race r)
        => new(
            Id: r.Id,
            Name: r.Name,
            Traits: r.Traits.ToDto(),
            Events: r.Events.Select(e => e.ToSummaryDto()).ToList()
        );

    // Value Object
    public static RaceTraitsDto ToDto(this LostColonyManager.Domain.ValuesObjects.RaceTraits t)
        => new(t.Power, t.Technology, t.Diplomacy, t.Culture);

    // -------------------------
    // Structure
    // -------------------------
    public static StructureSummaryDto ToSummaryDto(this Structure s)
        => new(s.Id, s.Name, s.BonusType, s.Value, s.Type);

    public static StructureOnPlanetDto ToStructureOnPlanetDto(this Structure s)
        => new(
            StructureId: s.Id,
            Name: s.Name,
            BonusType: s.BonusType,
            Value: s.Value,
            Type: s.Type
        );

    public static StructureDto ToDto(this Structure s)
        => new(
            Id: s.Id,
            Name: s.Name,
            BonusType: s.BonusType,
            Value: s.Value,
            Cost: s.Cost,
            Requeriments: s.Requeriments,
            Type: s.Type,
            Planets: s.Planets.Select(ps => ps.Planet.ToSummaryDto()).ToList()
        );
}
