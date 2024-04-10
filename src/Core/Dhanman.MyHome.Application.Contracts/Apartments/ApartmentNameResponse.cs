namespace Dhanman.MyHome.Application.Contracts.Apartments;

public sealed class ApartmentNameResponse
{
    #region Properties 

    public Guid Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public ApartmentNameResponse(Guid id, string name)
    {
        Id = id;
        Name = name;

    }
    #endregion


}
