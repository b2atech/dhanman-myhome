namespace Dhanman.MyHome.Application.Constants;

public static class CacheKeys
{
    #region Buildings
    public static class Buildings
    {
        public const string CacheKeyPrefix = "buildings-{0}";

        public const string BuildingList = CacheKeyPrefix + "-list-{1}";

        public const string BuildingById = CacheKeyPrefix + "-by-apartmentId-{1}";
    }
    #endregion  

    #region Units
    public static class Units
    {
        public const string CacheKeyPrefix = "units-{0}";

        public const string UnitList = CacheKeyPrefix + "-list-{1}";

        public const string UnitById = CacheKeyPrefix + "-by-apartmentId-{1}";
    }
    #endregion  

    #region Residents
    public static class Residents
    {
        public const string CacheKeyPrefix = "residents-{0}";

        public const string ResidentList = CacheKeyPrefix + "-list-{1}";

        public const string ResidentById = CacheKeyPrefix + "-by-apartmentId-{1}";
    }
    #endregion  

    #region ResidentRequests
    public static class ResidentRequests
    {
        public const string CacheKeyPrefix = "residentRequests-{0}";

        public const string ResidentRequestList = CacheKeyPrefix + "-list-{1}";

        public const string ResidentRequestById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion  
    #region Vehicles
    public static class Vehicles
    {
        public const string CacheKeyPrefix = "vehicles-{0}";

        public const string VehicleList = CacheKeyPrefix + "-list-{1}";

        public const string VehicleById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region Apartments
    public static class Apartments
    {
        public const string CacheKeyPrefix = "apartments-{0}";

        public const string ApartmentList = CacheKeyPrefix + "-list-{1}";

        public const string ApartmentById = CacheKeyPrefix + "-by-id-{1}";

        public const string ApartmentNameList = CacheKeyPrefix + "-list-{1}";
    }
    #endregion  

    #region Floors
    public static class Floors
    {
        public const string CacheKeyPrefix = "floors-{0}";

        public const string FloorList = CacheKeyPrefix + "-list-{1}";

        public const string FloorNameList = CacheKeyPrefix + "-list-{1}";

        public const string FloorById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion  

    #region Gates
    public static class Gates
    {
        public const string CacheKeyPrefix = "gates-{0}";

        public const string GateList = CacheKeyPrefix + "-list-{1}";

        public const string GateNameList = CacheKeyPrefix + "-list-{1}";

        public const string GateById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region OccupancyTypes
    public static class OccupancyTypes
    {
        public const string CacheKeyPrefix = "occupancyTypes-{0}";

        public const string OccupancyTypeList = CacheKeyPrefix + "-list-{1}";

        public const string OccupancyTypeById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion  
}