using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Service
{
    [Column("id")]
    public int Id { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("expiration_date")]
    public DateTime ExpirationDate { get; set; }
    [Column("price")] 
    public int Price { get; set; }
    [Column("date_of_creation")]
    public DateTime? DateOfCreation { get; set; }
    [Column("date_of_update")]
    public DateTime? DateOfUpdate { get; set; }
}