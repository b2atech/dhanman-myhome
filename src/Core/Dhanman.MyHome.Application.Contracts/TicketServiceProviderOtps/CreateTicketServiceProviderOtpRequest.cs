namespace Dhanman.MyHome.Application.Contracts.TicketServiceProviderOtps
{
    public class CreateTicketServiceProviderOtpRequest
    {
        public string Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
        public Guid TicketId { get; set; }
    }
}
