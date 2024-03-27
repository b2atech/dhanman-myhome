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

}
