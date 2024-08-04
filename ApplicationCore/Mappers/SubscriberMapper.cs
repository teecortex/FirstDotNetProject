using ApplicationCore.Models;
using Infrastructure.Entities;

namespace ApplicationCore.Mappers;

public class SubscriberMapper
{
    public static SubscriberDTO Mapper(Subscriber sub, SubscriberDTO dto)
    {
        dto.Id = sub.Id;
        dto.FirstName = sub.FirstName;
        dto.LastName = sub.LastName;
        dto.Patronymic = sub.Patronymic;
        dto.DateOfBirth = sub.DateOfBirth;
        dto.PhoneNumber = sub.PhoneNumber;
        dto.Email = sub.Email;
        dto.Rating = sub.Rating;
        dto.TariffId = sub.TariffId;
        dto.DateOfCreation = sub.DateOfCreation;
        dto.DateOfUpdate = sub.DateOfUpdate;
        return dto;
    }
}