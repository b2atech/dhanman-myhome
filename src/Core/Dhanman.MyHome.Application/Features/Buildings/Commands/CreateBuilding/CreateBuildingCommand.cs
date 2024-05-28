using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuildings;

public class CreateBuildingCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public int TotalUnits { get; set; }
    public bool IsUsedForIn { get; set; }
    public bool IsUsedForOut { get; set; }
    public bool IsAllUsersAllowed { get; set; }
    public bool IsResidentsAllowed { get; set; }
    public bool IsStaffAllowed { get; set; }
    public bool IsVendorAllowed { get; set; }
    #endregion

    #region Constructors
    public CreateBuildingCommand(string name, int buildingTypeId, Guid apartmentId, int totalUnits)
    {

        Name = name;
        BuildingTypeId = buildingTypeId;
        ApartmentId = apartmentId;
        TotalUnits = totalUnits;
        IsUsedForIn = true;
        IsUsedForOut = true;
        IsAllUsersAllowed = true;
        IsResidentsAllowed = true;
        IsStaffAllowed = true;
        IsVendorAllowed = true;
    }
    #endregion
}
