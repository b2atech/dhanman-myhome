using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
namespace Dhanman.MyHome.Application.Features.DeliveryPersons.Commands.CreateDeliveryPerson;

public class CreateDeliveryPersonCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string MobileNumber { get; set; }

    #endregion

    #region Constructors
    public CreateDeliveryPersonCommand( string name, string companyName, string mobileNumber)
    {
        Name = name;
        CompanyName = companyName;
        MobileNumber = mobileNumber;
    }

    #endregion
}

