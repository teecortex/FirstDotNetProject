using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface ITariffService
{ 
    Task<TariffDTO[]> GetAllTariff(CancellationToken token);
    Task<TariffDTO> GetTariff(CancellationToken token, int id);
}