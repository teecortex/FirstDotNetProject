using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface IServiceService
{
    Task<ServiceDTO[]> GetAllServices(CancellationToken token);
    Task<ServiceDTO> GetService(CancellationToken token, int id);
}