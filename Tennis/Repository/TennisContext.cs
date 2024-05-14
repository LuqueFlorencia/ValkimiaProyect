using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tennis.Models.Entity;

namespace Tennis.Repository
{
    public class TennisContext : DbContext
    {
        public TennisContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.ApplyConfiguration(new MatchConfig());
            builder.ApplyConfiguration(new PersonConfig());
            builder.ApplyConfiguration(new PlayerConfig());
            builder.ApplyConfiguration(new RegisteredPlayersConfig());
            builder.ApplyConfiguration(new TournamentConfig());
            builder.ApplyConfiguration(new UserConfig());
        }
    }
}
