using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;


namespace Web.Controllers;

[Route("api/subscribers")]
public class SubscriberController : Controller
{
    private readonly ISubscriberService _service;

    public SubscriberController(ISubscriberService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        return _service.GetAllSubscribers(token);
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetSubscribers(int id, CancellationToken token)
    {
        var sub = await _service.GetSubscriber(id, token);
        if (sub != null)
        {
            return Ok(sub);
        }

        return NotFound();
    }
}