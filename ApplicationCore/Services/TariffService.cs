using System.Text.Json;
using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ApplicationCore.Services;

public class TariffService : ITariffService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public TariffService(ApplicationDbContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _cache = distributedCache;
    }


    public async Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        Tariff[]? tariffs = null;

        var tariffsString = await _cache.GetStringAsync("Tariffs");

        if (tariffsString != null)
        {
            tariffs = JsonSerializer.Deserialize<Tariff[]>(tariffsString);
        }
        else
        {
            tariffs = await _context.Tariffs.ToArrayAsync(token);

            tariffsString = JsonSerializer.Serialize(tariffs);
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
            tariffs = JsonSerializer.Deserialize<Tariff[]>(tariffsString);
            tariff = tariffs.FirstOrDefault(x => x.Id == id);
        }
        else
        {
            tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == id, token);
        }
        
        if (tariff != null)
        {
            return TariffMapper.Mapper(tariff, new TariffDTO());
        }

        return null;
    }
}