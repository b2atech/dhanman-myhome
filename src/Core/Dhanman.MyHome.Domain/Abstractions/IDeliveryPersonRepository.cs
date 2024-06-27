using Dhanman.MyHome.Domain.Entities.Deliveries;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IDeliveryPersonRepository
{
    Task<DeliveryPerson?> GetByIdAsync(int id);

    void Insert(DeliveryPerson person);

    Task<bool> IsUniqueMobileNumber(string mobileNumber, CancellationToken cancellationToken);
}
