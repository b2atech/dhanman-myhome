using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.DeliveryCompanyCategories;
 
public class DeliveryCompanyCategory : EntityInt, ISoftDeletableEntity
{

    #region Properties
    public string Name { get; set; }    
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }   
    #endregion

    #region Constructors
    public DeliveryCompanyCategory(int id, string name)
    {
        Id = id;
        Name = name;        
    }
    public DeliveryCompanyCategory() { }

    #endregion

}