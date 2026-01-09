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

            builder.Property(x => x.EventId)
                .IsRequired();

            builder.Property(x => x.ConsequencesIds)
                .HasColumnType("uuid[]")
                .IsRequired();

            // Util Indexes
            builder.HasIndex(x => x.EventId);
            builder.HasIndex(x => x.Name);
        }
    }
}
