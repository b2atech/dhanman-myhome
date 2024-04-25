namespace Dhanman.MyHome.Api.Contracts;

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

        public const string GetAllBuildings = apiVersion + "buildings";

        public const string GetAllBuildingNames = apiVersion + "buildingNames";        

        public const string GetBuildingById = apiVersion + "building/{id:int}";

        public const string UpdateBuildings = apiVersion + "updateBuilding";

    }

    public static class Units
    {
        public const string CreateUnit = apiVersion + "unit";

        public const string GetAllUnits = apiVersion + "units";

        public const string GetAllUnitNames = apiVersion + "unitNames";

        public const string GetUnitById = apiVersion + "unit/{id:int}";

        public const string UpdateUnits = apiVersion + "updateUnits";
       
        public const string GetAllUnitDetails = apiVersion + "unitDetail";

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

        public const string GetFloors = apiVersion + "floor/{buildingId:int}";

        public const string GetFloorNames = apiVersion + "floorNames/{buildingId:int}";

        public const string GetFloorById = apiVersion + "floor/{buildingId:int}";

        public const string UpdateFloors = apiVersion + "updateFloor";

    }
    public static class Gates
    {
        public const string CreateGate = apiVersion + "gate";

        public const string GetGates = apiVersion + "gates";

        public const string GetGateNames = apiVersion + "gateNames";

        public const string GetGatesById = apiVersion + "gate/{gateId:int}";

        public const string UpdateGates = apiVersion + "updateGate";

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

    }
    
}
