using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class ConsequenceConfiguration : IEntityTypeConfiguration<Consequence>
    {
        public void Configure(EntityTypeBuilder<Consequence> builder)
        {
            builder.ToTable("consequences");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.MinRange)
                .IsRequired();

            builder.Property(x => x.MaxRange)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Target)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            // Relationship: Choice (1) -> Consequences (N)
            builder.HasOne(x => x.Choice)
                .WithMany(c => c.Consequences)
                .HasForeignKey(x => x.ChoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Util Indexes
            builder.HasIndex(x => x.ChoiceId);
            builder.HasIndex(x => x.Name);
        }
    }
}
