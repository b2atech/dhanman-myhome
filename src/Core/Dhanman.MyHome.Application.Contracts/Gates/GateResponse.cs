namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    public Guid ApartmentId { get; }
    public string ApartmentName { get; }
    public int? BuildingId { get; }
    public string BuildingName { get; }
    public int GateTypeId { get; }
    public string GateTypeName { get; }
    public bool IsUsedForIn { get; }
    public bool IsUsedForOut { get; }
    public bool IsAllUsersAllowed { get; }
    public bool IsResidentsAllowed { get; }
    public bool IsStaffAllowed { get; }
    public bool IsVendorAllowed { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }

    #endregion

    #region Constructor
    public GateResponse(int id,
         string name,
         Guid apartmentId,
         string apartmentName,
         int? buildingId,
         string buildingName,
         int gateTypeId,
         string gateTypeName,
         bool isUsedForIn,
         bool isUsedForOut,
         bool isAllUsersAllowed,
         bool isResidentsAllowed,
         bool isStaffAllowed,
         bool isVendorAllowed,
         Guid createdBy,
         DateTime createdOnUtc)
    {
        Id = id;
        Name = name;
        ApartmentId = apartmentId;
        ApartmentName = apartmentName;
        BuildingId = buildingId;
        BuildingName = buildingName;
        GateTypeId = gateTypeId;
        GateTypeName = gateTypeName;
        IsUsedForIn = isUsedForIn;
        IsUsedForOut = isUsedForOut;
        IsAllUsersAllowed = isAllUsersAllowed;
        IsResidentsAllowed = isResidentsAllowed;
        IsStaffAllowed = isStaffAllowed;
        IsVendorAllowed = isVendorAllowed;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
    }
    #endregion


}
