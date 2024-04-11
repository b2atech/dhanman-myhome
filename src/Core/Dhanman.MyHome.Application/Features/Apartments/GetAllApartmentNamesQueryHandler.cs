using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;
using static Dhanman.MyHome.Domain.Errors;
using System.Numerics;
using System;
using System.Linq;

namespace Dhanman.MyHome.Application.Features.Apartments.Queries;

public class GetAllApartmentNamesQueryHandler : IQueryHandler<GetAllApartmentNamesQuery, Result<ApartmentNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllApartmentNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ApartmentNameListResponse>> Handle(GetAllApartmentNamesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var apartments = await _dbContext.Set<Apartment>()
                  .AsNoTracking()
                  .Select(e => new ApartmentNameResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ApartmentNameListResponse(apartments);

                  return listResponse;
              });
    }
    #endregion

}