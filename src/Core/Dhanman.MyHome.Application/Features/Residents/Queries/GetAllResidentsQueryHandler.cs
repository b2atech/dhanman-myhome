using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentTypes;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.UnitStatuses;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;
public class GetAllResidentsQueryHandler : IQueryHandler<GetAllResidentsQuery, Result<ResidentListResponse>>
{
#region Properties
private readonly IApplicationDbContext _dbContext;
#endregion

#region Constructors
public GetAllResidentsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ResidentListResponse>> Handle(GetAllResidentsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var residents = await _dbContext.SetInt<Resident>()
                    .AsNoTracking()
                    .Where(e => e.ApartmentId == request.ApartmentId)
                    .Select(e => new ResidentResponse(
                        e.Id,
                        _dbContext.SetInt<ResidentUnit>()
                            .Where(p => p.Id == e.Id)
                            .Select(p => p.UnitId)
                            .FirstOrDefault(),
                        _dbContext.SetInt<ResidentUnit>()
                            .Where(p => p.ResidentId == e.Id)
                            .Join(_dbContext.SetInt<Unit>(),
                                  p => p.UnitId,
                                  u => u.Id,
                                  (p, u) => u.Name)
                            .FirstOrDefault(),

                        e.FirstName,
                        e.LastName,
                        // Masked Email
                        string.IsNullOrEmpty(e.Email)
                            ? ""
                            : MaskEmail(e.Email),
                        // Masked Contact Number
                        string.IsNullOrEmpty(e.ContactNumber)
                            ? ""
                            : MaskContactNumber(e.ContactNumber),
                        e.ResidentTypeId,
                        _dbContext.SetInt<ResidentType>()
                            .Where(p => p.Id == e.ResidentTypeId)
                            .Select(p => p.Name)
                            .FirstOrDefault(),
                        e.OccupancyStatusId,
                        _dbContext.SetInt<UnitStatus>()
                            .Where(p => p.Id == e.OccupancyStatusId)
                            .Select(p => p.Name)
                            .FirstOrDefault(),

                        e.CreatedOnUtc,
                        e.ModifiedOnUtc,
                        e.CreatedBy,
                        e.ModifiedBy,
                        _dbContext.Set<User>()
                            .Where(p => p.Id == e.CreatedBy)
                            .Select(p => p.FirstName + " " + p.LastName)
                            .FirstOrDefault(),

                        _dbContext.Set<User>()
                            .Where(p => p.Id == e.ModifiedBy)
                            .Select(p => p.FirstName + " " + p.LastName)
                            .FirstOrDefault()
                    ))
                    .ToListAsync(cancellationToken);

                var listResponse = new ResidentListResponse(residents);
                return listResponse;
            });
    }

    // Helper Methods
    private static string MaskContactNumber(string number)
    {
        return number.Length >= 4
            ? new string('X', number.Length - 4) + number[^4..]
            : new string('X', number.Length);
    }

    private static string MaskEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        if (atIndex <= 1) return email;

        var namePart = email[..atIndex];
        var domainPart = email[atIndex..];

        var visibleLength = Math.Min(4, namePart.Length);
        return namePart[..visibleLength] + new string('X', namePart.Length - visibleLength) + domainPart;
    }    
    #endregion

}