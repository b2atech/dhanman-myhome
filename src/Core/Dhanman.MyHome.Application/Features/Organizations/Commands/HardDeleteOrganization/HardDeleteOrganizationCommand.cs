using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Organizations.Commands.HardDeleteOrganization;

public class HardDeleteOrganizationCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public Guid OrganizationId { get; }
    #endregion

    #region Constructors
    public HardDeleteOrganizationCommand(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
    #endregion
}