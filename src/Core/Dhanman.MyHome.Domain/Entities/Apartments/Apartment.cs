using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using System;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Apartment : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public int ApartmentTypeId { get; set; }
    public int AddressId { get; set; }
    public string Phone { get; set; }
    public string PAN { get; set; }
    public string TAN { get; set; }
    public string AssociationName { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get;  }

    public Guid? ModifiedBy { get;  }
    #endregion

    #region Constructor
    public Apartment(Guid id, string name, int apartmentTypeId,
        int addressId, string phone, string pAN, string tAN, string associationName,
           Guid createdBy)
    {
        Id = id;
        Name = name;
        ApartmentTypeId = apartmentTypeId;
        AddressId = addressId;
        Phone = phone;
        PAN = pAN;
        TAN = tAN;
        AssociationName = associationName;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;

    }    
    #endregion
}
