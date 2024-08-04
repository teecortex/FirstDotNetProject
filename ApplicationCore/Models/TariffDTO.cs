using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record TariffDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string TypeOfFeeDebit { get; set; }
    public int SubscriptionFee { get; set; }
    public int MinutesLimit { get; set; }
    public int InternetTrafficLimit { get; set; }
    public int Price { get; set; }
    public DateTime? DateOfCreation { get; set; }
    public DateTime? DateOfUpdate { get; set; }
};