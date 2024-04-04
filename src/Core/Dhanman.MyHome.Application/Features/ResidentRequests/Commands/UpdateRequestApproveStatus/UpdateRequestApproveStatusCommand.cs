using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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