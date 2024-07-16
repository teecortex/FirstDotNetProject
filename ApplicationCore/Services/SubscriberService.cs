using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class SubscriberService : ISubscriberService
{
    private readonly ApplicationContext _context;
    
    public SubscriberService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<SubscriberDTO[]> GetAllSubscriber(CancellationToken token)
    {
        var subs = await _context.subscribers.ToArrayAsync(token);
        return subs.Select(x => new SubscriberDTO(x)).ToArray();
    }

    public async Task<SubscriberDTO> GetSubscriber(CancellationToken token, int id)
    {
        var subs = await _context.subscribers.ToArrayAsync(token);
        var sub = subs.FirstOrDefault(x => x.Id == id);
        if (sub != null)
        {
            return new SubscriberDTO(sub);
        }

        return null;
    }
}