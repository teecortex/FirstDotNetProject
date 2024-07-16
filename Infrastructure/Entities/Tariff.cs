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
    public string Type_of_fee_debit { get; set; }
    [Column("subscription_fee")]
    public int Subscription_fee { get; set; }
    [Column("minutes_limit")]
    public int Minutes_limit { get; set; }
    [Column("internet_traffic_limit")]
    public int Internet_traffic_limit { get; set; }
    [Column("price")]
    public int Price { get; set; }
    [Column("date_of_creation")]
    public DateTime? Date_of_creation { get; set; }
    [Column("date_of_update")]
    public DateTime? Date_of_update { get; set; }
}