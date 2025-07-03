using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Users;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Users.Queries;

public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Result<UserListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllUsersQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<UserListResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var visitors = await _dbContext.Set<User>()
                  .AsNoTracking()
                  .Select(e => new UserResponse(
                          e.Id,
                          e.FirstName,
                          e.LastName,
                          e.ContactNumber,
                          e.IsOwner,
                          e.Email,
                          e.CreatedOnUtc,
                          e.ModifiedOnUtc,
                          e.CreatedBy,
                          e.ModifiedBy
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new UserListResponse(visitors);

                  return listResponse;
              });
    }
    #endregion

}
