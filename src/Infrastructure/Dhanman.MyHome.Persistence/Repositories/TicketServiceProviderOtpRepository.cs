using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories
{
    public sealed class TicketServiceProviderOtpRepository : ITicketServiceProviderOtpRepository
    {
        #region Properties
        private readonly IApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public TicketServiceProviderOtpRepository(IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public void Delete(TicketServiceProviderOtp unit)
        {
            throw new NotImplementedException();
        }

        public Task<TicketServiceProviderOtp> GetBydIdIntAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods


        public async Task<TicketServiceProviderOtp?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TicketServiceProviderOtp>()
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TicketServiceProviderOtp>> GetByTicketIdAsync(Guid ticketId)
        {
            return await _dbContext.Set<TicketServiceProviderOtp>()
                                    .Where(x => x.TicketId == ticketId)
                                    .ToListAsync();
        }

        public Task<int> GetLastUnitIdAsync()
        {
            throw new NotImplementedException();
        }

        public int GetTotalRecordsCount()
        {
            throw new NotImplementedException();
        }

        public void Insert(TicketServiceProviderOtp ticket) => _dbContext.Insert(ticket);
        public void Update(TicketServiceProviderOtp ticket) => _dbContext.Update(ticket);
        public Task<bool> IsFlatValidAsync(string name)
        {
            throw new NotImplementedException();
        }

     

        public async Task UpdateAsync(TicketServiceProviderOtp entity)
        {
            _dbContext.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

      

        public Task<TicketServiceProviderOtp> GetBydIdIAsync(Guid id) => _dbContext.GetBydIdAsync<TicketServiceProviderOtp>(id);
     


        #endregion
    }
}
