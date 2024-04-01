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

        public const string UpdateResidentRequests = apiVersion + "updateResidentRequests";

    }
    public static class Vehicles
    {
        public const string CreateVehicle = apiVersion + "vehicles";

        public const string GetAllVehicles = apiVersion + "vehicles";

        public const string GetAllVehicleNames = apiVersion + "vehicleNames";

        public const string GetVehicleById = apiVersion + "vehicle/{id:int}";

        public const string UpdateVehicles = apiVersion + "updateVehicles";

    }

}
