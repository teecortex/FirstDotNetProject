using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/tariffs")]
public class TariffController : Controller
{
    private readonly ITariffService _service;
    private readonly ILogger<TariffController> _logger;

    public TariffController(ITariffService service, ILogger<TariffController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public Task<TariffDTO[]> GetAllTariffs(CancellationToken token)
    {
        _logger.LogInformation("Getting all tariffs");
        return _service.GetAllTariffs(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<TariffDTO?> GetTariff(int id, CancellationToken token)
    {
        _logger.LogInformation($"Getting tariff by {id}");
        var tariff = await _service.GetTariff(id, token);
        if (tariff != null)
        {
            return tariff;
        }

        return null;
    }
}