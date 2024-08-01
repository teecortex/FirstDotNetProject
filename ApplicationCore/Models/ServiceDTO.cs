using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record ServiceDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public DateTime ExpirationDate { get; init; }
    public int Price { get; init; }
    public DateTime? DateOfCreation { get; init; }
    public DateTime? DateOfUpdate { get; init; }

    public ServiceDTO(Service service)
    {
        Id = service.Id;
        Title = service.Title;
        Description = service.Description;
        ExpirationDate = service.ExpirationDate;
        Price = service.Price;
        DateOfCreation = service.DateOfCreation;
        DateOfUpdate = service.DateOfUpdate;
    }
}