using Dhanman.MyHome.Domain.Entities.Tickets;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ITicketServiceProviderOtpRepository
{
    #region Methods
    Task<TicketServiceProviderOtp> GetBydIdIAsync(Guid id);

    void Insert(TicketServiceProviderOtp unit);

    void Delete(TicketServiceProviderOtp unit);

    void Update(TicketServiceProviderOtp unit);

    int GetTotalRecordsCount();

    Task<bool> IsFlatValidAsync(string name);
    #endregion
}
