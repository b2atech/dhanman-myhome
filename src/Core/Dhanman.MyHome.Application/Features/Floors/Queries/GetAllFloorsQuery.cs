﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Floors;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorsQuery : ICacheableQuery<Result<FloorListResponse>>
{
    #region Properties
    public int BuildingId { get; set; }
    #endregion

    #region Constructors
    public GetAllFloorsQuery(int buildingId)
    {
        BuildingId = buildingId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Floors.FloorList, "user", BuildingId);
    #endregion 

}