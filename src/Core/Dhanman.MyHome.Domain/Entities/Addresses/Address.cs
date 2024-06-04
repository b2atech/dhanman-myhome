using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Addresses;
public class Address : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties

    public Guid CountryId { get; set; }
    public Guid StateId { get; set; }
    public Guid? CityId { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string ZipCode { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructor
    public Address(Guid id, Guid countryId, Guid stateId, Guid? cityId, string addressLine1, string addressLine2, string zipCode)
    {
        Id = id;
        CountryId = countryId;
        StateId = stateId;
        CityId = cityId;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        ZipCode = zipCode;
    }
    #endregion
}