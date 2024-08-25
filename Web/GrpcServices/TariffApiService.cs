using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Web.GrpcServices;

public class TariffApiService : TariffService.TariffServiceBase
{
    private ApplicationDbContext _db;
    private readonly ILogger<TariffApiService> _logger;

    public TariffApiService(ApplicationDbContext dbContext, ILogger<TariffApiService> logger)
    {
        _db = dbContext;
        _logger = logger;
    }

    public override Task<TariffListReply> ListTariffs(Empty request, ServerCallContext context)
    {
        _logger.LogInformation("Getting list of tariffs from db");
        var listReply = new TariffListReply();
        var tariffList = _db.Tariffs.Select(item => new TariffReply() {Id = item.Id, Title = item.Title, 
            Description = item.Description, TypeOfFeeDebit = item.TypeOfFeeDebit, SubscriptionFee = item.SubscriptionFee, 
            MinutesLimit = item.MinutesLimit, InternetTrafficLimit = item.InternetTrafficLimit, Price = item.Price, 
            DateOfCreation = item.DateOfCreation.ToString(), DateOfUpdate = item.DateOfUpdate.ToString()}).ToList();
        
        listReply.Tariffs.AddRange(tariffList);
        
        return Task.FromResult(listReply);
    }

    public override async Task<TariffReply?> GetTariff(GetTariffRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Getting tariff by id={request.Id} from db");
        var tariff = await _db.Tariffs.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (tariff == null)
        {
            _logger.LogInformation("Tariff was not found");
            return new TariffReply();
        }
        else
        {
            _logger.LogInformation("Tariff successfully found");
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