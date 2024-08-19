using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Application.Features.Gates.Commands.CreateGate;
using Dhanman.MyHome.Application.Features.Gates.Commands.DeleteGate;
using Dhanman.MyHome.Application.Features.Gates.Commands.UpdateGate;
using Dhanman.MyHome.Application.Features.Gates.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class GatesController : ApiController
{
    public GatesController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Gates
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Read")]
    [HttpGet(ApiRoutes.Gates.GetAllGates)]
    [ProducesResponseType(typeof(GateListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGates(Guid apartmentId) =>
    await Result.Success(new GetAllGatesQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Read")]
    [HttpGet(ApiRoutes.Gates.GetGateNames)]
    [ProducesResponseType(typeof(GateNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGateNames(Guid apartmentId) =>
    await Result.Success(new GetAllGateNamesQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Read")]
    [HttpGet(ApiRoutes.Gates.GetGateById)]
    [ProducesResponseType(typeof(GateResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGateByGateId(int gateId) =>
    await Result.Success(new GetGateByGateIdQuery(gateId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Write")]
    [HttpPost(ApiRoutes.Gates.CreateGate)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateGate([FromBody] CreateGateRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateGateCommand(
             value.Name,
             value.ApartmentId,
             value.BuildingId,
             value.GateTypeId,
             value.IsUsedForIn,
             value.IsUsedForOut,
             value.IsAllUsersAllowed,
             value.IsResidentsAllowed,
             value.IsStaffAllowed,
             value.IsVendorAllowed
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Write")]
    [HttpPut(ApiRoutes.Gates.UpdateGate)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGates([FromBody] UpdateGateRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateGateCommand(
                value.Id,
                value.Name,
                value.ApartmentId,
                value.BuildingId,
                value.GateTypeId,
                value.IsUsedForIn,
                value.IsUsedForOut,
                value.IsAllUsersAllowed,
                value.IsResidentsAllowed,
                value.IsStaffAllowed,
                value.IsVendorAllowed
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

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Gate.Delete")]
    [HttpDelete(ApiRoutes.Gates.DeleteGateById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGateById(int id)
    {
        var result = await Result.Success(new DeleteGateCommand(id))
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
