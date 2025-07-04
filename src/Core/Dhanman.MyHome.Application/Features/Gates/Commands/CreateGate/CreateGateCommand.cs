using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.CreateGate;

public class CreateGateCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
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
    public CreateGateCommand(string name, Guid apartmentId, int? buildingId, int gateTypeId, bool isUsedForIn, bool isUsedForOut, bool isAllUsersAllowed, bool isResidentsAllowed, bool isStaffAllowed, bool isVendorAllowed)
    {
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
