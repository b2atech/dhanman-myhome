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
    public ApartmentsController(IMediator mediator) : base(mediator)
    {
    }

    #region Apartments     

    [HttpGet(ApiRoutes.Apartments.GetApartments)]
    [ProducesResponseType(typeof(ApartmentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartments() =>
    await Result.Success(new GetAllApartmentsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Apartments.GetApartmentNames)]
    [ProducesResponseType(typeof(ApartmentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartmentNames() =>
    await Result.Success(new GetAllApartmentNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
    
}