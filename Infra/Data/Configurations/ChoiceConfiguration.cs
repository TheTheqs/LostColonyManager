using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.ToTable("choices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.BonusType)
                .HasConversion<int>()
                .IsRequired();

            // Relationship: Event (1) -> Choices (N)
            builder.HasOne(x => x.Event)
                .WithMany(e => e.Choices)
                .HasForeignKey(x => x.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Choice (1) -> Consequences (N)
            builder.HasMany(x => x.Consequences)
                .WithOne(c => c.Choice)
                .HasForeignKey(c => c.ChoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Util Indexes
            builder.HasIndex(x => x.EventId);
            builder.HasIndex(x => x.Name);
        }
    }
}
