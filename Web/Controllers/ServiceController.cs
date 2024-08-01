using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/services")]
public class ServiceController : Controller
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        return await _service.GetAllServices(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetService(int id, CancellationToken token)
    {
        var service = await _service.GetService(id, token);
        if (service != null)
        {
            return Ok(service);
        }
        return NotFound();
    }
}