using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;

    public ServiceService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        var services = await _context.Services.ToArrayAsync(token);
        return services.Select(x => ServiceMapper.Mapper(x, new ServiceDTO())).ToArray();
        
    }

    public async Task<ServiceDTO> GetService(int id, CancellationToken token)
    {
        var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == id, token);

        if (service != null)
        {
            return ServiceMapper.Mapper(service, new ServiceDTO());
        }
        
        return null;
    }
}