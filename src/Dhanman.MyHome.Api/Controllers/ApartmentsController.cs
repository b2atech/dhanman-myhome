using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ApartmentsController : ApiController
{
    public ApartmentsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Apartments     
    //[Authorize(Policy = "coapolicy")]
    [HttpGet(ApiRoutes.Apartments.GetApartments)]
    [ProducesResponseType(typeof(ApartmentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartments() =>
    await Result.Success(new GetAllApartmentsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    //[HttpGet(ApiRoutes.Apartments.GetApartmentNames)]
    //[ProducesResponseType(typeof(ApartmentNameListResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetAllApartmentNames() =>
    //await Result.Success(new GetAllApartmentNamesQuery())
    //.Bind(query => Mediator.Send(query))
    //.Match(Ok, NotFound);

    #endregion

    
}