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
        public const string CreateBuilding = apiVersion + "buildings";

        public const string GetAllBuildings = apiVersion + "buildings";

        public const string GetAllBuildingNames = apiVersion + "buildingNames";        

        public const string GetBuildingById = apiVersion + "building/{apartmentId:guid}";

        public const string UpdateBuildings = apiVersion + "updateBuilding";

    }

    public static class Units
    {
        public const string CreateUnit = apiVersion + "units";

        public const string GetAllUnits = apiVersion + "units";

        public const string GetAllUnitNames = apiVersion + "unitNames";

        public const string GetUnitById = apiVersion + "unit/{apartmentId:guid}";

        public const string UpdateUnits = apiVersion + "updateUnits";

    }

    public static class Residents
    {
        public const string CreateResident = apiVersion + "residents";

        public const string GetAllResidents = apiVersion + "residents";

        public const string GetAllResidentNames = apiVersion + "residentNames";

        public const string GetResidentById = apiVersion + "resident/{apartmentId:guid}";

        public const string UpdateResidents = apiVersion + "updateResidents";

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

        public const string GetApartmentById = apiVersion + "apartments/{apartmentId:guid}";

        public const string UpdateApartments = apiVersion + "updateApartments";

    }

}
