using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Deliveries;

public class DeliveryPerson : EntityInt,ISoftDeletableEntity,IAuditableEntity
{
    #region Properties
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string MobileNumber { get; set; }
    public Guid CreatedBy { get; protected set; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; protected set; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructor
    public DeliveryPerson(string name, string companyName, string mobileNumber)
    {
        Name = name;
        CompanyName = companyName;
        MobileNumber = mobileNumber;
    }
    #endregion

}
