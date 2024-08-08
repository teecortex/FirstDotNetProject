using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Web.GrpcServices;

public class SubscriberApiService : SubscriberService.SubscriberServiceBase
{
    private ApplicationDbContext db;
    private readonly ILogger<SubscriberApiService> _logger;

    public SubscriberApiService(ApplicationDbContext dbContext, ILogger<SubscriberApiService> logger)
    {
        db = dbContext;
        _logger = logger;
    }

    public override Task<SubscriberListReply> ListSubscribers(Empty request, ServerCallContext context)
    {
        var listReply = new SubscriberListReply();
        var subscriberList = db.Subscribers.Select(item => new SubscriberReply() { Id = item.Id, FirstName = item.FirstName, 
            LastName = item.LastName, Patronymic = item.Patronymic, DateOfBirth = Timestamp.FromDateTime(item.DateOfBirth.ToUniversalTime()), 
            PhoneNumber = item.PhoneNumber, Email = item.Email, Rating = item.Rating, TariffId = item.TariffId, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Subscribers.AddRange(subscriberList);
        return Task.FromResult(listReply);
    }

    public override async Task<SubscriberReply> GetSubscriber(GetSubscriberRequest request, ServerCallContext context)
    {
        var sub = await db.Subscribers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (sub == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Subscriber not found"));
        }
        else
        {
            var reply = new SubscriberReply()
            {
                Id = sub.Id, FirstName = sub.FirstName,
                LastName = sub.LastName, Patronymic = sub.Patronymic,
                DateOfBirth = Timestamp.FromDateTime(sub.DateOfBirth.ToUniversalTime()),
                PhoneNumber = sub.PhoneNumber, Email = sub.Email, Rating = sub.Rating, TariffId = sub.TariffId,
                DateOfCreation = sub.DateOfCreation.ToString(), DateOfUpdate = sub.DateOfUpdate.ToString()
            };
            return reply;
        }
    }
}