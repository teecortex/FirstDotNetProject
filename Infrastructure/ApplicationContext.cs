using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure;

public class ApplicationContext : DbContext
{
    public DbSet<Subscriber> subscribers { get; set; }
    public DbSet<Tariff> tariffs { get; set; }
    public DbSet<Service> services { get; set; }

    public ApplicationContext()
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