using ApplicationCore.Models;
using Infrastructure.Entities;

namespace ApplicationCore.Mappers;

public class ServiceMapper
{
    public static ServiceDTO Mapper(Service service, ServiceDTO dto)
    {
        dto.Id = service.Id;
        dto.Title = service.Title;
        dto.Description = service.Description;
        dto.ExpirationDate = service.ExpirationDate;
        dto.Price = service.Price;
        dto.DateOfCreation = service.DateOfCreation;
        dto.DateOfUpdate = service.DateOfUpdate;

        return dto;
    }
}