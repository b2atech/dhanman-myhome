namespace Dhanman.MyHome.Application.Contracts.TicketStatuses;

public sealed class TicketStatusResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }    
    #endregion

    #region Constructor
    public TicketStatusResponse(int id, string name)
    {
        Id = id;
        Name = name;        
    }
    #endregion


}
