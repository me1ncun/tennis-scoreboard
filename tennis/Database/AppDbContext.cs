using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using tennis_scoreboard.Models;
using tennis.Extensions;

public class AppDbContext : DbContext
{
    private IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Database"));
    }
    
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
}