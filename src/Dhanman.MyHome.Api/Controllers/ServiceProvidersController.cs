using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.IdendityTypes;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;
using Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;
using Dhanman.MyHome.Application.Features.IdendityTypes.Queries;
using Dhanman.MyHome.Application.Features.OccupantTypes.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CreateServiceProvider;
using Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CheckinServiceProvider;
using Dhanman.MyHome.Application.Features.ServiceProviders.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviderTypes.Queries;
using Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;
using Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.CreateUnitServiceProvider;
using Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.GetAssignUnits;
using Dhanman.MyHome.Application.Features.UnitServiceProviders.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ServiceProvidersController : ApiController
{
    public ServiceProvidersController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }


    #region ServiceProviders    
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ServiceProvider.Write")]
    [HttpPost(ApiRoutes.ServiceProviders.CreateServiceProvider)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateServiceProviderRequest([FromBody] CreateServiceProviderRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new CreateServiceProviderCommand(
             value.FirstName,
             value.LastName,
             value.Email,
             value.VisitingFrom,
             value.ContactNumber,
             value.PermanentAddress,
             value.PresentAddress,
             value.ServiceProviderTypeId,
             value.ServiceProviderSubTypeId,
             value.VehicleNumber,
             value.IdentityTypeId,
             value.IdentityNumber,
             value.ValidityDate,
             value.PoliceVerificationStatus,
             value.IsHireable,
             value.IsVisible,
             value.IsFrequentVisitor,
             value.ApartmentId,
             value.Pin))
         .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.ServiceProviders.GetAllServiceProviders)]
    [ProducesResponseType(typeof(ServiceProviderListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviders() =>
    await Result.Success(new GetAllServiceProvidersQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.ServiceProviders.GetAllServiceProviderNames)]
    [ProducesResponseType(typeof(ServiceProviderNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviderNames() =>
    await Result.Success(new GetAllServiceProviderNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.ServiceProviders.CheckinServiceProvider)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckinServiceProvider(Guid apartmentId, [FromBody] CheckinServiceProviderRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CheckinServiceProviderCommand(apartmentId, value.Pin))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    #endregion

    #region ServiceProviderSubType     

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.ServiceProviderSubType.GetAllServiceProvderSubType)]
    [ProducesResponseType(typeof(ServiceProivderSubTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProvderSubType() =>
    await Result.Success(new GetAllServiceProviderSubTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion

    #region ServiceProviders    

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ServiceProvider.Write")]
    [HttpPost(ApiRoutes.UnitServiceProviders.CreateUnitServiceProvider)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUnitServiceProvider([FromBody] CreateUnitServiceProviderRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new CreateUnitServiceProviderCommand(
            value.UnitIds,
            value.ServiceProviderId,
            value.Start,
            value.End))
         .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);



    #endregion

    #region ServiceProviderType     

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.ServiceProviderType.GetAllServiceProvderType)]
    [ProducesResponseType(typeof(ServiceProivderTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProvderType() =>
    await Result.Success(new GetAllServiceProviderTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion

    #region AssignServiceProviderUnits

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpPost(ApiRoutes.ServiceProviderAssignedUnits.GetAllAssignUnits)]
    [ProducesResponseType(typeof(AssignSPUnitsListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAssignUnits([FromBody] GetAssignUnitsResquest? request) =>
await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new GetAssignUnitsCommand(
                value.BuildingIds
              ))
            .Bind(command => Mediator.Send(command))
           .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.ServiceProviderAssignedUnits.GetAllServiceProviderAssignedUnitsById)]
    [ProducesResponseType(typeof(AssignUnitListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviderAssignedUnits(int id) =>
    await Result.Success(new GetAllServiceProviderUnitsByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion

    #region IdentityTypes

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.IdentityTypes.GetAllIdentityTypes)]
    [ProducesResponseType(typeof(IdentityTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllIdendityTypes() =>
     await Result.Success(new GetAllIdendityTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion


}