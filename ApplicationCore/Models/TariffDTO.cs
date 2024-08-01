using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record TariffDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string TypeOfFeeDebit { get; init; }
    public int SubscriptionFee { get; init; }
    public int MinutesLimit { get; init; }
    public int InternetTrafficLimit { get; init; }
    public int Price { get; init; }
    public DateTime? DateOfCreation { get; init; }
    public DateTime? DateOfUpdate { get; init; }

    public TariffDTO(Tariff tariff)
    {
        Id = tariff.Id;
        Title = tariff.Title;
        Description = tariff.Description;
        TypeOfFeeDebit = tariff.TypeOfFeeDebit;
        SubscriptionFee = tariff.SubscriptionFee;
        MinutesLimit = tariff.MinutesLimit;
        InternetTrafficLimit = tariff.InternetTrafficLimit;
        Price = tariff.Price;
        DateOfCreation = tariff.DateOfCreation;
        DateOfUpdate = tariff.DateOfUpdate;
    }
};