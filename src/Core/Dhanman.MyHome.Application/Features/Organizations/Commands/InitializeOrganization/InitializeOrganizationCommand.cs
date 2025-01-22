using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Organizations.Commands.InitializeOrganization;
public class InitializeOrganizationCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyGuids { get; set; }
    public string CompanyNames { get; set; }
    public Guid UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructor
    public InitializeOrganizationCommand(Guid id, string name, string companyGuids, string companyNames, Guid userId, string userFirstName, string userLastName, string phoneNumber, string email, Guid createdBy)
    {
        Id = id;
        Name = name;
        CompanyGuids = companyGuids;
        CompanyNames = companyNames;
        UserId = userId;
        UserFirstName = userFirstName;
        UserLastName = userLastName;
        PhoneNumber = phoneNumber;
        Email = email;
        CreatedBy = createdBy;
    }
    #endregion
}

