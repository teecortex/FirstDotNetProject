using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Web;

public class QueryProvider
{
    public IQueryable<Infrastructure.Entities.Subscriber> GetSubscribers([Service] ApplicationDbContext db)
    {
        return db.Subscribers;
    }
    
    public IQueryable<Infrastructure.Entities.Tariff> GetTariffs([Service] ApplicationDbContext db)
    {
        return db.Tariffs;
    }
    
    public IQueryable<Infrastructure.Entities.Service> GetServices([Service] ApplicationDbContext db)
    {
        return db.Services;
    }
    
    public async Task<Infrastructure.Entities.Subscriber?> GetSubscriber([Service] ApplicationDbContext db, int id)
    {
        var sub = await db.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

        if (sub != null)
        {
            return sub;
        }

        return null;
    }
    
    public async Task<Infrastructure.Entities.Tariff?> GetTariff([Service] ApplicationDbContext db, int id)
    {
        var tariff = await db.Tariffs.FirstOrDefaultAsync(x => x.Id == id);
        return tariff;
    }
    
    public async Task<Infrastructure.Entities.Service?> GetService([Service] ApplicationDbContext db, int id)
    {
        var service = await db.Services.FirstOrDefaultAsync(x => x.Id == id);
        if (service != null)
        {
            return service;
        }

        
        return null;
    }
}