namespace Dhanman.MyHome.Application.Contracts.Common;

public sealed class EntityDeletedResponse
{
    #region Properties
    public Guid Id { get; }
    public int IntId { get; }
    #endregion

    #region Constructor
    public EntityDeletedResponse(Guid id) => Id = id;
    public EntityDeletedResponse(int id) => IntId = id;
    #endregion
}
