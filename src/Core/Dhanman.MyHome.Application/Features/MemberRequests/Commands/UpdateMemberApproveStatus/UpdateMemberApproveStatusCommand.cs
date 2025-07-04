using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.UpdateMemberApproveStatus;

public sealed class UpdateMemberApproveStatusCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int Id { get; set; }

    #endregion

    #region Constructors  
    public UpdateMemberApproveStatusCommand(int id)
    {
        Id = id;
    }
    #endregion
}