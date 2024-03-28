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
}