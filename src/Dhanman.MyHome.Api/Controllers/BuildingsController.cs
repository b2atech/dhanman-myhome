using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Contracts.BuildingTypes;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;
using Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuildings;
using Dhanman.MyHome.Application.Features.Buildings.Commands.DeleteBuilding;
using Dhanman.MyHome.Application.Features.Buildings.Commands.UpdateBuilding;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using Dhanman.MyHome.Application.Features.BuildingTypes.Queries;
using Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class BuildingsController : ApiController
{
    public BuildingsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {

    }

    #region Buildings     

    [HttpGet(ApiRoutes.Buildings.GetAllBuildings)]
    [ProducesResponseType(typeof(BuildingListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildings(Guid apartmentId) =>
    await Result.Success(new GetAllBuildingsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);




    [HttpGet(ApiRoutes.Buildings.GetBuildingById)]
    [ProducesResponseType(typeof(BuildingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBuildingById(int id) =>
    await Result.Success(new GetBuildingByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [HttpPost(ApiRoutes.Buildings.CreateBuilding)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBuilding([FromBody] CreateBuildingRequest? request) =>
      await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new CreateBuildingCommand(
              value.Name,
              value.BuildingTypeId,
              value.ApartmentId,
              value.TotalUnits
          ))
          .Bind(command => Mediator.Send(command))
          .Match(Ok, BadRequest);


    [HttpPut(ApiRoutes.Buildings.UpdateBuilding)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBuilding([FromBody] UpdateBuildingRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateBuildingCommand(
                value.Id,
                value.ApartmentId,
                value.Name,
                value.BuildingTypeId,
                value.TotalUnits
                ))
            .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    [HttpDelete(ApiRoutes.Buildings.DeleteBuildingById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBuildingById(int id)
    {
        var result = await Result.Success(new DeleteBuildingCommand(id))
                    .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }


    #endregion

    #region BuildingType

    [HttpGet(ApiRoutes.BuildingsTypes.GetAllBuildingTypes)]
    [ProducesResponseType(typeof(BuildingTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildingTypes() =>
    await Result.Success(new GetAllBuildingTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region OccupancyTypes     

    [HttpGet(ApiRoutes.OccupancyTypes.GetAllOccupancyTypes)]
    [ProducesResponseType(typeof(OccupancyTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOccupancyTypes() =>
    await Result.Success(new GetAllOccupancyTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

}