using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.ResidentTokens.Commands.SaveTokens
{
    public class SaveResidentTokenCommand:ICommand<Result<EntityCreatedResponse>>
    {
        #region Properties
        public int ResidentId { get; set; }
        public string FCMToken { get; set; }

        #endregion

        #region Constructors
        public SaveResidentTokenCommand(int residentId, string fCMToken)
        {
            ResidentId = residentId;
            FCMToken = fCMToken;
        }
        #endregion
    }
}
