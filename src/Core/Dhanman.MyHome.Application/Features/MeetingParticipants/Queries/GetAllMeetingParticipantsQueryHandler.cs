using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Users;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.MeetingParticipants;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Queries;

public class GetAllMeetingParticipantsQueryHandler : IQueryHandler<GetAllMeetingParticipantsQuery, Result<UserNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;

    #endregion

    #region Constructor
    public GetAllMeetingParticipantsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods
    public async Task<Result<UserNameListResponse>> Handle(GetAllMeetingParticipantsQuery request, CancellationToken cancellationToken)
    {

        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var participants = await _dbContext.SetInt<MeetingParticipant>()
                  .AsNoTracking()
                  .Where(e => e.OccurrenceId == request.OccurrenceId)
                  .Select(e => new UserNameResponse(
                          e.UserId,
                          _dbContext.Set<User>()
                              .Where(p => p.Id == e.UserId)
                              .Select(p => p.FirstName).FirstOrDefault(),
                          _dbContext.Set<User>()
                              .Where(p => p.Id == e.UserId)
                              .Select(p => p.LastName).FirstOrDefault()))
                  .ToListAsync(cancellationToken);

                  var listResponse = new UserNameListResponse(participants);

                  return listResponse;
              });        
    }

    #endregion
}