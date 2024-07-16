using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record TariffDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string Type_of_fee_debit { get; init; }
    public int Subscription_fee { get; init; }
    public int Minutes_limit { get; init; }
    public int Internet_traffic_limit { get; init; }
    public int Price { get; init; }
    public DateTime? Date_of_creation { get; init; }
    public DateTime? Date_of_update { get; init; }

    public TariffDTO(Tariff tariff)
    {
        Id = tariff.Id;
        Title = tariff.Title;
        Description = tariff.Description;
        Type_of_fee_debit = tariff.Type_of_fee_debit;
        Subscription_fee = tariff.Subscription_fee;
        Minutes_limit = tariff.Minutes_limit;
        Internet_traffic_limit = tariff.Internet_traffic_limit;
        Price = tariff.Price;
        Date_of_creation = tariff.Date_of_creation;
        Date_of_update = tariff.Date_of_update;
    }
};