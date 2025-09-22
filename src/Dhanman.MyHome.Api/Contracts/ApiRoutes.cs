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

        public const string GetAllBuildings = apiVersion + "apartments/{apartmentId:guid}/buildings";

        public const string GetAllBuildingNames = apiVersion + "apartments/{apartmentId:guid}/building-names";

        public const string GetBuildingById = apiVersion + "building/{id:int}";

        public const string UpdateBuilding = apiVersion + "building";

        public const string DeleteBuildingById = apiVersion + "building/{id:int}";

    }

    public static class CommunityCalenders
    {
        public const string GetAllCommunityCalenderNames = apiVersion + "community-calenders";

    }

    public static class Units
    {
        public const string CreateUnit = apiVersion + "unit";

        public const string CreateUnits = apiVersion + "units";

        public const string GetAllUnits = apiVersion + "apartments/{apartmentId:guid}/units";

        public const string GetAllUnitNames = apiVersion + "apartments/{apartmentId:guid}/buildings/{buildingId:int}/floors/{floorId:int}/unit-names";

        public const string GetAllUnitNamesByApartmentId = apiVersion + "apartments/{apartmentId:guid}/unit-names-of-apartment";

        public const string GetUnitById = apiVersion + "unit/{id:int}";

        public const string GetUnitIdByUserId = apiVersion + "users/{userId:guid}/apartments/{apartmentId:guid}/unit-id";

        public const string GetUnitsWithPrimaryOwner = apiVersion + "apartments/{apartmentId:guid}/units/primary-owners";

        public const string UpdateUnit = apiVersion + "unit";

        public const string GetAllUnitDetails = apiVersion + "unit-details";

        public const string DeleteUnit = apiVersion + "unit/{id:int}";

        public const string GetUnitByFloorId = apiVersion + "floors/{floorId:guid}/units";

        public const string GetRelatedDetails = apiVersion + "units/{unitId:int}/unit-related-details";
    }

    public static class Residents
    {
        public const string CreateResident = apiVersion + "residents";

        public const string GetAllResidents = apiVersion + "apartments/{apartmentId:guid}/residents";

        public const string GetAllResidentNames = apiVersion + "apartments/{apartmentId:guid}/resident-names";

        public const string GetResidentById = apiVersion + "resident/{id:int}";

        public const string UpdateResidents = apiVersion + "update-residents";
    }

    public static class ResidentRequests
    {
        public const string CreateResidentRequest = apiVersion + "resident-request";

        public const string GetAllResidentRequests = apiVersion + "apartments/{apartmentId:guid}/resident-requests";

        public const string GetAllResidentRequestNames = apiVersion + "resident-request-names";

        public const string GetResidentRequestById = apiVersion + "resident-request/{id:int}";

        public const string UpdateRequestApproveStatus = apiVersion + "request-approve-status";

        public const string UpdateRequestRejectStatus = apiVersion + "request-reject-status";

    }

    public static class MemberRequests
    {
        public const string GetAllMemberRequests = apiVersion + "communities/{apartmentId:guid}/member-requests";
        public const string ApproveMemberRequest = apiVersion + "approve-member-request";
    }
    public static class Vehicles
    {
        public const string CreateVisitorVehicle = apiVersion + "visitor-vehicles";

        public const string CreateUnitVehicle = apiVersion + "unit-vehicles";

        public const string GetAllVehicles = apiVersion + "vehicles";

        public const string GetAllVehicleNames = apiVersion + "vehicle-names";

        public const string GetVehicleById = apiVersion + "vehicle/{id:int}";

        public const string GetVehicleByUnitsId = apiVersion + "vehicle/{unitId:int}";

        public const string UpdateVehicles = apiVersion + "update-vehicles";
    }

    public static class Apartments
    {
        public const string CreateApartments = apiVersion + "apartment";

        public const string GetApartments = apiVersion + "apartments";

        public const string GetApartmentNames = apiVersion + "apartment-names";

        public const string GetApartmentById = apiVersion + "apartments/{apartmentId:guid}";

        public const string UpdateApartments = apiVersion + "update-apartments";
    }

    public static class Floors
    {
        public const string CreateFloor = apiVersion + "floor";

        public const string GetFloors = apiVersion + "apartments/{apartmentId:guid}/floors";

        public const string GetFloorNames = apiVersion + "apartments/{apartmentId:guid}/buildings/{buildingId:int}/floor-names";

        public const string GetFloorById = apiVersion + "floor/{id:int}";

        public const string UpdateFloor = apiVersion + "floor";

        public const string DeleteFloorById = apiVersion + "floor/{id:int}";
    }

    public static class Gates
    {
        public const string CreateGate = apiVersion + "gate";

        public const string GetAllGates = apiVersion + "apartments/{apartmentId:guid}/gates";

        public const string GetGateNames = apiVersion + "apartments/{apartmentId:guid}/gate-names";

        public const string GetGateById = apiVersion + "gate/{gateId:int}";

        public const string UpdateGate = apiVersion + "gate";

        public const string DeleteGateById = apiVersion + "gate/{id:int}";
    }

    public static class OccupancyTypes
    {
        public const string GetAllOccupancyTypes = apiVersion + "occupancy-types";
    }

    public static class Visitors
    {
        public const string CreateVisitor = apiVersion + "visitor";

        public const string GetAllVisitors = apiVersion + "visitors/{apartmentId:guid}";

        public const string GetAllVisitorNames = apiVersion + "visitor-names/{apartmentId:guid}";

        public const string GetVisitorById = apiVersion + "visitor/{id:int}";

        public const string UpdateVisitors = apiVersion + "update-visitors";

        public const string DeleteVisitorById = apiVersion + "visitor/{id:int}";

        public const string CheckInVisitorLog = apiVersion + "check-in";

        public const string CheckOutVisitorLog = apiVersion + "check-out";

        public const string GetAllVisitorLogs = apiVersion + "apartments/{apartmentId:guid}/dates/{date:dateTime}/visitorLogs";

        public const string GetSingleVisitorLogs = apiVersion + "apartments/{apartmentId:guid}/visitors/{visitorId:int}/visitorTypeIds/{visitorTypeId:int}/visitorLogs";

        public const string GetVisitorsByUnitId = apiVersion + "apartments/{apartmentId:guid}/units/{unitId:int}/visitorsByUnitId";

        public const string GetVisitorTypes = apiVersion + "visitor-types";

        public const string GetVisitorIdentityTypes = apiVersion + "visitor-identity-types";

        public const string UpdateVisitor = apiVersion + "visitor";

        public const string CreateVisitorWithPendingApproval = apiVersion + "visitors";

        public const string GetVisitorApprovalInfoById = apiVersion + "visitor-approval/{visitorApprovalId:int}";

        public const string GetVisitorsByEmailOrContactNumber = apiVersion + "apartments/{apartmentId:guid}/visitors";

        public const string VisitorsApproved = apiVersion + "approve";

        public const string VisitorsReject = apiVersion + "reject";

        public const string VisitorApprovals = apiVersion + "visitor-approvals";

    }

    public static class Events
    {
        public const string GetAllEvents = apiVersion + "companies/{companyId:guid}/events";
        public const string GetEvent = apiVersion + "events/{id:guid}";
        public const string CreateEvent = apiVersion + "events";
        public const string UpdateEvent = apiVersion + "events";
        public const string DeleteEvent = apiVersion + "events/{id:guid}";
        public const string GetCalendarEvents = apiVersion + "calendars/events";
    }

    #region MeetingDetails
    public static class MeetingDetails
    {
        public const string GetMeetingDetails = apiVersion + "events/{eventId:guid}/occurrence-date/{occurrenceDate:dateTime}/meeting-details";
    }
    #endregion

    public static class BookingFacilities
    {
        public const string GetAllBookingFacilities = apiVersion + "booking-facilities";
    }

    public static class EventOccurrences
    {
        public const string CreateEventOccurrence = apiVersion + "event-occurrences";

        public const string UpdateEventOccurrence = apiVersion + "event-occurrences";

        public const string DeleteEventOccurrenceById = apiVersion + "event-occurrence/{id:int}";

        public const string GetEventOccurrence = apiVersion + "event-occurrence/{id:int}";
    }

    public static class MeetingAgendaItems
    {
        public const string CreateMeetingAgendaItem = apiVersion + "meeting-agenda-items";

        public const string UpdateMeetingAgendaItem = apiVersion + "meeting-agenda-items";

        public const string DeleteMeetingAgendaItemById = apiVersion + "meeting-agenda-item/{id:int}";

        public const string GetMeetingAgendaItemById = apiVersion + "meeting-agenda-item/{id:int}";
    }

    public static class ServiceProviders
    {
        public const string CreateServiceProvider = apiVersion + "service-providers";

        public const string GetAllServiceProviders = apiVersion + "service-providers";

        public const string GetAllServiceProviderNames = apiVersion + "service-provider-names";

        public const string GetServiceProviderById = apiVersion + "service-provider/{id:int}";

        public const string UpdateServiceProviders = apiVersion + "update-service-providers";
    }

    public static class ServiceProviderSubType
    {
        public const string GetAllServiceProvderSubType = apiVersion + "service-provider-sub-type";
    }

    public static class UnitServiceProviders
    {
        public const string CreateUnitServiceProvider = apiVersion + "unit-service-provider";

        public const string GetAllUnitServiceProviders = apiVersion + "unit-service-providers";
    }

    public static class ServiceProviderType
    {
        public const string GetAllServiceProvderType = apiVersion + "service-provider-type";
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
        public const string GetAllSubCategory = apiVersion + "sub-category";
    }

    public static class ServiceProviderAssignedUnits
    {
        public const string GetAllAssignUnits = apiVersion + "assign-units";

        public const string GetAllServiceProviderAssignedUnitsById = apiVersion + "assign-units/{id:int}";
    }

    public static class OccupantTypes
    {
        public const string GetAllOccupantTypes = apiVersion + "occupant-types";

    }

    public static class UnitTypes
    {
        public const string GetAllUnitTypes = apiVersion + "unit-types";
    }

    public static class BuildingsTypes
    {
        public const string GetAllBuildingTypes = apiVersion + "building-types";
    }

    public static class DeliveryCompanies
    {
        public const string GetAllDeliveryCompanies = apiVersion + "delivery-companies";

    }

    public static class Users
    {

        public const string CreateUser = apiVersion + "users";

        public const string GetAllUsers = apiVersion + "users";

    }

    public static class Organizations
    {
        public const string InitializeOrganization = apiVersion + "initialize-organizations";
        public const string HardDeleteOrganization = apiVersion + "organizations/{organizationId:guid}";
        public const string GetOrganizationById = apiVersion + "organizations/{id:guid}";
    }

    public static class Companies
    {
        public const string CreateCompany = apiVersion + "companies";
    }

    public static class IdentityTypes
    {
        public const string GetAllIdentityTypes = apiVersion + "identity-types";

    }

    public static class GateTypes
    {
        public const string GetAllGateTypes = apiVersion + "gate-types";

    }

    public static class TicketStatuses
    {
        public const string GetTicketStatuses = apiVersion + "ticket-statuses";

        public const string UpdateTicketStatusAssign = apiVersion + "ticket-statuses/assign";

        public const string UpdateTicketStatusResolved = apiVersion + "ticket-statuses/resolved";

        public const string UpdateTicketStatusClosed = apiVersion + "ticket-statuses/closed";

        public const string UpdateTicketStatusCancelled = apiVersion + "ticket-statuses/cancelled";

        public const string UpdateTicketStatusRejected = apiVersion + "ticket-statuses/rejected";


    }

    public static class TicketCategories
    {
        public const string GetTicketCategories = apiVersion + "ticket-categories";
    }

    public static class TicketPriorities
    {
        public const string GetTicketPriorities = apiVersion + "ticket-priorities";
    }

    public static class Tickets
    {
        public const string CreateTicket = apiVersion + "tickets";
        public const string GetAllTickets = apiVersion + "tickets/{apartmentId:guid}";
        public const string GetTicketById = apiVersion + "ticket/{id:guid}";
        public const string UpdateTicketServiceProvider = apiVersion + "ticket-service-provider";
        public const string GetAllServiceProviderTicketCategories = apiVersion + "service-provider-ticket-categories";
    }

    public static class TicketServiceProviderOtp
    {
        public const string CreateTicketServiceProviderOtp = apiVersion + "ticket-service-provider-otp";
        public const string GetAllTickets = apiVersion + "tickets/{apartmentId:guid}";
        public const string GetOtpByTicketId = apiVersion + "ticket-otp/{ticketId:guid}";
        //public const string UpdateTicketServiceProvider = apiVersion + "ticket-service-provider";
        //public const string GetAllServiceProviderTicketCategories = apiVersion + "service-provider-ticket-categories";
    }

    public static class PushNotification
    {
        public const string CreatePushNotification = apiVersion + "guest-notification";

        public const string CreateUnitPushNotification = apiVersion + "units/{unitId:int}/guest-requests";

        public const string CreateResidentToken = apiVersion + "resident-token";
        public const string CreateUserFcmToken = apiVersion + "user-token";
    }

    public static class PublicApartments
    {
        public const string GetApartmentNames = apiVersion + "apartment-names";
    }

    public static class PublicMemberRequests
    {
        public const string CreateMemberRequest = apiVersion + "member-request";

    }

    public static class Permissions
    {
        public const string ClearAllPermissionsCache = apiVersion + "users/clear-all-permissions-cache";

        public const string ClearUserPermissionsCache = apiVersion + "users/clear-user-permissions-cache/{userId}/{organizationId}";

    }



    public static class CommitteeMembers
    {
        public const string GetAllCommitteeMemberNames = apiVersion + "apartments/{apartmentId:guid}/committee-member-names";

        public const string GetAllCommitteeMembers = apiVersion + "apartments/{apartmentId:guid}/committee-member";

        public const string CreateCommitteeMember = apiVersion + "committee-member";

        public const string GetPortfolioByApartmentId = apiVersion + "apartments/{apartmentId:guid}/portfolios";

        public const string GetAllRoles = apiVersion + "roles";


    }



    #region MeetingParticipants
    public static class MeetingParticipants
    {
        public const string UpdateMeetingParticipant = apiVersion + "participants";
        public const string GetAllMeetingParticipants = apiVersion + "occurrences/{occurrenceId:int}/participants";
    }
    #endregion

    #region MeetingActionItems
    public static class MeetingActionItems
    {
        public const string UpdateMeetingActionItem = apiVersion + "action-items";
    }
    #endregion

    #region MeetingNotes
    public static class MeetingNotes
    {
        public const string UpdateMeetingNote = apiVersion + "notes";
    }
    #endregion


    #region WaterTankerDeliveries
    public static class WaterTankerDeliveries
    {
        public const string CreateWaterTankerDeliveries = apiVersion + "water-tanker-deliveries";

        public const string WaterTankerDeliveryById = apiVersion + "water-tanker-delivery/{id:int}";

        public const string UpdateWaterTankerDelivery = apiVersion + "water-tanker-delivery";

        public const string DeleteWaterTankerDeliveryById = apiVersion + "water-tanker-delivery/{id:int}";

        public const string GetWaterTankerDeliverySummery = apiVersion + "companies/{companyId:guid}/{startDate:datetime}/{endDate:datetime}/water-tanker-summary";

        public const string GetAWaterTankerDeliveriesByVendorId = apiVersion + "companies/{companyId:guid}/vendors/{vendorId:guid}/{startDate:datetime}/{endDate:datetime}/water-tanker-deliveries";
    }
    #endregion

}
