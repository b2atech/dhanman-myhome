using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CheckinServiceProvider;

public sealed class CheckinServiceProviderCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties    
    public Guid ApartmentId { get; set; }
    public string Pin { get; set; }
    #endregion

    #region Constructor
    public CheckinServiceProviderCommand(Guid apartmentId, string pin)
    {
        ApartmentId = apartmentId;
        Pin = pin;
    }

    #endregion
}
