using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public Guid? PermanentAddressId { get; set; }    
    public int ResidentTypeId { get; set; }     
    public int OccupancyStatusId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateResidentCommand(int unitId, string firstName, string lastName, string email, string contactNumber, Guid? permanentAddressId, int residentTypeId, int occupancyStatusId, Guid createdBy)
    {
        UnitId = unitId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;
        CreatedBy = createdBy;
    }
    
    public CreateResidentCommand() { }

    #endregion
}