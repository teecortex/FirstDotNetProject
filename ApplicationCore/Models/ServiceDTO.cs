using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record ServiceDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public DateTime Expiration_date { get; init; }
    public int Price { get; init; }
    public DateTime? Date_of_creation { get; init; }
    public DateTime? Date_of_update { get; init; }

    public ServiceDTO(Service service)
    {
        Id = service.Id;
        Title = service.Title;
        Description = service.Description;
        Expiration_date = service.Expiration_date;
        Price = service.Price;
        Date_of_creation = service.Date_of_creation;
        Date_of_update = service.Date_of_update;
    }
}