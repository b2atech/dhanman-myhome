using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.UpdateGate;

public class UpdateGateCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int GateId { get; set; }
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int? BuildingId { get; set; }
    public int GateTypeId { get; set; }
    public bool IsUsedForIn { get; set; }
    public bool IsUsedForOut { get; set; }
    public bool IsAllUsersAllowed { get; set; }
    public bool IsResidentsAllowed { get; set; }
    public bool IsStaffAllowed { get; set; }
    public bool IsVendorAllowed { get; set; }
    #endregion

    #region Constructors
    public UpdateGateCommand(int id, string name, Guid apartmentId, int? buildingId, int gateTypeId, bool isUsedForIn, bool isUsedForOut, bool isAllUsersAllowed, bool isResidentsAllowed, bool isStaffAllowed, bool isVendorAllowed)
    {
        GateId = id;
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        GateTypeId = gateTypeId;
        IsUsedForIn = isUsedForIn;
        IsUsedForOut = isUsedForOut;
        IsAllUsersAllowed = isAllUsersAllowed;
        IsResidentsAllowed = isResidentsAllowed;
        IsStaffAllowed = isStaffAllowed;
        IsVendorAllowed = isVendorAllowed;
    }
    #endregion
}
