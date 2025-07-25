using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands.DeleteWaterTankerDelivery;
using Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class WaterTankerDeliveryController : ApiController
{
    public WaterTankerDeliveryController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {

    }

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.WaterTankerDelivery.Write")]
    #region WaterTankerDelivery
    [HttpPost(ApiRoutes.WaterTankerDeliveries.CreateWaterTankerDeliveries)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateWaterTankerDelivery([FromBody] List<CreateWaterTankerDeliveryDto> request) =>
      await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new CreateWaterTankerDeliveriesBulkCommand(value))
          .Bind(command => Mediator.Send(command))
          .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.WaterTankerDeliveries.WaterTankerDeliveryById)]
    [ProducesResponseType(typeof(WaterTankerDeliveryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBuildingById(int id) =>
    await Result.Success(new WaterTankerDeliveryByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.WaterTankerDelivery.Write")]
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

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.WaterTankerDelivery.Delete")]
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
    //Fetch tanker count and its actual total liter's

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.WaterTankerDeliveries.GetWaterTankerDeliverySummery)]
    [ProducesResponseType(typeof(WaterTankerSummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitors(Guid companyId, DateTime startDate, DateTime endDate) =>
   await Result.Success(new GetWaterTankerSummaryQuery(companyId, startDate, endDate))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    //Fetch tanker delivery data by vendor and dates

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.WaterTankerDeliveries.GetAWaterTankerDeliveriesByVendorId)]
    [ProducesResponseType(typeof(WaterTankerSummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAWaterTankerDeliveriesByVendorId(Guid companyId, Guid vendorId, DateTime startDate, DateTime endDate ) =>
   await Result.Success(new GetVendorWaterTankerDeliveriesQuery(companyId, vendorId, startDate, endDate))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    #endregion

}
