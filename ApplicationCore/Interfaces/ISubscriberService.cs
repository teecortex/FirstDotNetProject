using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;


public interface ISubscriberService
{
    Task<SubscriberDTO[]> GetAllSubscriber(CancellationToken token);

    Task<SubscriberDTO> GetSubscriber(CancellationToken token, int id);
}