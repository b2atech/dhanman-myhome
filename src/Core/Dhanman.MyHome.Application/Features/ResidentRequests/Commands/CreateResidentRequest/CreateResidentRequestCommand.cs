﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;

public class CreateResidentRequestCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int Id { get; set; }
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int FloorId { get; set; }
    public int UnitId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public Address? PermanentAddress { get; set; }
    public int ResidentTypeId { get; set; }    
    public int OccupancyStatusId { get; set; }
    #endregion

    #region Constructors
    public CreateResidentRequestCommand(Guid apartmentId, int buildingId, int floorId, int unitId, string firstName, string lastName, string email, string contactNumber, Address? permanentAddress,  int residentTypeId, int occupancyStatusId)
    {        
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        FloorId = floorId;
        UnitId = unitId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddress = permanentAddress;    
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;     
       
    }


    public CreateResidentRequestCommand() { }

    #endregion
}