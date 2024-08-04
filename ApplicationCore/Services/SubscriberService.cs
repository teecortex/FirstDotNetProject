using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
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
        return subs.Select(x => SubscriberMapper.Mapper(x, new SubscriberDTO())).ToArray();
    }

    public async Task<SubscriberDTO> GetSubscriber(int id, CancellationToken token)
    {
        var sub = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id, token);
        
        if (sub != null)
        {
            return SubscriberMapper.Mapper(sub, new SubscriberDTO());
        }

        return null;
    }
}