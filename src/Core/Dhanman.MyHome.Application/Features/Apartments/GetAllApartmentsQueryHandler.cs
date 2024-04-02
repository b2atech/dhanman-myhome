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

public class GetAllApartmentsQueryHandler : IQueryHandler<GetAllApartmentsQuery, Result<ApartmentListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllApartmentsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ApartmentListResponse>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var apartments = await _dbContext.Set<Apartment>()
                  .AsNoTracking()
                  .Select(e => new ApartmentResponse(
                          e.Id,
                          e.Name,
                          e.ApartmentTypeId,
                          _dbContext.SetInt<ApartmentType>()
                          .Where(apartementType => apartementType.Id == e.ApartmentTypeId)
                          .Select(apartmentType => apartmentType.Name)
                          .FirstOrDefault(),
                          e.AddressId,
                          e.Phone,
                          e.PAN,
                          e.TAN,
                          e.AssociationName,
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc  ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ApartmentListResponse(apartments);

                  return listResponse;
              });
    }
    #endregion

}