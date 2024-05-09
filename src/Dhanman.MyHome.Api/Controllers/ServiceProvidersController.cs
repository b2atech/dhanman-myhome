using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;
using Dhanman.MyHome.Application.Features.ServiceProviders.Queries;
using Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ServiceProvidersController : ApiController
{
    public ServiceProvidersController(IMediator mediator) : base(mediator)
    {
    }


    #region ServiceProviders     

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
    public async Task<IActionResult> GetAllUnits() =>
    await Result.Success(new GetAllServiceProviderSubTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion
}