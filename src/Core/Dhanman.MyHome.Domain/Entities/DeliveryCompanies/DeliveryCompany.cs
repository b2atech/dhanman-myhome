using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.DeliveryCompanies;

public class DeliveryCompany : EntityInt, IAuditableEntity, ISoftDeletableEntity
{

    #region Properties
    public string Name { get; set; }
    public int DeliveryCompanyCategoryId { get; set; }
    public string Icon { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructors
    public DeliveryCompany(int id, string name, int deliveryCompanyCategoryId, string icon, Guid createdBy)
    {
        Id = id;
        Name = name;
        DeliveryCompanyCategoryId = deliveryCompanyCategoryId;
        Icon = icon;   
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.Now;
    }
    public DeliveryCompany() { }
    
    #endregion

}