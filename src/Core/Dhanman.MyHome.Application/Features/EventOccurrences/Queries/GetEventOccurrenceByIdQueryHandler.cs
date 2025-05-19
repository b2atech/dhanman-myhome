using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.EventOccurrences;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.EventOccurrences;
using Dhanman.MyHome.Domain.Entities.EventOccurrenceStatuses;
using Dhanman.MyHome.Domain.Entities.Events;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Queries;

public sealed class GetEventOccurrenceByIdQueryHandler : IQueryHandler<GetEventOccurrenceByIdQuery, Result<EventOccurrenceResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetEventOccurrenceByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<EventOccurrenceResponse>> Handle(GetEventOccurrenceByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var eventOccurrenceResponse = await (from eo in _dbContext.SetInt<EventOccurrence>().AsNoTracking()
                                                where eo.Id == request.EventOccurrenceId
                                                join et in _dbContext.Set<Event>().AsNoTracking()
                                                   on eo.EventId equals et.Id
                                                join eos in _dbContext.SetInt<EventOccurrenceStatus>().AsNoTracking()
                                                on eo.EventOccurrenceStatusId equals eos.Id
                                                join createdByUser in _dbContext.Set<User>()
                                                    on eo.CreatedBy
                                                    equals createdByUser.Id into createdByUserGroup
                                                from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                                join modifiedByUser in _dbContext.Set<User>()
                                                    on eo.ModifiedBy
                                                    equals modifiedByUser.Id into modifiedByUserGroup
                                                from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user

                                                select new EventOccurrenceResponse(
                                                  eo.Id,
                                                  eo.EventId,
                                                  et.Title,
                                                  eo.OccurrenceDate,
                                                  eo.StartTime,
                                                  eo.EndTime,
                                                  eo.GeneratedFromRecurrence,
                                                  eo.EventOccurrenceStatusId,
                                                  eos.Name,
                                                  eo.RecordingUrl,
                                                  eo.Notes,
                                                  eo.CreatedOnUtc,
                                                  eo.ModifiedOnUtc,
                                                  eo.CreatedBy,
                                                  eo.ModifiedBy,
                                                  $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                                  modifiedByUser != null ? $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}" : null
                                              )).FirstOrDefaultAsync(cancellationToken);

                  return eventOccurrenceResponse != null
                     ? Result.Success(eventOccurrenceResponse)
                     : Result.Failure<EventOccurrenceResponse>(Errors.General.EntityNotFound);

              });
    }
    #endregion

}