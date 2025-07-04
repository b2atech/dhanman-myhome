using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.BookingFacilites;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.FacilityBookings;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.BookingFacilities.Queries;

internal class GetAllBookingFacilitesQueryHandler : IQueryHandler<GetAllBookingFacilitesQuery, Result<BookingFacilitesListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllBookingFacilitesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BookingFacilitesListResponse>> Handle(GetAllBookingFacilitesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var residents = await _dbContext.SetInt<FacilityBooking>()
                  .AsNoTracking()
                  .Select(e => new BookingFacilitesResponse(
                          e.Id,
                          e.Name,
                          e.BuildingId
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new BookingFacilitesListResponse(residents);

                  return listResponse;
              });
    }
    #endregion
}
