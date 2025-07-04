using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.DeleteVisitor;

public class DeleteVisitorCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int VisitorId { get; }

    #endregion

    #region Constructors
    public DeleteVisitorCommand(int visitorId)
    {
        VisitorId = visitorId;
    }
    #endregion
}
