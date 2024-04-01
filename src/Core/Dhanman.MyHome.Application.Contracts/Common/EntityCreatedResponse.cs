namespace Dhanman.MyHome.Application.Contracts.Common;

public sealed class EntityCreatedResponse
{
    #region Properties
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public Guid Id { get; }

    public int IntId { get; }
    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityCreatedResponse"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    public EntityCreatedResponse(Guid id) => Id = id;

    public EntityCreatedResponse(int id) => IntId = id;
    #endregion
}