namespace Dhanman.MyHome.Application.Contracts.TicketPriorities;

public sealed class TicketPriorityResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public TicketPriorityResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion


}