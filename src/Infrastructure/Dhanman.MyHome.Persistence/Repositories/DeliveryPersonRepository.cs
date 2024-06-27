using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Deliveries;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories
{
    internal class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        #region Properties
        private readonly IApplicationDbContext _dbContext;
        #endregion

        #region Contructor
        public DeliveryPersonRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
        #endregion

        #region Methods
        public async Task<DeliveryPerson?> GetByIdAsync(int id) => await _dbContext.GetByIdIntAsync<DeliveryPerson>(id);
        public void Insert(DeliveryPerson person)=> _dbContext.InsertInt(person);

        public async Task<bool> IsUniqueMobileNumber(string mobileNumber, CancellationToken cancellationToken)
        {
            return !await _dbContext.SetInt<DeliveryPerson>()
                .AnyAsync(dp => dp.MobileNumber == mobileNumber, cancellationToken);
        }
        #endregion

    }
}
