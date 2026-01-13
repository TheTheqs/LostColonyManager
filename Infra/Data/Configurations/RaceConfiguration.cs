using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.ToTable("races");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            // Relationship: Race (1) -> Events (N)
            builder.HasMany(x => x.Events)
                .WithOne(e => e.Race)
                .HasForeignKey(e => e.RaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ComplexProperty(x => x.Traits, traits =>
            {
                traits.Property(t => t.Power).HasColumnName("traits_power");
                traits.Property(t => t.Technology).HasColumnName("traits_technology");
                traits.Property(t => t.Diplomacy).HasColumnName("traits_diplomacy");
                traits.Property(t => t.Culture).HasColumnName("traits_culture");
            });

            builder.HasIndex(x => x.Name);
        }
    }
}
