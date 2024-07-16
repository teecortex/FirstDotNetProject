using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class Subscriber
{
    [Column("id")]
    public int Id { get; set; }
    [Column("first_name")]
    public string First_name { get; set; }
    [Column("last_name")]
    public string Last_name { get; set; }
    [Column("patronymic")]
    public string? Patronymic { get; set; }
    [Column("date_of_birth")]
    public DateTime Date_of_birth { get; set; }
    [Column("phone_number")]
    public string Phone_number { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("rating")]
    public int Rating { get; set; }
    [Column("tarrif_id")]
    public int? Tariff_id { get; set; }
    [Column("date_of_creation")]
    public DateTime? Date_of_creation { get; set; }
    [Column("date_of_update")]
    public DateTime? Date_of_update { get; set; }
}