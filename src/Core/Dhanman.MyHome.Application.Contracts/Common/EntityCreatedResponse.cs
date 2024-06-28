namespace Dhanman.MyHome.Application.Contracts.Common;

public sealed class EntityCreatedResponse
{
    #region Properties
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public Guid Id { get; }
    public int IntId { get; }
    public List<int> IntIds { get; }
    #endregion

    #region Constructor
    public EntityCreatedResponse(Guid id) => Id = id;
    public EntityCreatedResponse(int id) => IntId = id;
    public EntityCreatedResponse(List<int> ids) => IntIds = ids;
    #endregion
}