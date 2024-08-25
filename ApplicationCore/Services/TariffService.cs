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

public class TariffService : ITariffService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<TariffService> _logger;

    public TariffService(ApplicationDbContext context, IDistributedCache distributedCache, ILogger<TariffService> logger)
    {
        _context = context;
        _cache = distributedCache;
        _logger = logger;
    }


    public async Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        Tariff[]? tariffs = null;
        
        var tariffsString = await _cache.GetStringAsync("Tariffs");

        if (tariffsString != null)
        {
            _logger.LogInformation("Getting all tariffs from redis cache");
            tariffs = JsonSerializer.Deserialize<Tariff[]>(tariffsString);
        }
        else
        {
            _logger.LogInformation("Getting all tariffs from db");
            tariffs = await _context.Tariffs.ToArrayAsync(token);

            tariffsString = JsonSerializer.Serialize(tariffs);
            
            _logger.LogInformation("Set key with tariffs in redis");
            _cache.SetStringAsync("Tariffs", tariffsString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            });
        }

        return tariffs.Select(x=> TariffMapper.Mapper(x, new TariffDTO())).ToArray();
    }

    public async Task<TariffDTO> GetTariff(int id, CancellationToken token)
    {
        Tariff[]? tariffs = null;
        Tariff? tariff = null;

        var tariffsString = await _cache.GetStringAsync("Tariffs");

        if (tariffsString != null)
        {
            _logger.LogInformation($"Getting tariff with id={id}");
            tariffs = JsonSerializer.Deserialize<Tariff[]>(tariffsString);
            tariff = tariffs.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            _logger.LogInformation($"Getting tariff with id={id}");
            tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (tariff != null)
        {
            return TariffMapper.Mapper(tariff, new TariffDTO());
        }

        return null;
    }
}