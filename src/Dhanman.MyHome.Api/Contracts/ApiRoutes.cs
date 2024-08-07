﻿namespace Dhanman.MyHome.Api.Contracts;

public static class ApiRoutes
{
    public const string apiVersion = "api/v{version:apiVersion}/";

    public static class Authentication
    {
        public const string Login = "authentication/login";

        public const string Register = "authentication/register";
    }

    public static class Buildings
    {
        public const string CreateBuilding = apiVersion + "building";

        public const string GetAllBuildings = apiVersion + "buildings/{apartmentId:guid}";

        public const string GetAllBuildingNames = apiVersion + "buildingNames/{apartmentId:guid}";

        public const string GetBuildingById = apiVersion + "building/{id:int}";

        public const string UpdateBuilding = apiVersion + "building";

        public const string DeleteBuildingById = apiVersion + "building/{id:int}";

    }

    public static class Units
    {
        public const string CreateUnit = apiVersion + "unit";

        public const string CreateUnits = apiVersion + "units";

        public const string GetAllUnits = apiVersion + "units/{apartmentId:guid}";

        public const string GetAllUnitNames = apiVersion + "unitNames/{apartmentId:guid}/{buildingId:int}/{floorId:int}";

        public const string GetUnitById = apiVersion + "unit/{id:int}";

        public const string UpdateUnit = apiVersion + "unit";

        public const string GetAllUnitDetails = apiVersion + "unitDetails";

        public const string DeleteUnit = apiVersion + "unit/{id:int}";
        public const string GetUnitByFloorId = apiVersion + "unitsByFloorId";
    }

    public static class Residents
    {
        public const string CreateResident = apiVersion + "resident";

        public const string GetAllResidents = apiVersion + "residents";

        public const string GetAllResidentNames = apiVersion + "residentNames";

        public const string GetResidentById = apiVersion + "resident/{id:int}";

        public const string UpdateResidents = apiVersion + "updateResidents";
    }

    public static class ResidentRequests
    {
        public const string CreateResidentRequest = apiVersion + "residentRequest";

        public const string GetAllResidentRequests = apiVersion + "residentRequests";

        public const string GetAllResidentRequestNames = apiVersion + "residentRequestNames";

        public const string GetResidentRequestById = apiVersion + "ResidentRequest/{id:int}";

        public const string UpdateRequestApproveStatus = apiVersion + "requestApproveStatus";

        public const string UpdateRequestRejectStatus = apiVersion + "requestRejectStatus";
    }

    public static class Vehicles
    {
        public const string CreateVehicle = apiVersion + "vehicles";

        public const string GetAllVehicles = apiVersion + "vehicles";

        public const string GetAllVehicleNames = apiVersion + "vehicleNames";

        public const string GetVehicleById = apiVersion + "vehicle/{id:int}";

        public const string UpdateVehicles = apiVersion + "updateVehicles";
    }

    public static class Apartments
    {
        public const string CreateApartments = apiVersion + "apartment";

        public const string GetApartments = apiVersion + "apartments";

        public const string GetApartmentNames = apiVersion + "apartmentNames";

        public const string GetApartmentById = apiVersion + "apartments/{apartmentId:guid}";

        public const string UpdateApartments = apiVersion + "updateApartments";
    }

    public static class Floors
    {
        public const string CreateFloor = apiVersion + "floor";

        public const string GetFloors = apiVersion + "floors/{apartmentId:guid}";

        public const string GetFloorNames = apiVersion + "floorNames/{apartmentId:guid}/{buildingId:int}";

        public const string GetFloorById = apiVersion + "floor/{id:int}";

        public const string UpdateFloor = apiVersion + "floor";

        public const string DeleteFloorById = apiVersion + "floor/{id:int}";
    }

    public static class Gates
    {
        public const string CreateGate = apiVersion + "gate";

        public const string GetAllGates = apiVersion + "gates/{apartmentId:guid}";

        public const string GetGateNames = apiVersion + "gateNames/{apartmentId:guid}";

        public const string GetGateById = apiVersion + "gate/{gateId:int}";

        public const string UpdateGate = apiVersion + "gate";

        public const string DeleteGateById = apiVersion + "gate/{id:int}";
    }

    public static class OccupancyTypes
    {
        public const string GetAllOccupancyTypes = apiVersion + "occupancyTypes";
    }

    public static class Visitors
    {
        public const string CreateVisitor = apiVersion + "visitor";

        public const string GetAllVisitors = apiVersion + "visitors";

        public const string GetAllVisitorNames = apiVersion + "visitorNames";

        public const string GetVisitorById = apiVersion + "visitor/{id:int}";

        public const string UpdateVisitors = apiVersion + "updateVisitors";

        public const string DeleteVisitorById = apiVersion + "visitor/{id:int}";

        public const string CreateVisitorLog = apiVersion + "visitorLog";

        public const string GetAllVisitorLogs = apiVersion + "visitorLogs/{apartmentId:guid}/{visitorId:int}/{visitorTypeId:int}";

        public const string GetVisitorsByUnitId = apiVersion + "visitorsByUnitId/{apartmentId:guid}/{unitId:int}";        
    }

    public static class Events
    {
        public const string GetAllEvents = apiVersion + "events/{companyId:guid}/{bookingFacilitiesId:int}";

        public const string CreateEvents = apiVersion + "events";
    }

    public static class BokkingFacilities
    {
        public const string GetAllBokkingFacilities = apiVersion + "bookingFacilities";
    }

    public static class ServiceProviders
    {
        public const string CreateServiceProvider = apiVersion + "serviceProviders";

        public const string GetAllServiceProviders = apiVersion + "serviceProviders";

        public const string GetAllServiceProviderNames = apiVersion + "serviceProviderNames";

        public const string GetServiceProviderById = apiVersion + "serviceProvider/{id:int}";

        public const string UpdateServiceProviders = apiVersion + "updateServiceProviders";
    }

    public static class ServiceProviderSubType
    {
        public const string GetAllServiceProvderSubType = apiVersion + "serviceProviderSubType";
    }

    public static class UnitServiceProviders
    {
        public const string CreateUnitServiceProvider = apiVersion + "unitServiceProvider";

        public const string GetAllUnitServiceProviders = apiVersion + "unitServiceProviders";
    }

    public static class ServiceProviderType
    {
        public const string GetAllServiceProvderType = apiVersion + "serviceProviderType";
    }

    public static class Complaints
    {
        public const string CreateComplaint = apiVersion + "complaint";

        public const string GetAllComplaints = apiVersion + "complaints";
    }

    public static class Category
    {
        public const string GetAllCategory = apiVersion + "category";
    }

    public static class SubCategory
    {
        public const string GetAllSubCategory = apiVersion + "subCategory";
    }

    public static class ServiceProviderAssignedUnits
    {
        public const string GetAllAssignUnits = apiVersion + "assignUnits";

        public const string GetAllServiceProviderAssignedUnitsById = apiVersion + "assignUnits/{id:int}";
    }

    public static class OccupantTypes
    {
        public const string GetAllOccupantTypes = apiVersion + "occupantTypes";

    }

    public static class UnitTypes
    {
        public const string GetAllUnitTypes = apiVersion + "unitTypes";
    }

    public static class BuildingsTypes
    {
        public const string GetAllBuildingTypes = apiVersion + "buildingTypes";
    }

    public static class DeliveryCompanies
    {       
        public const string GetAllDeliveryCompanies = apiVersion + "deliveryCompanies";       

    }

    public static class Users
    {
        public const string GetAllUsers = apiVersion + "users";

    }
}
