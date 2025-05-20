using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.EventOccurrences;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Query;

public sealed class GetMeetingAgendaItemByIdQueryHandler : IQueryHandler<GetMeetingAgendaItemByIdQuery, Result<MeetingAgendaItemResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetMeetingAgendaItemByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<MeetingAgendaItemResponse>> Handle(GetMeetingAgendaItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var response = await (from mai in _dbContext.SetInt<MeetingAgendaItem>().AsNoTracking()
                                        where mai.Id == request.MeetingAgendaItemId
                                        join eo in _dbContext.SetInt<EventOccurrence>().AsNoTracking() on mai.OccurrenceId equals eo.Id
                                        join createdByUser in _dbContext.Set<User>() on mai.CreatedBy equals createdByUser.Id into createdGroup
                                        from createdByUser in createdGroup.DefaultIfEmpty()
                                        join modifiedByUser in _dbContext.Set<User>() on mai.ModifiedBy equals modifiedByUser.Id into modifiedGroup
                                        from modifiedByUser in modifiedGroup.DefaultIfEmpty()
                                        select new MeetingAgendaItemResponse(
                                            mai.Id,
                                            mai.OccurrenceId,
                                            mai.ItemText,
                                            mai.OrderNo,
                                            mai.CreatedOnUtc,
                                            mai.IsDeleted,
                                            mai.DeletedOnUtc,
                                            mai.CreatedBy,
                                            mai.ModifiedOnUtc,
                                            mai.ModifiedBy,
                                            $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                            modifiedByUser != null ? $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}" : null
                                        )).FirstOrDefaultAsync(cancellationToken);

                  return response != null
                     ? Result.Success(response)
                     : Result.Failure<MeetingAgendaItemResponse>(Errors.General.EntityNotFound);

              });
    }
    #endregion

}
