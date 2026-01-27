namespace LostColonyManager.Application.UseCases
{
    public sealed record ManageAssociationResponse(
        Guid StructureId,
        Guid PlanetId,
        bool IsAssociated
    );
}
