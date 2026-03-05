using LostColonyManager.Domain.Models;
using LostColonyManager.Infra.Data;
using LostColonyManager.Interface.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Application.UseCases
{
    public sealed class ExportDatabaseRequest
    {
        // Intentionally empty - export is always FULL snapshot.
    }

    public sealed record ExportDatabaseResponse(
        DatabaseExportDto Snapshot
    );

    public sealed class ExportDatabaseUseCase
    {
        private readonly AppDbContext _context;

        public ExportDatabaseUseCase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExportDatabaseResponse> ExecuteAsync(ExportDatabaseRequest? request = null)
        {
            // IMPORTANT:
            // DbContext does NOT allow concurrent operations.
            // So we must run queries sequentially (await each one).
            var campaigns = await _context.Campaigns.AsNoTracking().ToListAsync();
            var events = await _context.Events.AsNoTracking().ToListAsync();
            var choices = await _context.Set<Choice>().AsNoTracking().ToListAsync();
            var consequences = await _context.Set<Consequence>().AsNoTracking().ToListAsync();
            var planets = await _context.Planets.AsNoTracking().ToListAsync();
            var races = await _context.Races.AsNoTracking().ToListAsync();
            var structures = await _context.Structures.AsNoTracking().ToListAsync();
            var planetStructures = await _context.PlanetStructures.AsNoTracking().ToListAsync();

            // 2) Lookups for relationship IDs
            var eventIdsByCampaign = events
                .Where(e => e.CampaignId.HasValue)
                .GroupBy(e => e.CampaignId!.Value)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.Id).ToList());

            var eventIdsByPlanet = events
                .Where(e => e.PlanetId.HasValue)
                .GroupBy(e => e.PlanetId!.Value)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.Id).ToList());

            var eventIdsByRace = events
                .Where(e => e.RaceId.HasValue)
                .GroupBy(e => e.RaceId!.Value)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.Id).ToList());

            var choiceIdsByEvent = choices
                .GroupBy(c => c.EventId)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.Id).ToList());

            var consequenceIdsByChoice = consequences
                .GroupBy(c => c.ChoiceId)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.Id).ToList());

            var structureIdsByPlanet = planetStructures
                .GroupBy(ps => ps.PlanetId)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.StructureId).ToList());

            var planetIdsByStructure = planetStructures
                .GroupBy(ps => ps.StructureId)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Guid>)g.Select(x => x.PlanetId).ToList());

            static IReadOnlyList<Guid> IdsOrEmpty(Dictionary<Guid, IReadOnlyList<Guid>> dict, Guid key)
                => dict.TryGetValue(key, out var ids) ? ids : Array.Empty<Guid>();

            // 3) DTO mapping (flat)
            var campaignDtos = campaigns
                .Select(c => new CampaignExportDto(
                    Id: c.Id,
                    Name: c.Name,
                    EventIds: IdsOrEmpty(eventIdsByCampaign, c.Id)
                ))
                .ToList();

            var eventDtos = events
                .Select(e => new EventExportDto(
                    Id: e.Id,
                    Name: e.Name,
                    Type: e.Type,
                    CampaignId: e.CampaignId,
                    RaceId: e.RaceId,
                    PlanetId: e.PlanetId,
                    ChoiceIds: IdsOrEmpty(choiceIdsByEvent, e.Id)
                ))
                .ToList();

            var choiceDtos = choices
                .Select(c => new ChoiceExportDto(
                    Id: c.Id,
                    Name: c.Name,
                    BonusType: c.BonusType,
                    EventId: c.EventId,
                    ConsequenceIds: IdsOrEmpty(consequenceIdsByChoice, c.Id)
                ))
                .ToList();

            var consequenceDtos = consequences
                .Select(c => new ConsequenceExportDto(
                    Id: c.Id,
                    Name: c.Name,
                    MinRange: c.MinRange,
                    MaxRange: c.MaxRange,
                    Type: c.Type,
                    Target: c.Target,
                    Value: c.Value,
                    ChoiceId: c.ChoiceId
                ))
                .ToList();

            var planetDtos = planets
                .Select(p => new PlanetExportDto(
                    Id: p.Id,
                    Name: p.Name,
                    Category: p.Category,
                    EventIds: IdsOrEmpty(eventIdsByPlanet, p.Id),
                    StructureIds: IdsOrEmpty(structureIdsByPlanet, p.Id)
                ))
                .ToList();

            var raceDtos = races
                .Select(r => new RaceExportDto(
                    Id: r.Id,
                    Name: r.Name,
                    Traits: new RaceTraitsExportDto(
                        Power: r.Traits.Power,
                        Technology: r.Traits.Technology,
                        Diplomacy: r.Traits.Diplomacy,
                        Culture: r.Traits.Culture
                    ),
                    EventIds: IdsOrEmpty(eventIdsByRace, r.Id)
                ))
                .ToList();

            var structureDtos = structures
                .Select(s => new StructureExportDto(
                    Id: s.Id,
                    Name: s.Name,
                    BonusType: s.BonusType,
                    BonusTargets: s.BonusTargets,
                    IncreasePerTurn: s.IncreasePerTurn,
                    Value: s.Value,
                    Cost: s.Cost,
                    Requeriments: s.Requeriments,
                    Type: s.Type,
                    PlanetIds: planetIdsByStructure.TryGetValue(s.Id, out var pids) ? pids : Array.Empty<Guid>()
                ))
                .ToList();

            var planetStructureDtos = planetStructures
                .Select(ps => new PlanetStructureExportDto(
                    PlanetId: ps.PlanetId,
                    StructureId: ps.StructureId
                ))
                .ToList();

            var snapshot = new DatabaseExportDto(
                Campaigns: campaignDtos,
                Events: eventDtos,
                Choices: choiceDtos,
                Consequences: consequenceDtos,
                Planets: planetDtos,
                Races: raceDtos,
                Structures: structureDtos,
                PlanetStructures: planetStructureDtos
            );

            return new ExportDatabaseResponse(snapshot);
        }
    }
}