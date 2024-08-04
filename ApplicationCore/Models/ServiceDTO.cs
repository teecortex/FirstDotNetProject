using Infrastructure.Entities;

namespace ApplicationCore.Models;

public record ServiceDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Price { get; set; }
    public DateTime? DateOfCreation { get; set; }
    public DateTime? DateOfUpdate { get; set; }
}