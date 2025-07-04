using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;

namespace Dhanman.MyHome.Application.Features.OccupantTypes.Queries
{
    public class GetAllOccupantTypesQuery : ICacheableQuery<Result<OccupantTypeListResponse>>
    {
        #region Properties     
        #endregion

        #region Constructors
        public GetAllOccupantTypesQuery() { }
        #endregion

        #region Methodes
        public string GetCacheKey() => string.Format(CacheKeys.OccupantTypes.OccupantTypeList, "user", "");
        #endregion
    }
}
