using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.CreateUnitServiceProvider;

public class CreateUnitServiceProviderCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int Id { get; set; }
    public int UnitId { get; set; }
    public int ServiceProviderId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    #endregion

    #region Constructors
    public CreateUnitServiceProviderCommand(int unitId, int serviceProviderId, DateTime start, DateTime end)
    {
        UnitId = unitId;
        ServiceProviderId = serviceProviderId;
        Start = start;
        End = end;
    }

    public CreateUnitServiceProviderCommand() { }

    #endregion
}
