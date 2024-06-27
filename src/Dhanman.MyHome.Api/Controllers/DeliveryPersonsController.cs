using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.DeliveryPersons;
using Dhanman.MyHome.Application.Features.DeliveryPersons.Commands.CreateDeliveryPerson;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers
{
    public class DeliveryPersonsController : ApiController
    {
        #region Constructor
        public DeliveryPersonsController(IMediator mediator) : base(mediator)
        {
        }
        #endregion

        #region Events
        [HttpPost(ApiRoutes.DeliveryPersons.CreateDeliveryPerson)]
        [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDeliveryPerson([FromBody] CreateDeliveryPersonRequest? request) =>
               await Result.Create(request, Errors.General.BadRequest)
               .Map(value => new CreateDeliveryPersonCommand(
                         value.Name,
                         value.CompanyName,
                         value.MobileNumber ))
                .Bind(command => Mediator.Send(command))
                      .Match(Ok, BadRequest);

        #endregion
    }



}

