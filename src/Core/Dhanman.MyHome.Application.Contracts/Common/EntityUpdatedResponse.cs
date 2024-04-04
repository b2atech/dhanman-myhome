namespace Dhanman.MyHome.Application.Contracts.Common;

public sealed class EntityUpdatedResponse
{
    #region Constructor
    public EntityUpdatedResponse(Guid id) => Id = id;

    public EntityUpdatedResponse(int intId) => IntId = intId;

    #endregion

    #region Properties

    public Guid Id { get; }

    public int IntId { get; }

    #endregion
}