using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Models;

public record SubscriberDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Rating { get; set; }
    public int? TariffId { get; set; }
    public DateTime? DateOfCreation { get; set; }
    public DateTime? DateOfUpdate { get; set; }
}