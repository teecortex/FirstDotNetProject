using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;


namespace Web.Controllers;

public class SubscriberController : Controller
{
    private readonly ISubscriberService _service;

    public SubscriberController(ISubscriberService service)
    {
        _service = service;
    }
    
    [HttpGet, Route("api/subscribers")]
    public Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        return _service.GetAllSubscriber(token);
    }

    [HttpGet, Route("api/subscribers/{id}")]
    public async Task<IActionResult> GetSubscriber(CancellationToken token, int id)
    {
        var sub = await _service.GetSubscriber(token, id);
        if (sub != null)
        {
            return new ObjectResult(sub);
        }

        return new NotFoundResult();
    }
}