namespace Dhanman.MyHome.Application.Contracts.Visitors
{
    public sealed class VisitorByContactResponse
    {
        public int Id { get; }
        public string FirstName { get; }
        public string? LastName { get; }
        public string? Email { get; }
        public string ContactNumber { get; }
        public int VisitorTypeId { get; }
        public string? VehicleNumber { get; }
        public int? IdentityTypeId { get; }
        public string? IdentityNumber { get; }

        public VisitorByContactResponse(int id, string firstName, string? lastName, string? email, string contactNumber, int visitorTypeId, string? vehicleNumber, int? identityTypeId, string? identityNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ContactNumber = contactNumber;
            VisitorTypeId = visitorTypeId;
            VehicleNumber = vehicleNumber;
            IdentityTypeId = identityTypeId;
            IdentityNumber = identityNumber;
        }

       
    }
}
