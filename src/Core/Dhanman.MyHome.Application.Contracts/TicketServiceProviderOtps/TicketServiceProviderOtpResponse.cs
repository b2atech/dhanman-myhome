namespace Dhanman.MyHome.Application.Contracts.TicketServiceProviderOtps;

public class TicketServiceProviderOtpResponse
{
    public Guid Id { get; set; }
    public string Otp { get; set; }
    public DateTime ExpirationTime { get; set; }
    public Guid TicketId { get; set; }
}
