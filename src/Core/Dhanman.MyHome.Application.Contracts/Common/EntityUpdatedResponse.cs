namespace Dhanman.MyHome.Application.Contracts.Common;

public sealed class EntityUpdatedResponse
{
    #region Properties

    public Guid Id { get; }

    public int IntId { get; }

    public List<int> IntIds { get; }

    #endregion

    #region Constructor
    public EntityUpdatedResponse(Guid id) => Id = id;

    public EntityUpdatedResponse(int intId) => IntId = intId;

    public EntityUpdatedResponse(List<int> intIds) => IntIds = intIds;

    #endregion
  
}