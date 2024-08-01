using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;


public interface ISubscriberService
{
    Task<SubscriberDTO[]> GetAllSubscribers(CancellationToken token);

    Task<SubscriberDTO> GetSubscriber(int id, CancellationToken token);
}