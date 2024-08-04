using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class TariffService : ITariffService
{
    private readonly ApplicationDbContext _context;

    public TariffService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        var tariffs = await _context.Tariffs.ToArrayAsync(token);

        return tariffs.Select(x=> TariffMapper.Mapper(x, new TariffDTO())).ToArray();
    }

    public async Task<TariffDTO> GetTariff(int id, CancellationToken token)
    {
        var tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == id, token);
        
        if (tariff != null)
        {
            return TariffMapper.Mapper(tariff, new TariffDTO());
        }

        return null;
    }
}