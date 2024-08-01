using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface IServiceService
{
    Task<ServiceDTO[]> GetAllServices(CancellationToken token);
    Task<ServiceDTO> GetService(int id, CancellationToken token);
}