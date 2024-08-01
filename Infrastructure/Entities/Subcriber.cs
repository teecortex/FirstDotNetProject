using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Subscriber
{
    [Column("id")]
    public int Id { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    [Column("patronymic")]
    public string? Patronymic { get; set; }
    [Column("date_of_birth")]
    public DateTime DateOfBirth { get; set; }
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("rating")]
    public int Rating { get; set; }
    [Column("tarrif_id")]
    public int? TariffId { get; set; }
    [Column("date_of_creation")]
    public DateTime? DateOfCreation { get; set; }
    [Column("date_of_update")]
    public DateTime? DateOfUpdate { get; set; }
}