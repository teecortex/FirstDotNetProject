using ApplicationCore.Models;
using Infrastructure.Entities;

namespace ApplicationCore.Mappers;

public class TariffMapper
{
    public static TariffDTO Mapper(Tariff tariff, TariffDTO dto)
    {
        dto.Id = tariff.Id;
        dto.Title = tariff.Title;
        dto.Description = tariff.Description;
        dto.TypeOfFeeDebit = tariff.TypeOfFeeDebit;
        dto.SubscriptionFee = tariff.SubscriptionFee;
        dto.MinutesLimit = tariff.MinutesLimit;
        dto.InternetTrafficLimit = tariff.InternetTrafficLimit;
        dto.Price = tariff.Price;
        dto.DateOfCreation = tariff.DateOfCreation;
        dto.DateOfUpdate = tariff.DateOfUpdate;
        
        return dto;
    }
}