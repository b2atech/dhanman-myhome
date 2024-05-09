using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.BookingFacilites;

namespace Dhanman.MyHome.Application.Features.BookingFacilities.Queries;

public class GetAllBookingFacilitesQuery : ICacheableQuery<Result<BookingFacilitesListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllBookingFacilitesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.BookingFacilities.BookingFacilitiesList, "user", "");
    #endregion
}
