using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Tariff
{
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("type_of_fee_debit")]
    public string TypeOfFeeDebit { get; set; }
    [Column("subscription_fee")]
    public int SubscriptionFee { get; set; }
    [Column("minutes_limit")]
    public int MinutesLimit { get; set; }
    [Column("internet_traffic_limit")]
    public int InternetTrafficLimit { get; set; }
    [Column("price")]
    public int Price { get; set; }
    [Column("date_of_creation")]
    public DateTime? DateOfCreation { get; set; }
    [Column("date_of_update")]
    public DateTime? DateOfUpdate { get; set; }
}