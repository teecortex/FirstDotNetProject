using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Models;

public record SubscriberDTO
{
    public int Id { get; init; }
    public string First_name { get; init; }
    public string Last_name { get; init; }
    public string? Patronymic { get; init; }
    public DateTime Date_of_birth { get; init; }
    public string Phone_number { get; init; }
    public string Email { get; init; }
    public int Rating { get; init; }
    public int? Tariff_id { get; init; }
    public DateTime? Date_of_creation { get; init; }
    public DateTime? Date_of_update { get; init; }

    public SubscriberDTO(Subscriber sub)
    {
        Id = sub.Id;
        First_name = sub.First_name;
        Last_name = sub.Last_name;
        Patronymic = sub.Patronymic;
        Date_of_birth = sub.Date_of_birth;
        Phone_number = sub.Phone_number;
        Email = sub.Email;
        Rating = sub.Rating;
        Tariff_id = sub.Tariff_id;
        Date_of_creation = sub.Date_of_creation;
        Date_of_update = sub.Date_of_update;
    }

}