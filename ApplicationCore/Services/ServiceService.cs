using System.Text.Json;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ApplicationCore.Services;

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public ServiceService(ApplicationDbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _cache = distributedCache;
    }


    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        Service[]? services = null;

        var servicesString = await _cache.GetStringAsync("Services");

        if (servicesString != null)
        {
            services = JsonSerializer.Deserialize<Service[]>(servicesString);
        }
        else
        {
            services = await _context.Services.ToArrayAsync(token);

            servicesString = JsonSerializer.Serialize(services);
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
            services = JsonSerializer.Deserialize<Service[]>(servicesString);
            service = services.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (service != null)
        {
            return ServiceMapper.Mapper(service, new ServiceDTO());
        }
        
        return null;
    }
}