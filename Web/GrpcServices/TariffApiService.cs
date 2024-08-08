using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.GrpcServices;

public class TariffApiService : TariffService.TariffServiceBase
{
    private ApplicationDbContext db;
    private readonly ILogger<TariffApiService> _logger;

    public TariffApiService(ApplicationDbContext dbContext, ILogger<TariffApiService> logger)
    {
        db = dbContext;
        _logger = logger;
    }

    public override Task<TariffListReply> ListTariffs(Empty request, ServerCallContext context)
    {
        var listReply = new TariffListReply();
        var tariffList = db.Tariffs.Select(item => new TariffReply() {Id = item.Id, Title = item.Title, 
            Description = item.Description, TypeOfFeeDebit = item.TypeOfFeeDebit, SubscriptionFee = item.SubscriptionFee, 
            MinutesLimit = item.MinutesLimit, InternetTrafficLimit = item.InternetTrafficLimit, Price = item.Price, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Tariffs.AddRange(tariffList);
        
        return Task.FromResult(listReply);
    }

    public override async Task<TariffReply> GetTariff(GetTariffRequest request, ServerCallContext context)
    {
        var tariff = await db.Tariffs.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (tariff == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Tariff not found"));
        }
        else
        {
            var reply = new TariffReply()
            {
                Id = tariff.Id, Title = tariff.Title,
                Description = tariff.Description, TypeOfFeeDebit = tariff.TypeOfFeeDebit,
                SubscriptionFee = tariff.SubscriptionFee,
                MinutesLimit = tariff.MinutesLimit, InternetTrafficLimit = tariff.InternetTrafficLimit, Price = tariff.Price,
                DateOfCreation = tariff.DateOfCreation.ToString(), DateOfUpdate = tariff.DateOfUpdate.ToString()
            };

            return reply;
        }
    }
}