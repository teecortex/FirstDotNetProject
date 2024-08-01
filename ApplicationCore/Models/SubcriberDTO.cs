using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Models;

public record SubscriberDTO
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Patronymic { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public int Rating { get; init; }
    public int? TariffId { get; init; }
    public DateTime? DateOfCreation { get; init; }
    public DateTime? DateOfUpdate { get; init; }

    public SubscriberDTO(Subscriber sub)
    {
        Id = sub.Id;
        FirstName = sub.FirstName;
        LastName = sub.LastName;
        Patronymic = sub.Patronymic;
        DateOfBirth = sub.DateOfBirth;
        PhoneNumber = sub.PhoneNumber;
        Email = sub.Email;
        Rating = sub.Rating;
        TariffId = sub.TariffId;
        DateOfCreation = sub.DateOfCreation;
        DateOfUpdate = sub.DateOfUpdate;
    }

}