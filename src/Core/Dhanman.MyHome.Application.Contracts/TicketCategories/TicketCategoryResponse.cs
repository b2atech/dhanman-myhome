namespace Dhanman.MyHome.Application.Contracts.TicketCatetories;
 
public sealed class TicketCategoryResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public TicketCategoryResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion


}