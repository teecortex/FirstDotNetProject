using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Web.GrpcServices;

public class SubscriberApiService : SubscriberService.SubscriberServiceBase
{
    private ApplicationDbContext _db;
    private readonly ILogger<SubscriberApiService> _logger;

    public SubscriberApiService(ApplicationDbContext dbContext, ILogger<SubscriberApiService> logger)
    {
        _db = dbContext;
        _logger = logger;
    }

    public override Task<SubscriberListReply> ListSubscribers(Empty request, ServerCallContext context)
    {
        _logger.LogInformation("Getting list of subscribers from db");
        var listReply = new SubscriberListReply();
        var subscriberList = _db.Subscribers.Select(item => new SubscriberReply() { Id = item.Id, FirstName = item.FirstName, 
            LastName = item.LastName, Patronymic = item.Patronymic, DateOfBirth = item.DateOfBirth.ToString(), 
            PhoneNumber = item.PhoneNumber, Email = item.Email, Rating = item.Rating, TariffId = item.TariffId, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Subscribers.AddRange(subscriberList);
        return Task.FromResult(listReply);
    }

    public override async Task<SubscriberReply?> GetSubscriber(GetSubscriberRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Getting subscriber by id={request.Id} from db");
        var sub = await _db.Subscribers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (sub == null)
        {
            _logger.LogInformation("Subscriber was not found");
            return new SubscriberReply();
        }
        else
        {
            _logger.LogInformation("Subscriber successfully found");
            var reply = new SubscriberReply()
            {
                Id = sub.Id, FirstName = sub.FirstName,
                LastName = sub.LastName, Patronymic = sub.Patronymic,
                DateOfBirth = sub.DateOfBirth.ToString(),
                PhoneNumber = sub.PhoneNumber, Email = sub.Email, Rating = sub.Rating, TariffId = sub.TariffId,
                DateOfCreation = sub.DateOfCreation.ToString(), DateOfUpdate = sub.DateOfUpdate.ToString()
            };
            return reply;
        }
    }
}