public sealed class ServiceProviderTicketCategoryResponse
{
    public int Id { get; }
    public int TicketCategoryId { get; }
    public string TicketCategoryName { get; }
    public int ServiceProviderId { get; }
    public string Name { get; }
    public string Email { get; }
    public string ContactNumber { get; }

    public ServiceProviderTicketCategoryResponse(int id ,int ticketCategoryId, string ticketCategoryName, int serviceProviderId, string name, string email, string contactNumber)
    {
        Id = id;
        TicketCategoryId = ticketCategoryId;
        TicketCategoryName = ticketCategoryName ?? "N/A";
        ServiceProviderId = serviceProviderId;
        Name = name ?? "N/A";
        Email = email ?? "N/A";
        ContactNumber = contactNumber ?? "N/A";
    }

    public ServiceProviderTicketCategoryResponse(int id,int ticketCategoryId, string? ticketCategoryName, int serviceProviderId, string? name, string? email)
    {
        Id = id;
        TicketCategoryId = ticketCategoryId;
        TicketCategoryName = ticketCategoryName ?? "N/A";
        ServiceProviderId = serviceProviderId;
        Name = name ?? "N/A";
        Email = email ?? "N/A";
        ContactNumber = "N/A"; 
    }
}
