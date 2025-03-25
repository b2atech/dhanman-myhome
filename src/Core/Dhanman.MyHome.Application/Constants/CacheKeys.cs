namespace Dhanman.MyHome.Application.Constants;

public static class CacheKeys
{
    #region Buildings
    public static class Buildings
    {
        public const string CacheKeyPrefix = "buildings-{0}";

        public const string BuildingList = CacheKeyPrefix + "-list-{1}";

        public const string BuildingByApartmentId = CacheKeyPrefix + "-by-apartmentId-{1}";

        public const string BuildingById = CacheKeyPrefix + "-by-buildingId-{1}";
    }
    #endregion  

    #region Units
    public static class Units
    {
        public const string CacheKeyPrefix = "units-{0}";

        public const string UnitList = CacheKeyPrefix + "-list-{1}";

        public const string UnitByBuildingId = CacheKeyPrefix + "-by-buildingId-{1}";

        public const string UnitById = CacheKeyPrefix + "-by-unitId-{1}";

        public const string UnitIdByUserId = CacheKeyPrefix + "-by-userId-{1}";
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

        public const string GateByGateId = CacheKeyPrefix + "-by-gateId-{1}";
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

    #region Visitors
    public static class Visitors
    {
        public const string CacheKeyPrefix = "visitors-{0}";

        public const string VisitorList = CacheKeyPrefix + "-list-{1}";

        public const string VisitorById = CacheKeyPrefix + "-by-id-{1}";

        public const string VisitorLogList = CacheKeyPrefix + "-list-{1}";
    }
    #endregion

    #region Events
    public static class Events
    {
        public const string CacheKeyPrefix = "events-{0}";

        public const string EventList = CacheKeyPrefix + "-list-{1}";

        public const string EventById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region BookingFacilities
    public static class BookingFacilities
    {
        public const string CacheKeyPrefix = " bookingFacilities-{0}";

        public const string BookingFacilitiesList = CacheKeyPrefix + "-list-{1}";

        public const string BookingFacilitiesById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region ServiceProviderSubType
    public static class ServiceProviderSubType
    {
        public const string CacheKeyPrefix = " serviceProviderSubType-{0}";

        public const string ServiceProviderSubTypeList = CacheKeyPrefix + "-list-{1}";

        public const string ServiceProviderSubTypeById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region ServiceProviders
    public static class ServiceProviders
    {
        public const string CacheKeyPrefix = "serviceProviders-{0}";

        public const string ServiceProviderList = CacheKeyPrefix + "-list-{1}";

        public const string ServiceProviderById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region ServiceProviderSubType
    public static class ServiceProviderType
    {
        public const string CacheKeyPrefix = " serviceProviderType-{0}";

        public const string ServiceProviderTypeList = CacheKeyPrefix + "-list-{1}";

        public const string ServiceProviderTypeById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region Category
    public static class Category
    {
        public const string CacheKeyPrefix = " category-{0}";

        public const string CategoryList = CacheKeyPrefix + "-list-{1}";

    }
    #endregion

    #region SubCategory
    public static class SubCategory
    {
        public const string CacheKeyPrefix = " subCategory-{0}";

        public const string SubCategoryList = CacheKeyPrefix + "-list-{1}";

    }
    #endregion

    #region OccupantTypes
    public static class OccupantTypes
    {
        public const string CacheKeyPrefix = "occupantTypes-{0}";

        public const string OccupantTypeList = CacheKeyPrefix + "-list-{1}";

        public const string OccupantTypeById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region UnitTypes
    public static class UnitTypes
    {
        public const string CacheKeyPrefix = "unitTypes-{0}";

        public const string UnitTypeList = CacheKeyPrefix + "-list-{1}";

        public const string UnitTypeById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region BuildingTypes
    public static class BuildingTypes
    {
        public const string CacheKeyPrefix = " buildingTypes-{0}";

        public const string BuildingTypesList = CacheKeyPrefix + "-list-{1}";

    }
    #endregion

    #region ServiceProviderAssignUnits
    public static class ServiceProviderAssignUnits
    {
        public const string CacheKeyPrefix = "unitTypes-{0}";

        public const string ServiceProviderAssignUnitsId = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion

    #region DeliveryCompanies
    public static class DeliveryCompanies
    {
        public const string CacheKeyPrefix = "deliveryCompanies-{0}";

        public const string DeliveryCompanyList = CacheKeyPrefix + "-list-{1}";
 
    }
    #endregion

    #region Users
    public static class Users
    {
        public const string CacheKeyPrefix = "Users-{0}";

        public const string UserList = CacheKeyPrefix + "-list-{1}";

    }
    #endregion

    #region IdentiyTypes
    public static class IdentiyTypes
    {
        public const string CacheKeyPrefix = "identityTypes-{0}";

        public const string IdentityTypeList = CacheKeyPrefix + "-by-id-{1}";
    }


    #endregion

    #region GateTypes
    public static class GateTypes
    {
        public const string CacheKeyPrefix = "identityTypes-{0}";

        public const string GateTypeList = CacheKeyPrefix + "-by-id-{1}";
    }


    #endregion

    #region Tickets
    public static class Tickets
    {
        public const string CacheKeyPrefix = "tickets-{0}";

        public const string TicketList = CacheKeyPrefix + "-list-{1}";

        public const string TicketById = CacheKeyPrefix + "-by-ticketId-{1}";

        public const string ServiceProviderCategoryList = CacheKeyPrefix + "-list-{1}";

    }
    #endregion

    #region Tickets
    public static class TicketServiceProviderOtps
    {
        public const string CacheKeyPrefix = "tickets-{0}";

        public const string OtpByTicketId = CacheKeyPrefix + "-by-ticketId-{1}";

    }
    #endregion

    #region CommunityResidentRequests
    public static class CommunityResidentRequests
    {
        public const string CacheKeyPrefix = "communityResidentRequests-{0}";

        public const string CommunityResidentRequestList = CacheKeyPrefix + "-list-{1}";

        public const string CommunityResidentRequestById = CacheKeyPrefix + "-by-id-{1}";
    }
    #endregion  
}