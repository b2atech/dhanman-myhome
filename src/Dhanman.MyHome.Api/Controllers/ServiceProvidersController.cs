using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;
using Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;
using Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CreateServiceProvider;
using Dhanman.MyHome.Application.Features.ServiceProviders.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviderTypes.Queries;
using Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.CreateUnitServiceProvider;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ServiceProvidersController : ApiController
{
    public ServiceProvidersController(IMediator mediator) : base(mediator)
    {
    }


    #region ServiceProviders    

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
             value.CreatedBy))
         .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.ServiceProviders.GetAllServiceProviders)]
    [ProducesResponseType(typeof(ServiceProviderListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviders() =>
    await Result.Success(new GetAllServiceProvidersQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.ServiceProviders.GetAllServiceProviderNames)]
    [ProducesResponseType(typeof(ServiceProviderNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviderNames() =>
    await Result.Success(new GetAllServiceProviderNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion


    #region ServiceProviderSubType     

    [HttpGet(ApiRoutes.ServiceProviderSubType.GetAllServiceProvderSubType)]
    [ProducesResponseType(typeof(ServiceProivderSubTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProvderSubType() =>
    await Result.Success(new GetAllServiceProviderSubTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion

    #region ServiceProviders    

    [HttpPost(ApiRoutes.UnitServiceProviders.CreateUnitServiceProvider)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUnitServiceProvider([FromBody] CreateUnitServiceProviderRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
        .Map(value => new CreateUnitServiceProviderCommand(
            value.UnitId,
            value.ServiceProviderId,
            value.Start,
            value.End))
         .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);



    #endregion

    #region ServiceProviderType     

    [HttpGet(ApiRoutes.ServiceProviderType.GetAllServiceProvderType)]
    [ProducesResponseType(typeof(ServiceProivderTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProvderType() =>
    await Result.Success(new GetAllServiceProviderTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion
}