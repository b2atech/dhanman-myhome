using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CreateServiceProvider;

public class CreateServiceProviderCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public Address PermanentAddress { get; set; }
    public Address PresentAddress { get; set; }
    public int ServiceProviderTypeId { get; set; }
    public int ServiceProviderSubTypeId { get; set; }
    public string? VehicleNumber { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime ValidityDate { get; set; }
    public bool PoliceverificationStatus { get; set; }
    public bool IsHireable { get; set; }
    public bool IsVisible { get; set; }
    public bool IsFrequentVisitor { get; set; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateServiceProviderCommand(string firstName, string? lastName, string? email, string visitingFrom, string contactNumber, Address permanentAddress, Address presentAddress, int serviceProviderTypeId, int serviceProviderSubTypeId, string? vehicleNumber, int identityTypeId, string identityNumber, DateTime validityDate, bool policeverificationStatus, bool isHireable, bool isVisible, bool isFrequentVisitor,  Guid createdBy)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        PermanentAddress = permanentAddress;
        PresentAddress = presentAddress;
        ServiceProviderTypeId = serviceProviderTypeId;
        ServiceProviderSubTypeId = serviceProviderSubTypeId;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;  
        ValidityDate = validityDate;
        PoliceverificationStatus = policeverificationStatus;
        IsHireable = isHireable;
        IsVisible = isVisible;
        IsFrequentVisitor = isFrequentVisitor;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.Now;
    }
    public CreateServiceProviderCommand() { }

    #endregion
}