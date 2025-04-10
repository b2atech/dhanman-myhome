﻿using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Features.Floors.Commands.CreateFloor;
using Dhanman.MyHome.Application.Features.Floors.Commands.DeleteFloor;
using Dhanman.MyHome.Application.Features.Floors.Commands.UpdateFloor;
using Dhanman.MyHome.Application.Features.Floors.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class FloorsController : ApiController
{
    public FloorsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Floors

    [HttpGet(ApiRoutes.Floors.GetFloors)]
    [ProducesResponseType(typeof(FloorListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllFloors(Guid apartmentId) =>
    await Result.Success(new GetAllFloorsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

   
    [HttpGet(ApiRoutes.Floors.GetFloorById)]
    [ProducesResponseType(typeof(FloorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFloorsByBuildingId(int id) =>
    await Result.Success(new GetFloorByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Floors.CreateFloor)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateFloor([FromBody] CreateFloorRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateFloorCommand(
             value.Name,
             value.ApartmentId,
             value.BuildingId,
             value.TotalUnits
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.Floors.UpdateFloor)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFloors([FromBody] UpdateFloorRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateFloorCommand(
                value.Id,
                value.Name,
                value.BuildingId,
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

    [HttpDelete(ApiRoutes.Floors.DeleteFloorById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFloorById(int id)
    {
        var result = await Result.Success(new DeleteFloorCommand(id))
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

}
