namespace Dhanman.MyHome.Application.Constants;

public static class CacheKeys
{
    #region Residents
    public static class Residents
    {
        public const string CacheKeyPrefix = "residents-{0}";

        public const string ResidentList = CacheKeyPrefix + "-list-{1}";

        public const string ResidentById = CacheKeyPrefix + "-by-apartmentId-{1}";
    }
    #endregion  

    #region Buildings
    public static class Buildings
    {
        public const string CacheKeyPrefix = "buildings-{0}";

        public const string BuildingList = CacheKeyPrefix + "-list-{1}";

        public const string BuildingById = CacheKeyPrefix + "-by-apartmentId-{1}";
    }
    #endregion  
}