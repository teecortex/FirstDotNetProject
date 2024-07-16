using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class TariffService : ITariffService
{
    private readonly ApplicationContext _context;

    public TariffService(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<TariffDTO[]> GetAllTariff(CancellationToken token)
    {
        var tariffs = await _context.tariffs.ToArrayAsync(token);

        return tariffs.Select(x=> new TariffDTO(x)).ToArray();
    }

    public async Task<TariffDTO> GetTariff(CancellationToken token, int id)
    {
        var tariffs = await _context.tariffs.ToArrayAsync(token);
        var tariff = tariffs.FirstOrDefault(x => x.Id == id);

        if (tariff != null)
        {
            return new TariffDTO(tariff);
        }

        return null;
    }
}