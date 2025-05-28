using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands.DeleteWaterTankerDelivery;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class WaterTankerDeliveryController : ApiController
{
    public WaterTankerDeliveryController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {

    }

    #region WaterTankerDelivery
    [HttpPost(ApiRoutes.WaterTankerDeliveries.CreateWaterTankerDeliveries)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateWaterTankerDelivery([FromBody] List<CreateWaterTankerDeliveryDto> request) =>
      await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new CreateWaterTankerDeliveriesBulkCommand(value))
          .Bind(command => Mediator.Send(command))
          .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.WaterTankerDeliveries.UpdateWaterTankerDelivery)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWaterTankerDeliveries([FromBody] UpdateWaterTankerDeliveryRequest request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateWaterTankerDeliveryCommand(
                    value.Id,
                    value.CompanyId,
                    value.VendorId,
                    value.DeliveryDate,
                    value.DeliveryTime,
                    value.TankerCapacityLiters,
                    value.ActualReceivedLiters
                ))
            .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    //Delete
    [HttpDelete(ApiRoutes.WaterTankerDeliveries.DeleteWaterTankerDeliveryById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWaterTankerDelivery(int id)
    {
        var result = await Result.Success(new DeleteWaterTankerDeliveryCommand(id))
                    .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    #endregion

}
