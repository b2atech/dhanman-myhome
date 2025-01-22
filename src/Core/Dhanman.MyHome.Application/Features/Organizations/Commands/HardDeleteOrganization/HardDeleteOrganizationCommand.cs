using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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