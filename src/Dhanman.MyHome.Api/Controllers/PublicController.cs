using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using Dhanman.MyHome.Application.Features.MemberRequests.Commands.CreateMemberRequest;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class PublicController : PublicApiController
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PublicController(HttpClient httpClient, IConfiguration configuration, IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    #region ApartmentsNames

    [HttpGet(ApiRoutes.PublicApartments.GetApartmentNames)]
    [ProducesResponseType(typeof(ApartmentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartmentNames() =>
   await Result.Success(new GetAllApartmentNamesQuery())
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    #endregion

    #region MemberRequests     

    [HttpPost(ApiRoutes.ResidentRequests.CreateMemberRequest)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMemberRequestRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateMemberRequestCommand(
                 value.ApartmentId,
                 value.FirstName,
                 value.LastName,
                 value.Email,
                 value.MobileNumber,
                 value.MemberAdditionalDetails,
                 value.CurrentAddress
                 ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

    #endregion
}
