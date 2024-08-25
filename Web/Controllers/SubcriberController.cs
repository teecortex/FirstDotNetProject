using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using Infrastructure.Entities;


namespace Web.Controllers;

[Route("api/subscribers")]
public class SubscriberController : Controller
{
    private readonly ISubscriberService _service;
    private readonly ILogger<SubscriberController> _logger;

    public SubscriberController(ISubscriberService service, ILogger<SubscriberController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    [HttpGet]
    public Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        _logger.LogInformation("Getting all subscribers");  
        return _service.GetAllSubscribers(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<SubscriberDTO?> GetSubscribers(int id, CancellationToken token)
    {
        _logger.LogInformation($"Getting subscriber by {id}");
        var sub = await _service.GetSubscriber(id, token);
        if (sub != null)
        {
            return sub;
        }

        return null;
    }
}