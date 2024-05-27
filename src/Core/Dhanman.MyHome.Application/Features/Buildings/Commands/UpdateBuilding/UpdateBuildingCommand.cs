using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.UpdateBuilding;

public sealed class UpdateBuildingCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int BuildingId { get; set; }
    public Guid ApartmentId { get; set; }
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructors
    public UpdateBuildingCommand(int id,Guid apartmentId, string name, int buildingTypeId, int totalUnits)
    {
        BuildingId = id;
        ApartmentId = apartmentId;
        Name = name;
        BuildingTypeId = buildingTypeId;
        TotalUnits = totalUnits;
    }
    #endregion
}
