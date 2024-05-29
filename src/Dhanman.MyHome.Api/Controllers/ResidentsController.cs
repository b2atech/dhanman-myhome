using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestRejectStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Queries;
using Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;
using Dhanman.MyHome.Application.Features.Residents.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ResidentsController : ApiController
{
    public ResidentsController(IMediator mediator) : base(mediator)
    {
    }


    #region Residents  

    [HttpPost(ApiRoutes.Residents.CreateResident)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentRequest([FromBody] CreateResidentRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateResidentCommand(
                value.UnitId,
                value.FirstName,
                value.LastName,
                value.Email,
                value.ContactNumber,
                value.PermanentAddress,
                value.ResidentTypeId,
                value.OccupancyStatusId))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.Residents.GetAllResidents)]
    [ProducesResponseType(typeof(ResidentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidents() =>
    await Result.Success(new GetAllResidentsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Residents.GetAllResidentNames)]
    [ProducesResponseType(typeof(ResidentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentNames() =>
    await Result.Success(new GetAllResidentNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region ResidentRequests     

    [HttpPost(ApiRoutes.ResidentRequests.CreateResidentRequest)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentRequest([FromBody] CreateResidentRequestRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateResidentRequestCommand(
                value.ApartmentId,
                value.BuildingId,
                value.FloorId,
                value.UnitId,
                value.FirstName,
                value.LastName,
                value.Email,
                value.ContactNumber,
                value.PermanentAddress,             
                value.ResidentTypeId,
                value.OccupancyStatusId))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.ResidentRequests.GetAllResidentRequests)]
    [ProducesResponseType(typeof(ResidentRequestListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentRequests() =>
    await Result.Success(new GetAllResidentRequestsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPut(ApiRoutes.ResidentRequests.UpdateRequestApproveStatus)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRequestApproveStatus([FromBody] UpdateRequestApproveStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateRequestApproveStatusCommand(
                value.Id))
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

    [HttpPut(ApiRoutes.ResidentRequests.UpdateRequestRejectStatus)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRequestRejectStatus([FromBody] UpdateRequestRejectStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateRequestRejectStatusCommand(
                value.Id))
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