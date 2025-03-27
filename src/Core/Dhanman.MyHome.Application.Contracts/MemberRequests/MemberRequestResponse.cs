namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public sealed class MemberRequestResponse
{
    #region Properties 
    public int Id { get; }
    public Guid ApartmentId { get; }
    public string ApartmentType { get; }    
    public string FirstName { get; }
    public string LastName { get; }
    public string RequestedByName { get; }
    public string Email { get; }
    public string ContactNumber { get; }    
    public int RequestStatusId { get; }
    public string RequestStatus { get; }

    #endregion
    //string requestedByName,
    #region Constructor
    public MemberRequestResponse(int id, Guid apartmentId, string apartmentType, string firstName, string lastName,  string email, string contactNumber, int requestStatusId, string requestStatus)
    {
        Id = id;
        ApartmentId = apartmentId;
        ApartmentType = apartmentType;
        FirstName = firstName;
        LastName = lastName;
        //RequestedByName = $"{firstName} {lastName}"; 
        Email = email;
        ContactNumber = contactNumber;
        RequestStatusId = requestStatusId;
        RequestStatus = requestStatus;
    }
    #endregion
}