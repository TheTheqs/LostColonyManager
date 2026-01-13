using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events", t =>
            {
                t.HasCheckConstraint(
                    "CK_events_exactly_one_owner",
                    @"(CASE WHEN ""CampaignId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""RaceId""     IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PlanetId""   IS NULL THEN 0 ELSE 1 END) = 1"
                );
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            // Owner relationships (optional FKs)
            builder.HasOne(x => x.Campaign)
                .WithMany(c => c.Events)
                .HasForeignKey(x => x.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Race)
                .WithMany(r => r.Events)
                .HasForeignKey(x => x.RaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Planet)
                .WithMany(p => p.Events)
                .HasForeignKey(x => x.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Choices relationship
            builder.HasMany(x => x.Choices)
                .WithOne(c => c.Event)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes úteis
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.Name);

            builder.HasIndex(x => x.CampaignId);
            builder.HasIndex(x => x.RaceId);
            builder.HasIndex(x => x.PlanetId);
        }
    }
}
