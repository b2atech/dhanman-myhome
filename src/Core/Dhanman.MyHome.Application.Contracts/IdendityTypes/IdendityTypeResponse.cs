namespace Dhanman.MyHome.Application.Contracts.IdendityTypes;

public sealed class IdendityTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public IdendityTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
