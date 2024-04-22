namespace Dhanman.MyHome.Application.Contracts.Visitors;
public sealed class VisitorNameResponse
{
    #region Properties

    public int Id { get; }
    public string FirstName { get; }
    public string? LastName { get; }    

    #endregion

    #region Constructor
    public VisitorNameResponse(int id, string firstName, string? lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;        
    }
    #endregion
}