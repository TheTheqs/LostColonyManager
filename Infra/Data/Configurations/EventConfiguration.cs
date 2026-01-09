using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.ReferenceId)
                .IsRequired();

            builder.Property(x => x.ChoicesIds)
                .HasColumnType("uuid[]")
                .IsRequired();

            // Util Indexes
            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.ReferenceId);
            builder.HasIndex(x => x.Name);
        }
    }
}
