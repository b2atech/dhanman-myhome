using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;

public sealed class UpdateRequestApproveStatusCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int Id { get; set; }       

    #endregion

    #region Constructors  
    public UpdateRequestApproveStatusCommand(int id)
    {
        Id = id;       
    }
    #endregion
}