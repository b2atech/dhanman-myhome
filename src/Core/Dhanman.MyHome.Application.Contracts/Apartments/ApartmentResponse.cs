namespace Dhanman.MyHome.Application.Contracts.Apartments;

public sealed class ApartmentResponse
{
    #region Properties 

    public Guid Id { get; }
    public string Name { get; }
    public int ApartmentTypeId { get; }
    public string ApartmentTypeName { get; }
    public Guid AddressId { get; }
    public string Phone { get; }
    public string PAN { get; }
    public string TAN { get; }
    public string AssociationName { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }

    #endregion

    #region Constructor
    public ApartmentResponse(Guid id, string name, int apartmentTypeId, string apartmentTypeName, Guid addressId, string phone, string pAN, string tAN, string associationName, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc)
    {
        Id = id;
        Name = name;
        ApartmentTypeId = apartmentTypeId;
        ApartmentTypeName = apartmentTypeName;
        AddressId = addressId;
        Phone = phone;
        PAN = pAN;
        TAN = tAN;
        AssociationName = associationName;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }
    #endregion

   
}
