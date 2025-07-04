using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuildings;

public class CreateBuildingCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructors
    public CreateBuildingCommand(string name, int buildingTypeId, Guid apartmentId, int totalUnits)
    {

        Name = name;
        BuildingTypeId = buildingTypeId;
        ApartmentId = apartmentId;
        TotalUnits = totalUnits;
    }
    #endregion
}
