using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class PlanetConfiguration : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.ToTable("planets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Category)
                .IsRequired();

            // Relationship: Planet (1) -> Events (N)
            builder.HasMany(x => x.Events)
                .WithOne(e => e.Planet)
                .HasForeignKey(e => e.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);


            // Util Indexes
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.Category);
        }
    }
}
