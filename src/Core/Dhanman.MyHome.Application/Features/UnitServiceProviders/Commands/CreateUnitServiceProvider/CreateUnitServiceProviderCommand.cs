using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.CreateUnitServiceProvider;

public class CreateUnitServiceProviderCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int Id { get; set; }
    public List<int> UnitIds { get; set; }
    public int ServiceProviderId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    #endregion

    #region Constructors
    public CreateUnitServiceProviderCommand(List<int> unitIds, int serviceProviderId, DateTime start, DateTime end)
    {
        UnitIds = unitIds;
        ServiceProviderId = serviceProviderId;
        Start = start;
        End = end;
    }

    public CreateUnitServiceProviderCommand() { }

    #endregion
}
