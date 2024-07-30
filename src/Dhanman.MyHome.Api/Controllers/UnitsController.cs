using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Contracts.UnitTypes;
using Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;
using Dhanman.MyHome.Application.Features.Units.Command.CreateUnits;
using Dhanman.MyHome.Application.Features.Units.Command.DeleteUnit;
using Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;
using Dhanman.MyHome.Application.Features.Units.Command.UpdateUnit;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Application.Features.UnitTypes;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class UnitsController : ApiController
{
    public UnitsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }


    #region Units     

    [HttpGet(ApiRoutes.Units.GetAllUnits)]
    [ProducesResponseType(typeof(UnitListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnits(Guid apartmentId) =>
    await Result.Success(new GetAllUnitsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Units.GetAllUnitDetails)]
    [ProducesResponseType(typeof(UnitDetailListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitDetails([FromBody] GetUnitDetailRequest? request) =>
    await Result.Create(request, Errors.General.BadRequest)
                .Map(value => new GetUnitDetailsCommand(
                    value.BuildingIds,
                    value.OccupancyIds
                  ))
                .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);

    [HttpPost(ApiRoutes.Units.GetUnitByFloorId)]
    [ProducesResponseType(typeof(UnitByFloorIdListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitDetailsByFloorId([FromBody] GetUnitsByFloorIdRequest? request) =>
    await Result.Create(request, Errors.General.BadRequest)
                .Map(value => new GetUnitByFloorIdCommand(
                    value.BuildingIds,
                    value.FloorIds
                  ))
                .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.Units.GetAllUnitNames)]
    [ProducesResponseType(typeof(UnitNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitNames(Guid apartmentId, int buildingId, int floorId) =>
    await Result.Success(new GetAllUnitNamesQuery(apartmentId, buildingId, floorId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Units.GetUnitById)]
    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFloorsByFloorId(int id) =>
    await Result.Success(new GetUnitByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Units.CreateUnits)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUnitListRequest([FromBody] CreateUnitListRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateUnitCommand(
                value.UnitList
               ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);


    [HttpPost(ApiRoutes.Units.CreateUnit)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUnitRequest([FromBody] CreateUnitsRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateUnitsCommand(
                value.Name,
                value.BuildingId,
                value.FloorId,
                value.UnitTypeId,
                value.OccupantId,
                value.OccupancyId,
                value.Area,
                value.Bhk,
                value.EIntercom,
                value.PhoneExtension,
                value.ApartmentId,
                Guid.NewGuid()
               ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);


    [HttpPut(ApiRoutes.Units.UpdateUnit)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateInvoice([FromBody] UpdateUnitRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateUnitCommand(
                value.Id,
                value.Name,
                value.BuildingId,
                value.FloorId,
                value.UnitTypeId,
                value.OccupantId,
                value.OccupancyId,
                value.Area,
                value.Bhk,
                value.EIntercom,
                value.PhoneExtension
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

    //Delete
    [HttpDelete(ApiRoutes.Units.DeleteUnit)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUnit(int id)
    {
        var result = await Result.Success(new DeleteUnitCommand(id))
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

    #region UnitTypes

    [HttpGet(ApiRoutes.UnitTypes.GetAllUnitTypes)]
    [ProducesResponseType(typeof(UnitTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllUnitTypes() =>
     await Result.Success(new GetAllUnitTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

}
