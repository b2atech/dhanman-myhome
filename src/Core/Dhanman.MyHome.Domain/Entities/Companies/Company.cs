using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Companies;

public class Company : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public Guid OrganizationId { get; set; }
    public bool IsApartment { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructors
    public Company() { }

    public Company(Guid id, Guid organizationId,string name, bool isApartment)
    {
        Id = id;
        OrganizationId = organizationId;
        Name = name;       
        IsApartment = isApartment;
    } 
    #endregion
}