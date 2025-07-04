using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.BuildingTypes;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;
using Dhanman.MyHome.Domain.Entities.Users;
using static Dhanman.MyHome.Application.Constants.CacheKeys;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class WaterTankerDeliveryByIdQueryHandler : IQueryHandler<WaterTankerDeliveryByIdQuery, Result<WaterTankerDeliveryResponse>>
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public WaterTankerDeliveryByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<WaterTankerDeliveryResponse>> Handle(WaterTankerDeliveryByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
             .Ensure(query => query != null, Errors.General.EntityNotFound)
             .Bind(async query =>
             {
                 var waterTankerDeliveryResponse = await (from waterTankerDelivery in _dbContext.SetInt<WaterTankerDelivery>().AsNoTracking()
                                               where waterTankerDelivery.Id == request.WaterTankerDeliveryId

                                               join vendorNameUser in _dbContext.Set<User>()
                                               on waterTankerDelivery.VendorId
                                               equals vendorNameUser.Id into vendorNameUserGroup
                                               from vendorNameUser in vendorNameUserGroup.DefaultIfEmpty()

                                               join createdByUser in _dbContext.Set<User>()
                                                   on waterTankerDelivery.CreatedBy
                                                   equals createdByUser.Id into createdByUserGroup
                                               from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                               join modifiedByUser in _dbContext.Set<User>()
                                                   on waterTankerDelivery.ModifiedBy
                                                   equals modifiedByUser.Id into modifiedByUserGroup
                                               from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user

                                               select new WaterTankerDeliveryResponse(
                                                 waterTankerDelivery.Id,
                                                  waterTankerDelivery.DeliveryDate,
                                                 waterTankerDelivery.DeliveryTime,
                                                 waterTankerDelivery.VendorId,
                                                 vendorNameUser != null ? $"{vendorNameUser.FirstName.Value} {vendorNameUser.LastName.Value}" : null,  // Get Vendor Name
                                                 waterTankerDelivery.TankerCapacityLiters,
                                                 waterTankerDelivery.ActualReceivedLiters,
                                                 waterTankerDelivery.CreatedBy,
                                                 $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                                 waterTankerDelivery.CreatedOnUtc,
                                                 waterTankerDelivery.ModifiedBy,
                                                 $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}",
                                                 waterTankerDelivery.ModifiedOnUtc
                                                 ))
                                        .FirstOrDefaultAsync(cancellationToken);

                 return waterTankerDeliveryResponse != null
                    ? Result.Success(waterTankerDeliveryResponse)
                    : Result.Failure<WaterTankerDeliveryResponse>(Errors.General.EntityNotFound);

             });
    }
    #endregion




}
