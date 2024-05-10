using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class AddressRepository : IAddressRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public AddressRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Address?> GetByIdAsync(Guid id) => await _dbContext.GetBydIdAsync<Address>(id);

    public void Insert(Address address) => _dbContext.Insert(address);

    public void Delete(Address address) => _dbContext.Remove(address);

    public void Update(Address updateAddress) => _dbContext?.Update(updateAddress);

    public Address GetAddressDetailsForServiceProvider(Guid addressId)
    {
        Address detailsTask = _dbContext.Set<Address>()
            .Where(x => x.Id == addressId)
            .FirstOrDefault();
        return detailsTask;
    }    
    #endregion
}