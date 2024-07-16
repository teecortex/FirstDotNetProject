using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ServiceController : Controller
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    [HttpGet, Route("api/services")]
    public async Task<ServiceDTO[]> GetAllServices(CancellationToken token)
    {
        return await _service.GetAllServices(token);
    }

    [HttpGet, Route("api/services/{id}")]
    public async Task<IActionResult> GetService(CancellationToken token, int id)
    {
        var service = await _service.GetService(token, id);
        if (service != null)
        {
            return new ObjectResult(service);
        }

        return new NotFoundResult();
    }
}