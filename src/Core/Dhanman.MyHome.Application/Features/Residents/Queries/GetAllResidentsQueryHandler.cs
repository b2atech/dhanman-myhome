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
                        string.IsNullOrEmpty(e.Email) ? "" : MaskEmail(e.Email),
                        string.IsNullOrEmpty(e.ContactNumber) ? "" : MaskContactNumber(e.ContactNumber),
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

                return new ResidentListResponse(residents);
            });
    }

    // Random mask 5 digits of contact number
    private static string MaskContactNumber(string number)
    {
        if (number.Length < 5)
            return new string('X', number.Length);

        var chars = number.ToCharArray();
        var random = new Random();

        var indicesToMask = new HashSet<int>();
        while (indicesToMask.Count < 5)
        {
            int index = random.Next(0, number.Length);
            indicesToMask.Add(index);
        }

        foreach (var i in indicesToMask)
        {
            chars[i] = 'X';
        }

        return new string(chars);
    }

    // Random mask email username (before @)
    private static string MaskEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        if (atIndex <= 0) return email;

        var namePart = email[..atIndex];
        var domainPart = email[atIndex..];

        var random = new Random();
        var chars = namePart.ToCharArray();

        int toMask = Math.Max(1, namePart.Length <= 4 ? namePart.Length - 1 : namePart.Length - 4);
        var indicesToMask = new HashSet<int>();
        while (indicesToMask.Count < toMask)
        {
            int index = random.Next(0, namePart.Length);
            indicesToMask.Add(index);
        }

        foreach (var i in indicesToMask)
        {
            chars[i] = 'X';
        }

        return new string(chars) + domainPart;
    }
    #endregion

}