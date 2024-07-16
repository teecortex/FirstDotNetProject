using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class ServiceService : IServiceService
{
    private readonly ApplicationContext _context;

    public ServiceService(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        var services = await _context.services.ToArrayAsync(token);
        return services.Select(x => new ServiceDTO(x)).ToArray();
    }

    public async Task<ServiceDTO> GetService(CancellationToken token, int id)
    {
        var services = await _context.services.ToArrayAsync(token);
        var service = services.FirstOrDefault(x => x.Id == id);

        if (service != null)
        {
            return new ServiceDTO(service);
        }
        
        return null;
    }
}