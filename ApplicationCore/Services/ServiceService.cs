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

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<ServiceService> _logger;

    public ServiceService(ApplicationDbContext context, IDistributedCache distributedCache, ILogger<ServiceService> logger)
    {
        _context = context;
        _cache = distributedCache;
        _logger = logger;
    }


    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        Service[]? services = null;

        var servicesString = await _cache.GetStringAsync("Services");

        if (servicesString != null)
        {
            _logger.LogInformation("Getting all services from redis cache");
            services = JsonSerializer.Deserialize<Service[]>(servicesString);
        }
        else
        {
            _logger.LogInformation("Getting all services from db");
            services = await _context.Services.ToArrayAsync(token);

            servicesString = JsonSerializer.Serialize(services);
            
            _logger.LogInformation("Set key with services in redis");
            _cache.SetStringAsync("Services", servicesString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            });
        }
        
        return services.Select(x => ServiceMapper.Mapper(x, new ServiceDTO())).ToArray();
    }

    public async Task<ServiceDTO> GetService(int id, CancellationToken token)
    {

        Service[]? services = null;
        Service? service = null;

        var servicesString = await _cache.GetStringAsync("Services");

        if (servicesString != null)
        {
            _logger.LogInformation($"Getting service with id={id} from redis cache");
            services = JsonSerializer.Deserialize<Service[]>(servicesString);
            service = services.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            _logger.LogInformation($"Getting service with id={id} from db");
            service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (service != null)
        {
            return ServiceMapper.Mapper(service, new ServiceDTO());
        }
        
        return null;
    }
}