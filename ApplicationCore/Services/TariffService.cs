using ApplicationCore.Interfaces;
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

        return tariffs.Select(x=> new TariffDTO(x)).ToArray();
    }

    public async Task<TariffDTO> GetTariff(int id, CancellationToken token)
    {
        var tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == id, token);
        
        if (tariff != null)
        {
            return new TariffDTO(tariff);
        }

        return null;
    }
}