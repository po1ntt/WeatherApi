using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Towns> town { get; set; }
        public DbSet<WeatherInfo> weather { get; set; }
        public DbSet<FavoriteTowns> favoriteTowns { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=weatherdb;Username=postgres;Password=12345");
        }
    }
}
