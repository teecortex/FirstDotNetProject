using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.GrpcServices;

public class ServiceApiService : ServiceService.ServiceServiceBase
{
    private ApplicationDbContext db;
    private readonly ILogger<ServiceApiService> _logger;

    public ServiceApiService(ApplicationDbContext dbContext, ILogger<ServiceApiService> logger)
    {
        db = dbContext;
        _logger = logger;
    }

    public override Task<ServiceListReply> ListServices(Empty request, ServerCallContext context)
    {
        var listReply = new ServiceListReply();
        var serviceList = db.Services.Select(item => new ServiceReply() { Id = item.Id, Title = item.Title, 
            Description = item.Description, ExpirationDate = Timestamp.FromDateTime(item.ExpirationDate.ToUniversalTime()), Price = item.Price, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Services.AddRange(serviceList);
        return Task.FromResult(listReply);
    }

    public override async Task<ServiceReply> GetService(GetServiceRequest request, ServerCallContext context)
    {
        var service = await db.Services.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (service == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Service not found"));
        }
        else
        {
            var reply = new ServiceReply()
            {
                Id = service.Id, Title = service.Title,
                Description = service.Description, ExpirationDate = Timestamp.FromDateTime(service.ExpirationDate.ToUniversalTime()),
                Price = service.Price,
                DateOfCreation = service.DateOfCreation.ToString(), DateOfUpdate = service.DateOfUpdate.ToString()
            };

            return reply;
        }
    }
}