﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Buildings;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;
 
public class GetAllBuildingsQuery : ICacheableQuery<Result<BuildingListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllBuildingsQuery()
    {
         
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingList, "user");
    #endregion 

}