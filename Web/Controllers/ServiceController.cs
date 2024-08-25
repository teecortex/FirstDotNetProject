using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/services")]
public class ServiceController : Controller
{
    private readonly IServiceService _service;
    private readonly ILogger<ServiceController> _logger;

    public ServiceController(IServiceService service, ILogger<ServiceController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        _logger.LogInformation("Getting all services");
        return await _service.GetAllServices(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<ServiceDTO?> GetService(int id, CancellationToken token)
    {
        _logger.LogInformation($"Getting service by {id}");
        var service = await _service.GetService(id, token);
        if (service != null)
        {
            return service;
        }
        
        return null;
    }
}