using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface ITariffService
{ 
    Task<TariffDTO[]> GetAllTariffs(CancellationToken token);
    Task<TariffDTO> GetTariff(int id, CancellationToken token);
}