using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using Dhanman.MyHome.Domain.Entities.ResidentStatuses;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Queries;

public class GetAllMemberRequestsQueryHandler : IQueryHandler<GetAllMemberRequestsQuery, Result<MemberRequestListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllMemberRequestsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<MemberRequestListResponse>> Handle(GetAllMemberRequestsQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
            return Result.Failure<MemberRequestListResponse>(Errors.General.EntityNotFound);

        var memberRequests = await (from e in _dbContext.SetInt<ResidentRequest>().AsNoTracking()
                                    join a in _dbContext.Set<Apartment>().AsNoTracking() on e.ApartmentId equals a.Id into apt
                                    from a in apt.DefaultIfEmpty()
                                    join m in _dbContext.Set<MemberAdditionalDetail>().AsNoTracking() on e.MemberAdditionalDetailsId equals m.Id into mad
                                    from m in mad.DefaultIfEmpty()
                                    join r in _dbContext.SetInt<ResidentRequestStatus>().AsNoTracking() on e.RequestStatusId equals r.Id into rs
                                    from r in rs.DefaultIfEmpty()
                                    join city in _dbContext.Set<City>().AsNoTracking() on m.HattyId equals city.Id into cityGroup
                                    from city in cityGroup.DefaultIfEmpty()
                                    join spouseCity in _dbContext.Set<City>().AsNoTracking() on m.SpouseHattyId equals spouseCity.Id into spouseCityGroup
                                    from spouseCity in spouseCityGroup.DefaultIfEmpty()
                                    where e.ApartmentId == request.ApartmentId
                                    select new MemberRequestResponse(
                                        e.Id,
                                        e.ApartmentId,
                                        a.Name,
                                        e.FirstName,
                                        e.LastName,
                                        e.Email,
                                        e.ContactNumber,
                                        e.MemberAdditionalDetailsId,
                                        m.MemberType,
                                        m.UserName,
                                        m.Password,
                                        m.HattyId,
                                        city.Name,
                                        m.CompanyName,
                                        m.Designation,
                                        m.DateOfBirth,
                                        m.Gender,
                                        m.MaritalStatus,
                                        m.AboutYourself,
                                        m.SpouseName,
                                        m.SpouseHattyId,
                                        spouseCity.Name,
                                        e.RequestStatusId,
                                        r.Name
                                    )).ToListAsync(cancellationToken);

        return Result.Success(new MemberRequestListResponse(memberRequests));
    }    
    #endregion

}