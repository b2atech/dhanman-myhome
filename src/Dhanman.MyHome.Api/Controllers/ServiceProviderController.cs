using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;
using Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ServiceProviderController : ApiController
{
    public ServiceProviderController(IMediator mediator) : base(mediator)
    {
    }


    #region ServiceProvocerSubType     

    [HttpGet(ApiRoutes.ServiceProviderSubType.GetAllServiceProvderSubType)]
    [ProducesResponseType(typeof(ServiceProivderSubTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnits() =>
    await Result.Success(new GetAllServiceProviderSubTypeQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

  
    #endregion


}