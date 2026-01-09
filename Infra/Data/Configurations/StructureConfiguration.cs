using LostColonyManager.Domain.Enums;
using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace LostColonyManager.Infra.Data.Configurations
{
    public class StructureConfiguration : IEntityTypeConfiguration<Structure>
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private static readonly ValueConverter<Dictionary<Resource, int>, string> DictToJsonConverter =
            new(
                v => JsonSerializer.Serialize(v, JsonOptions),
                v => JsonSerializer.Deserialize<Dictionary<Resource, int>>(v, JsonOptions) ?? new()
            );

        private static readonly ValueComparer<Dictionary<Resource, int>> DictComparer =
            new(
                (a, b) => JsonSerializer.Serialize(a, JsonOptions) == JsonSerializer.Serialize(b, JsonOptions),
                v => JsonSerializer.Serialize(v, JsonOptions).GetHashCode(),
                v => JsonSerializer.Deserialize<Dictionary<Resource, int>>(
                         JsonSerializer.Serialize(v, JsonOptions), JsonOptions
                     ) ?? new()
            );

        public void Configure(EntityTypeBuilder<Structure> builder)
        {
            builder.ToTable("structures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.BonusType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.ReferenceId)
                .IsRequired();

            builder.Property(x => x.Cost)
                .HasConversion(DictToJsonConverter)
                .HasColumnType("jsonb")
                .Metadata.SetValueComparer(DictComparer);

            builder.Property(x => x.Requeriments)
                .HasConversion(DictToJsonConverter)
                .HasColumnType("jsonb")
                .Metadata.SetValueComparer(DictComparer);

            builder.HasIndex(x => x.Type);
            builder.HasIndex(x => x.ReferenceId);
            builder.HasIndex(x => x.Name);
        }
    }
}
