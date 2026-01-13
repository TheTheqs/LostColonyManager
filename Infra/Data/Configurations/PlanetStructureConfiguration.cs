using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class PlanetStructureConfiguration : IEntityTypeConfiguration<PlanetStructure>
    {
        public void Configure(EntityTypeBuilder<PlanetStructure> builder)
        {
            builder.ToTable("planet_structures");

            builder.HasKey(x => new { x.PlanetId, x.StructureId });

            builder.HasOne(x => x.Planet)
                .WithMany(p => p.Structures)
                .HasForeignKey(x => x.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Structure)
                .WithMany(s => s.Planets)
                .HasForeignKey(x => x.StructureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PlanetId);
            builder.HasIndex(x => x.StructureId);
        }
    }
}
