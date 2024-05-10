using Dhanman.MyHome.Domain.Entities.Addresses;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id);

    void Insert(Address address);

    void Delete(Address address);

    void Update(Address updateAddress);

    Address GetAddressDetailsForServiceProvider(Guid addressId);    

}