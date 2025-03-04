namespace Dhanman.MyHome.Application.Contracts.TicketCatetories;
 
public sealed class TicketCatetoryResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public TicketCatetoryResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion


}