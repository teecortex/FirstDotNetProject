using System.Text.Json;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ApplicationCore.Services;

public class SubscriberService : ISubscriberService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    
    public SubscriberService(ApplicationDbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _cache = distributedCache;
    }

    public async Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        Subscriber[]? subs = null;

        var subsString = await _cache.GetStringAsync("Subs");

        if (subsString != null)
        {
            subs = JsonSerializer.Deserialize<Subscriber[]>(subsString);
        }
        else
        {
            subs = await _context.Subscribers.ToArrayAsync(token);

            subsString = JsonSerializer.Serialize(subs);
            _cache.SetStringAsync("Subs", subsString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            });
        }
        
        return subs.Select(x => SubscriberMapper.Mapper(x, new SubscriberDTO())).ToArray();
    }

    public async Task<SubscriberDTO> GetSubscriber(int id, CancellationToken token)
    {
        Subscriber[]? subs = null;
        Subscriber? sub = null;
        
        var subString = await _cache.GetStringAsync($"Subs");

        if (subString != null)
        {
            subs = JsonSerializer.Deserialize<Subscriber[]>(subString);
            sub = subs.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            sub = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (sub != null)
        {
            return SubscriberMapper.Mapper(sub, new SubscriberDTO());
        }

        return null;
    }
}