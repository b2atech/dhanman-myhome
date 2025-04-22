using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.UpdateEvent;
public class UpdateEventCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int CommunityCalenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public string RecurrenceRule { get; set; }
    public int RecurrenceRuleId { get; set; }
    public DateTime RecurrenceEndDate { get; set; }

    #endregion

    #region Constructors
    public UpdateEventCommand(Guid id, Guid companyId, int communityCalenderId, string title, string description, DateTime startTime, DateTime endTime, bool isRecurring, string recurrenceRule, int recurrenceRuleId, DateTime recurrenceEndDate)
    {
        Id = id;
        CompanyId = companyId;
        CommunityCalenderId = communityCalenderId;
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        IsRecurring = isRecurring;
        RecurrenceRule = recurrenceRule;
        RecurrenceRuleId = recurrenceRuleId;
        RecurrenceEndDate = recurrenceEndDate;
    }
    #endregion
}
