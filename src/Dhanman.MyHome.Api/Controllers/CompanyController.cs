using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Companies;
using Dhanman.MyHome.Application.Features.Companies.Commands;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class CompanyController : ApiController
{
    public CompanyController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    [HttpPost(ApiRoutes.Companies.CreateCompany)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
               .Map(value => new CreateCompanyCommand(
                   value.CompanyId,
                   value.OrgnizationId,
                   value.Name,
                   value.IsApartment
                   ))
               .Bind(command => Mediator.Send(command))
              .Match(Ok, BadRequest);

}