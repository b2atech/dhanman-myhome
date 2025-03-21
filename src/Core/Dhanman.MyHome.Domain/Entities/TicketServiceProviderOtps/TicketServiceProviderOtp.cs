using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Tickets;

public class TicketServiceProviderOtp : Entity
{
    #region Properties
    public string Otp { get; set; }
    public DateTime ExpirationTime { get; set; }
    public Guid TicketId { get; set; }
   
    #endregion

    #region Constructors
    public TicketServiceProviderOtp() { }

    public TicketServiceProviderOtp(Guid id, string otp, DateTime expirationTime, Guid ticketId)
    {
        Id = id;
        Otp = otp;
        ExpirationTime = expirationTime;
        TicketId = ticketId;
       
    }
    #endregion
}
