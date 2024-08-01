using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class SubscriberService : ISubscriberService
{
    private readonly ApplicationDbContext _context;
    
    public SubscriberService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token)
    {
        var subs = await _context.Subscribers.ToArrayAsync(token);
        return subs.Select(x => new SubscriberDTO(x)).ToArray();
    }

    public async Task<SubscriberDTO> GetSubscriber(int id, CancellationToken token)
    {
        var sub = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id, token);
        
        if (sub != null)
        {
            return new SubscriberDTO(sub);
        }

        return null;
    }
}