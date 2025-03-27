using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
