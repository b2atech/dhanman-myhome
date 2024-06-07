using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Residents;

public class Resident : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public Guid? PermanentAddressId { get; set; }
    public int ResidentTypeId { get; set; }
    public int OccupancyStatusId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public Resident(int id, string firstName, string lastName, string email, string contactNumber, Guid? permanentAddressId, int residentTypeId, int occupancyStatusId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;       
    }
    #endregion
}
