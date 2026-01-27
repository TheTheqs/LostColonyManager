using LostColonyManager.Application.Interfaces;
using LostColonyManager.Domain.Models;

namespace LostColonyManager.Application.UseCases
{
    public sealed class ManageAssociationUseCase
    {
        private readonly IStructureRepository _structureRepository;
        private readonly IPlanetRepository _planetRepository;
        private readonly IPlanetStructureRepository _planetStructureRepository;

        public ManageAssociationUseCase(
            IStructureRepository structureRepository,
            IPlanetRepository planetRepository,
            IPlanetStructureRepository planetStructureRepository
        )
        {
            _structureRepository = structureRepository;
            _planetRepository = planetRepository;
            _planetStructureRepository = planetStructureRepository;
        }

        public async Task<ManageAssociationResponse> ExecuteAsync(
            ManageAssociationRequest request
        )
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.StructureId == Guid.Empty)
                throw new ArgumentException("StructureId is required.", nameof(request.StructureId));

            if (request.PlanetId == Guid.Empty)
                throw new ArgumentException("PlanetId is required.", nameof(request.PlanetId));

            var structureExists = await _structureRepository.ExistsByIdAsync(request.StructureId);
            if (!structureExists)
                throw new InvalidOperationException($"Structure '{request.StructureId}' does not exist.");

            var planetExists = await _planetRepository.ExistsByIdAsync(request.PlanetId);
            if (!planetExists)
                throw new InvalidOperationException($"Planet '{request.PlanetId}' does not exist.");

            var alreadyAssociated = await _planetStructureRepository.ExistsAsync(
                planetId: request.PlanetId,
                structureId: request.StructureId
            );

            if (!request.IsAssociated)
            {
                if (alreadyAssociated)
                    throw new InvalidOperationException("This association already exists.");

                await _planetStructureRepository.AddAsync(new PlanetStructure
                {
                    PlanetId = request.PlanetId,
                    StructureId = request.StructureId
                });
            }
            else
            {
                if (!alreadyAssociated)
                    throw new InvalidOperationException("This association does not exist.");

                await _planetStructureRepository.DeleteAsync(
                    planetId: request.PlanetId,
                    structureId: request.StructureId
                );
            }

            return new ManageAssociationResponse(
                StructureId: request.StructureId,
                PlanetId: request.PlanetId,
                IsAssociated: request.IsAssociated
            );
        }
    }
}
