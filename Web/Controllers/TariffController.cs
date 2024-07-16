using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class TariffController : Controller
{
    private readonly ITariffService _service;

    public TariffController(ITariffService service)
    {
        _service = service;
    }

    [HttpGet, Route("api/tariffs")]
    public Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        return _service.GetAllTariff(token);
    }

    [HttpGet, Route("api/tariffs/{id}")]
    public async Task<IActionResult> GetTariff(CancellationToken token, int id)
    {
        var tariff = await _service.GetTariff(token, id);
        if (tariff != null)
        {
            return new ObjectResult(tariff);
        }

        return new NotFoundResult();
    }
}