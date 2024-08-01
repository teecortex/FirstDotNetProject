using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>().ToTable("services");
        modelBuilder.Entity<Tariff>().ToTable("tariffs");
        modelBuilder.Entity<Subscriber>().ToTable("subscribers");
    }

    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }

    private string GetDbString()
    {
        string data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));
        var jsonDoc = JsonDocument.Parse(data);
        var db = jsonDoc.RootElement.GetProperty("Database");
        var db_dict = JsonSerializer.Deserialize<Dictionary<string, string>>(db);
    
        string conn = "Host=" + db_dict["Host"] + ";Port=" + db_dict["Port"] + ";Database=" + db_dict["Name"] +
                      ";Username=" + db_dict["Username"];
    
        return conn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetDbString());
    }
}