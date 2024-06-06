using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
