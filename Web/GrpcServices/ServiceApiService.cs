using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.GrpcServices;

public class ServiceApiService : ServiceService.ServiceServiceBase
{
    private ApplicationDbContext _db;
    private readonly ILogger<ServiceApiService> _logger;

    public ServiceApiService(ApplicationDbContext dbContext, ILogger<ServiceApiService> logger)
    {
        _db = dbContext;
        _logger = logger;
    }

    public override Task<ServiceListReply> ListServices(Empty request, ServerCallContext context)
    {
        _logger.LogInformation("Getting list of services from db");
        var listReply = new ServiceListReply();
        var serviceList = _db.Services.Select(item => new ServiceReply() { Id = item.Id, Title = item.Title, 
            Description = item.Description, ExpirationDate = item.ExpirationDate.ToString(), Price = item.Price, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Services.AddRange(serviceList);
        return Task.FromResult(listReply);
    }

    public override async Task<ServiceReply?> GetService(GetServiceRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Getting service by id={request.Id} from db");
        var service = await _db.Services.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (service == null)
        {
            _logger.LogInformation("Service was not found");
            // throw new RpcException(new Status(StatusCode.NotFound, "Service not found"));
            return null;
        }
        else
        {
            _logger.LogInformation("Service successfully found");
            var reply = new ServiceReply()
            {
                Id = service.Id, Title = service.Title,
                Description = service.Description, ExpirationDate = service.ExpirationDate.ToString(),
                Price = service.Price,
                DateOfCreation = service.DateOfCreation.ToString(), DateOfUpdate = service.DateOfUpdate.ToString()
            };

            return reply;
        }
    }
}