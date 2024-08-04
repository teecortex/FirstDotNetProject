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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
        base(options)
    {
        Database.EnsureCreated();
    }
}