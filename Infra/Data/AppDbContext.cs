using LostColonyManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LostColonyManager.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<Choice> Choices => Set<Choice>();
        public DbSet<Consequence> Consequences => Set<Consequence>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Planet> Planets => Set<Planet>();
        public DbSet<Race> Races => Set<Race>();
        public DbSet<Structure> Structures => Set<Structure>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}