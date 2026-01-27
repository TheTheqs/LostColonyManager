using LostColonyManager.Domain.Enums;

namespace LostColonyManager.Application.UseCases
{
    public sealed class ManageAssociationRequest
    {
        public Guid StructureId { get; init; }
        public Guid PlanetId { get; init; }
        public bool IsAssociated { get; init; }
    }
}
