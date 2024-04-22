namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorResponse
{
    #region Properties

    public int Id { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string Email { get; }
    public string VisitingFrom { get; }
    public string ContactNumber { get; }
    public int VisitorTypeId { get; }
    public string VisitorType { get; }
    public string? VehicleNumber { get; }
    public int IdentityTypeId { get; }
    public string IdentityNumber { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }

    #endregion

    #region Constructor
    public VisitorResponse(int id, string firstName, string? lastName, string email, string visitingFrom, string contactNumber, int visitorTypeId, string visitorType, string? vehicleNumber, int identityTypeId, string identityNumber, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        VisitorType = visitorType;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }
    #endregion
}