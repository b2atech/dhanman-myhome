using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Companies.Commands;

public sealed class CreateCompanyCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties    
    public Guid CompanyId { get; }
    public Guid OrganizationId { get; }
    public string Name { get; }
    public bool IsApartment { get; }
    #endregion

    #region Constructors
    public CreateCompanyCommand(Guid companyId, Guid organizationId, string name, bool isApartment)
    {
        CompanyId = companyId;
        OrganizationId = organizationId;
        Name = name;
        IsApartment = isApartment;
    }
    #endregion
}

