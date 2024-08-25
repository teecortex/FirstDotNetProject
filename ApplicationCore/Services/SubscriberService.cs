using System.Text.Json;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services;

public class SubscriberService : ISubscriberService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<SubscriberService> _logger;

    public SubscriberService(ApplicationDbContext context, IDistributedCache distributedCache,
        ILogger<SubscriberService> logger)
    {
        _context = context;
        _cache = distributedCache;
        _logger = logger;
    }

    public async Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        Subscriber[]? subs = null;

        var subsString = await _cache.GetStringAsync("Subs", token);

        if (subsString != null)
        {
            _logger.LogInformation("Getting all subscribers from redis cache");
            subs = JsonSerializer.Deserialize<Subscriber[]>(subsString);
        }
        else
        {
            _logger.LogInformation("Getting all subscribers from db");
            subs = await _context.Subscribers.ToArrayAsync(token);

            subsString = JsonSerializer.Serialize(subs);
            
            _logger.LogInformation("Set key with subscribers in redis");
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
            _logger.LogInformation($"Getting subscriber with id={id} from redis cache");
            subs = JsonSerializer.Deserialize<Subscriber[]>(subString);
            sub = subs.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            _logger.LogInformation($"Getting subscriber with id={id} from db");
            sub = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (sub != null)
        {
            return SubscriberMapper.Mapper(sub, new SubscriberDTO());
        }

        return null;
    }
}