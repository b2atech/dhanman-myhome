using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.DeleteEventOccurrence;

public sealed class DeleteEventOccurrenceCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int EventOccurrenceId { get; }

    #endregion

    #region Constructors
    public DeleteEventOccurrenceCommand(int eventOccurrenceId)
    {
        EventOccurrenceId = eventOccurrenceId;
    }
    #endregion
}
