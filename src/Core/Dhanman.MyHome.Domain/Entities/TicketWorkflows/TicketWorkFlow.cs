using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.TicketWorkflows;

public class TicketWorkFlow : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public int Status { get; set; }
    public int NextStatus { get; set; }
    public int PreviousStatus { get; set; }
    public bool IsInitial { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructor
    public TicketWorkFlow(int id, Guid apartmentId, int status, int nextStatus, int previousStatus, bool isInitial)
    {
        Id = id;
        ApartmentId = apartmentId;
        Status = status;
        NextStatus = nextStatus;
        PreviousStatus = previousStatus;
        IsInitial = isInitial;
    }
    #endregion
}