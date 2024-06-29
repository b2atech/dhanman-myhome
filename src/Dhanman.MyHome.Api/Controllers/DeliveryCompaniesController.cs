using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.DeliveryCompanies;
using Dhanman.MyHome.Application.Features.DeliveryCompanies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class DeliveryCompaniesController : ApiController
{
    public DeliveryCompaniesController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region DeliveryCompanies   

    [HttpGet(ApiRoutes.DeliveryCompanies.GetAllDeliveryCompanies)]
    [ProducesResponseType(typeof(DeliveryCompanyListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllDeliveryCompanies() =>
    await Result.Success(new GetAllDeliveryCompaniesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);       

    #endregion

}