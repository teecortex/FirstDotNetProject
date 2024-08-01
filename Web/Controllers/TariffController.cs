using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/tariffs")]
public class TariffController : Controller
{
    private readonly ITariffService _service;

    public TariffController(ITariffService service)
    {
        _service = service;
    }

    [HttpGet]
    public Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        return _service.GetAllTariffs(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetTariff(int id, CancellationToken token)
    {
        var tariff = await _service.GetTariff(id, token);
        if (tariff != null)
        {
            return Ok(tariff);
        }

        return NotFound();
    }
}